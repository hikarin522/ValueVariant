
using System.Runtime.CompilerServices;

namespace System.ValueVariant.Details;

public interface IValueVariant
{
    byte TypeIndex { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    void Accept(IValueVariantGenericActionVisitor visitor);

    void Accept<TA1>(IValueVariantGenericActionVisitor<TA1> visitor, TA1 arg1);

    TR Accept<TR>(IValueVariantGenericFuncVisitor<TR> visitor);

    TR Accept<TA1, TR>(IValueVariantGenericFuncVisitor<TA1, TR> visitor, TA1 arg1);
}

public interface IValueVariantElement<T>: IValueVariant where T: unmanaged { }
