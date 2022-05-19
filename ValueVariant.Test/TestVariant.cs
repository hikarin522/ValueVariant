using System;

using System.ValueVariant;

using Variant.Test;

namespace ValueVariant.Test;

[ValueVariant(ValueVariantGenerateOptions.MessagePackFormatter)]
public readonly partial struct TestVariant : IValueVariant<TestVariant, int, TestStruct<Guid>, DateTime> { }

public readonly struct TestVariantVisitor : TestVariant.IFuncVisitor<Type>
{
    public Type Visit(in int value)
        => typeof(int);

    public Type Visit(in TestStruct<Guid> value)
        => typeof(TestStruct<Guid>);

    public Type Visit(in DateTime value)
        => typeof(DateTime);
}

[ValueVariant]
public readonly partial struct TestVariant2 : IValueVariant<TestVariant2, TestStruct<Guid>, DateTime, int> { }
