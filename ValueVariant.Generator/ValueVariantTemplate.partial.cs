
using System.Linq;
using System.ValueVariant;

namespace ValueVariant.Generator;

public partial class ValueVariantTemplate
{
    public ValueVariantGenerateOptions Options { get; }

    public string TypeName { get; }

    public string Namespace { get; }

    public string FullName => $"{Namespace}.{TypeName}";

    public string[] Types { get; }

    public int Count => Types.Length;

    public ValueVariantTemplate(ValueVariantGenerateOptions options, string variantType, string[] types)
    {
        this.Options = options;
        var t = variantType.Split('.');
        this.TypeName = t.Last();
        this.Namespace = string.Join(".", t.Take(t.Length - 1));
        this.Types = types;
    }
}
