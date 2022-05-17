# ValueVariant
[![GitHub](https://img.shields.io/github/license/hikarin522/ValueVariant)](LICENSE)
[![Nuget](https://img.shields.io/nuget/v/ValueVariant)](https://www.nuget.org/packages/ValueVariant/)
[![Nuget](https://img.shields.io/nuget/dt/ValueVariant)](https://www.nuget.org/packages/ValueVariant/)

C# source generator to generate efficient and type-safe variant types for unmanaged types.

## Usage

```cs
using System.ValueVariant;

[ValueVariant]
public readonly partial struct SampleVariant: IValueVariant<int, long, float> { }
```

will generates

```cs
readonly partial struct SampleVariant: IEquatable<SampleVariant>
{
    [StructLayout(LayoutKind.Explicit)]
    private struct Union
    {
        [FieldOffset(0)] int Item1;
        [FieldOffset(0)] long Item2;
        [FieldOffset(0)] float Item3;
    }

    public enum TypeIndex : byte { None, Type1, Type2, Type3 }

    private readonly Union Value;

    public readonly TypeIndex Index;

    public int Item1 => Index == TypeIndex.Type1 ? Value.Item1 : throw new InvalidCastException();
    public long Item2 => Index == TypeIndex.Type2 ? Value.Item2 : throw new InvalidCastException();
    public float Item3 => Index == TypeIndex.Type3 ? Value.Item3 : throw new InvalidCastException();

    public SampleVariant(int value) { ... }
    public SampleVariant(long value) { ... }
    public SampleVariant(float value) { ... }

    public static implicit operator SampleVariant(int value) => new SampleVariant(value);
    public static implicit operator SampleVariant(long value) => new SampleVariant(value);
    public static implicit operator SampleVariant(float value) => new SampleVariant(value);

    public static explicit operator int(SampleVariant value) => value.Item1;
    public static explicit operator long(SampleVariant value) => value.Item2;
    public static explicit operator float(SampleVariant value) => value.Item3;

    public bool Equals(SampleVariant other) { ... }
    public override bool Equals(object obj) { ... }
    public override int GetHashCode() { ... }
    public override string ToString() { ... }

    public static bool operator ==(SampleVariant lhs, SampleVariant rhs) { ... }
    public static bool operator !=(SampleVariant lhs, SampleVariant rhs) { ... }

    ...
}
```
