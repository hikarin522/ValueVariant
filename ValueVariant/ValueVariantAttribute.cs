using System;

namespace ValueVariant
{
    [Flags]
    public enum ValueVariantGenerateOptions
    {
        None = 0,
        MessagePackFormatter = 256,
    }

    [AttributeUsage(AttributeTargets.Struct)]
    public sealed class ValueVariantAttribute : Attribute
    {
        public ValueVariantGenerateOptions Options { get; }

        public ValueVariantAttribute(ValueVariantGenerateOptions options = ValueVariantGenerateOptions.None)
        {
            this.Options = options;
        }
    }
}
