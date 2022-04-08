
namespace System.ValueVariant;

public interface IValueVariantActionVisitor<T> where T: unmanaged
{
    void Visit(in T value);
}

public interface IValueVariantActionVisitor<T, in T1> where T: unmanaged
{
    void Visit(in T value, T1 arg1);
}

public interface IValueVariantFuncVisitor<T, out R> where T: unmanaged
{
    R Visit(in T value);
}

public interface IValueVariantFuncVisitor<T, in T1, out R> where T: unmanaged
{
    R Visit(in T value, T1 arg1);
}

public interface IValueVariantGenericActionVisitor
{
    void Visit<T>(in T value) where T: unmanaged;
}

public interface IValueVariantGenericActionVisitor<in T1>
{
    void Visit<T>(in T value, T1 arg1) where T: unmanaged;
}

public interface IValueVariantGenericFuncVisitor<out TR>
{
    TR Visit<T>(in T value) where T: unmanaged;
}

public interface IValueVariantGenericFuncVisitor<in T1, out TR>
{
    TR Visit<T>(in T value, T1 arg1) where T: unmanaged;
}
