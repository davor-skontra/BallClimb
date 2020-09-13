using System;
using System.Collections.Generic;

namespace Core.Disposal
{
    public static class DisposableUtility
    {
        public static void Dispose(this IEnumerable<IDisposable> self)
        {
            foreach (var disposable in self)
            {
                disposable.Dispose();
            }
        }
    }
}
