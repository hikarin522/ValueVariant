using System;

using Variant.Test;

namespace ValueVariant.Test;

[ValueVariant(ValueVariantGenerateOptions.MessagePackFormatter)]
public readonly partial struct TestVariant : IValueVariant<TestVariant, int, TestStruct<Guid>, DateTime>
{
    public static explicit operator TestVariant(TestVariant2 value)
            => value.TryCast(out var result) ? result : throw new InvalidCastException();

    public readonly bool TryCast(out TestVariant2 result)
    {
        if (this == default) { result = default; return true; }
        this.Accept(TestVariant2.DefaultConverter.Instance, out result);
        return result != default;
    }
}

public readonly struct TestVariantVisitor : TestVariant.IFuncVisitor<Type>, TestVariant2.IFuncVisitor<Type>
{
    public Type Visit(in int value) => typeof(int);

    public Type Visit(in TestStruct<Guid> value) => typeof(TestStruct<Guid>);

    public Type Visit(in DateTime value) => typeof(DateTime);

    public Type Visit(in long value) => typeof(long);

    public Type Visit(in bool value) => typeof(bool);
}

[ValueVariant]
public readonly partial struct TestVariant2 : IValueVariant<TestVariant2, TestStruct<Guid>, DateTime, int, long, bool>
{
    public static explicit operator TestVariant2(TestVariant value)
        => value.TryCast(out var result) ? result : throw new InvalidCastException();

    public readonly bool TryCast(out TestVariant result)
    {
        if (this == default) { result = default; return true; }
        this.Accept(TestVariant.DefaultConverter<long, bool>.Instance, out result);
        return result != default;
    }
}
