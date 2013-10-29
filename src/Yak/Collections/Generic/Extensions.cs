using System.Collections.Generic;
using System.Linq;

namespace Yak.Collections.Generic {
    public static class Extensions {
        // todo: compare these methods with System.Exensions.DeepClone() impl for speed + correctness

        // I think this implements a shallow clone for refernce types and a deep clone for value types, but not sure
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key,
                                           entry => entry.Value);
        }
    }
}