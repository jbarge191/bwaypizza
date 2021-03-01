using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

 public class ToppingsComparer : IEqualityComparer<List<string>>
    {
        public bool Equals([AllowNull] List<string> x, [AllowNull] List<string> y)
        {
            if (x == null || y == null)
                return false;
            else if (x.Count != y.Count)
                return false;
            return x.Intersect(y).Count() == x.Count; // rely on the string equality comparer when using the Intersect method
        }

        /* This method is called in case Equals returns true. This would
        tell the Distinct of two objects (in this case toppings) are actually true or not. Calculating the hash code is just adding together the
        hash code of each string within the list.
        */
        public int GetHashCode([DisallowNull] List<string> obj)
        {
            int hashCode = 0;
            foreach (string o in obj)
                hashCode += o.GetHashCode();

            return hashCode;
        }
    }