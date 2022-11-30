using System;

using MessagePack;

using Variant.Test;

using Xunit;

namespace ValueVariant.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            TestVariant v;
            TestVariant2 v2;
            Type type;

            v = default(int);
            Assert.Equal(typeof(int), v.Accept(new TestVariantVisitor()));
            v2 = (TestVariant2)v;
            v2.Accept(new TestVariantVisitor(), out type);
            Assert.Equal(typeof(int), type);

            v = default(TestStruct<Guid>);
            Assert.Equal(typeof(TestStruct<Guid>), v.Accept(new TestVariantVisitor()));
            v2 = (TestVariant2)v;
            v2.Accept(new TestVariantVisitor(), out type);
            Assert.Equal(typeof(TestStruct<Guid>), type);

            v = default(DateTime);
            Assert.Equal(typeof(DateTime), v.Accept(new TestVariantVisitor()));
            v2 = (TestVariant2)v;
            v2.Accept(new TestVariantVisitor(), out type);
            Assert.Equal(typeof(DateTime), type);

            v2 = default(long);
            Assert.Equal(typeof(long), v2.Accept(new TestVariantVisitor()));
            Assert.Throws<InvalidCastException>(() => (TestVariant)v2);
        }

        [Fact]
        public void Test2()
        {
            TestVariant v;
            int v1;
            TestStruct<Guid> v2;
            DateTime v3;
            byte[]? buff;
            TestVariant d;

            var intValue = 0x12345678;
            v = intValue;
            Assert.Equal(TestVariant.TypeIndex.Type1, v.Index);
            Assert.Equal(intValue, v.Item1);
            Assert.Throws<InvalidCastException>(() => v.Item2);
            Assert.Throws<InvalidCastException>(() => v.Item3);
            Assert.True(v.TryGetValue(out v1));
            Assert.False(v.TryGetValue(out v2));
            Assert.False(v.TryGetValue(out v3));
            Assert.Equal(intValue, v1);
            buff = MessagePackSerializer.Serialize(v);
            d = MessagePackSerializer.Deserialize<TestVariant>(buff);
            Assert.Equal(v, d);

            var guid = new TestStruct<Guid>(Guid.NewGuid());
            v = guid;
            Assert.Equal(TestVariant.TypeIndex.Type2, v.Index);
            Assert.Equal(guid, v.Item2);
            Assert.Throws<InvalidCastException>(() => v.Item1);
            Assert.Throws<InvalidCastException>(() => v.Item3);
            Assert.False(v.TryGetValue(out v1));
            Assert.True(v.TryGetValue(out v2));
            Assert.False(v.TryGetValue(out v3));
            Assert.Equal(guid, v2);
            buff = MessagePackSerializer.Serialize(v);
            d = MessagePackSerializer.Deserialize<TestVariant>(buff);
            Assert.Equal(v, d);

            var now = DateTime.UtcNow;
            v = now;
            Assert.Equal(TestVariant.TypeIndex.Type3, v.Index);
            Assert.Equal(now, v.Item3);
            Assert.Throws<InvalidCastException>(() => v.Item1);
            Assert.Throws<InvalidCastException>(() => v.Item2);
            Assert.False(v.TryGetValue(out v1));
            Assert.False(v.TryGetValue(out v2));
            Assert.True(v.TryGetValue(out v3));
            Assert.Equal(now, v3);
            buff = MessagePackSerializer.Serialize(v);
            d = MessagePackSerializer.Deserialize<TestVariant>(buff);
            Assert.Equal(v, d);
        }
    }
}
