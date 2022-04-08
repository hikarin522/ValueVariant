
using System.Runtime.CompilerServices;

namespace System.ValueVariant.Details;

internal static class Unsafe
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe ref readonly U As<T, U>(in T value) where T : unmanaged where U : unmanaged
    {
        fixed (T* p = &value) {
            return ref *(U*)p;
        }
    }
}
