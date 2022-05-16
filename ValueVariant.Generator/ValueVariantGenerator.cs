using System;
using System.Collections.Generic;
using System.Linq;
using System.ValueVariant;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ValueVariant.Generator;

[Generator(LanguageNames.CSharp)]
public sealed class ValueVariantGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver(nameof(ValueVariantAttribute)));
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
        {
            return;
        }

        var attrSymbol = context.Compilation.GetTypeByMetadataName(typeof(ValueVariantAttribute).FullName);
        if (attrSymbol is null)
        {
            return;
        }

        var variantSymbols = receiver.List.Select(node => {
            return context.Compilation
                .GetSemanticModel(node.SyntaxTree)
                .GetDeclaredSymbol(node, token) as INamedTypeSymbol;
        }).Where(e => e is not null)
        .Distinct(SymbolEqualityComparer.Default).Cast<INamedTypeSymbol>()
        .Select(symbol => {
            var attr = symbol.GetAttributes()
                .SingleOrDefault(e => SymbolEqualityComparer.Default.Equals(e.AttributeClass, attrSymbol));
            if (attr is null) return (symbol, attr, itf: default(INamedTypeSymbol?));

            var itf = symbol.Interfaces
                .SingleOrDefault(e => e.ToString().StartsWith("System.ValueVariant.IValueVariant<") && e.TypeArguments.Length >= 2);

            return (symbol, attr, itf);
        }).Where(e => e.attr is not null && e.itf is not null).Select(e => (e.symbol, e.attr!, e.itf!));

        foreach (var (variantSymbol, attrData, itf) in variantSymbols)
        {
            var options = attrData.ConstructorArguments.Length > 0
                ? (ValueVariantGenerateOptions)attrData.ConstructorArguments[0].Value!
                : ValueVariantGenerateOptions.None;

            var variantType = variantSymbol.ToString();

            var types = itf.TypeArguments.Skip(1).Select(e => {
                if (e.SpecialType != SpecialType.None) {
                    return e.SpecialType.ToString().Replace('_', '.');
                }

                var name = e.ToString();
                if (name.EndsWith("?"))
                {
                    name = name.TrimEnd('?');
                    name = $"System.Nullable<{name}>";
                }

                return name;
            }).ToArray();

            var template = new ValueVariantTemplate(options, variantType, types);
            context.AddSource($"{variantType}.cs", template.TransformText());
        }
    }
}

internal class SyntaxReceiver : ISyntaxReceiver
{
    public string AttrName { get; }

    public List<StructDeclarationSyntax> List { get; } = new();

    public SyntaxReceiver(string attrName)
    {
        this.AttrName = attrName.EndsWith(nameof(Attribute))
            ? attrName.Substring(0, attrName.Length - nameof(Attribute).Length)
            : attrName;
    }

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not StructDeclarationSyntax { AttributeLists.Count: > 0 } node)
        {
            return;
        }

        if (node.AttributeLists.Any(e => e.Attributes.Any(attr => attr.Name.ToString().Contains(this.AttrName))))
        {
            List.Add(node);
        }
    }
}
