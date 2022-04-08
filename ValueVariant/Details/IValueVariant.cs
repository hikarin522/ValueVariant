
namespace System.ValueVariant.Details;

public interface IValueVariant
{
    byte TypeIndexByte { get; }

    void AcceptGenericActionVisitor<TV>(in TV visitor) where TV : IValueVariantGenericActionVisitor;

    void AcceptGenericActionVisitor<TV, TA1>(in TV visitor, TA1 arg1) where TV : IValueVariantGenericActionVisitor<TA1>;

    void AcceptGenericFuncVisitor<TV, TR>(in TV visitor, out TR result) where TV : IValueVariantGenericFuncVisitor<TR>;

    void AcceptGenericFuncVisitor<TV, TA1, TR>(in TV visitor, TA1 arg1, out TR result) where TV : IValueVariantGenericFuncVisitor<TA1, TR>;
}

public interface IValueVariantElement<T>: IValueVariant where T: unmanaged { }
