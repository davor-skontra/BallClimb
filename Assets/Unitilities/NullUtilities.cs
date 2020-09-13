using System.Collections.Generic;

namespace Unitilities
{
    public static class NullUtilities
    {
        public static bool IsNullOrDefault<TAny>(this TAny item) =>
            EqualityComparer<TAny>.Default.Equals(item, default);
    }
}