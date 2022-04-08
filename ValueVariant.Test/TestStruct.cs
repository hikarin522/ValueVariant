
using MessagePack;

namespace Variant.Test;

[MessagePackObject]
public readonly struct TestStruct<T>
    where T : unmanaged
{
    [Key(0)]
    public T Item { get; }

    public TestStruct(T value) => Item = value;
}
