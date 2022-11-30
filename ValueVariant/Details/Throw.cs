using System;

#if NETSTANDARD2_1_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace ValueVariant.Details;

internal static class Throw
{
#if NETSTANDARD2_1_OR_GREATER
    [DoesNotReturn]
#endif
    internal static void InvalidOperationException() => throw new InvalidOperationException();

#if NETSTANDARD2_1_OR_GREATER
    [DoesNotReturn]
#endif
    internal static T InvalidOperationException<T>() => throw new InvalidOperationException();

#if NETSTANDARD2_1_OR_GREATER
    [DoesNotReturn]
#endif
    internal static ref T InvalidOperationExceptionRef<T>() => throw new InvalidOperationException();
}
