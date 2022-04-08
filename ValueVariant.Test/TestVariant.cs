using System;

using System.ValueVariant;

using Variant.Test;

namespace ValueVariant.Test;

[ValueVariant(ValueVariantGenerateOptions.MessagePackFormatter)]
public readonly partial struct TestVariant : IValueVariant<TestVariant, int, TestStruct<Guid>, DateTime> { }

public readonly struct TestVariantVisitor : TestVariant.IFuncVisitor<int>
{
    public int Visit(in int value)
        => 1;

    public int Visit(in TestStruct<Guid> value)
        => 2;

    public int Visit(in DateTime value)
        => 3;
}

[ValueVariant]
public readonly partial struct TestVariant2 : IValueVariant<TestVariant2, TestStruct<int>, DateTime, int> { }
