using System;

using Variant.Test;

namespace ValueVariant.Test;

[ValueVariant(ValueVariantGenerateOptions.MessagePackFormatter)]
public readonly partial struct TestVariant : IValueVariant<TestVariant, int, TestStruct<Guid>, DateTime>
{
    public static explicit operator TestVariant(TestVariant2 value)
        => value.Accept(TestVariant2Converter.Instance);

    private sealed class TestVariant2Converter : DefaultConverter<TestVariant2Converter>, TestVariant2.IFuncVisitor<TestVariant>
    {
        public TestVariant Visit(in long value) => throw new InvalidCastException();
        public TestVariant Visit(in bool value) => throw new InvalidCastException();
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
    // implicit because TestVariant ⊂ TestVariant2
    public static implicit operator TestVariant2(TestVariant value)
        => value.Accept(TestVariantConverter.Instance);

    private sealed class TestVariantConverter : DefaultConverter<TestVariantConverter>, TestVariant.IFuncVisitor<TestVariant2> { }
}
