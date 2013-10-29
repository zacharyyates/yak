using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yak {
    public static class ObjectExtensions {
        /// <summary>
        /// Indicates whether an <see cref="object"/> is null.
        /// </summary>
        public static bool IsNull<T>(this T obj) where T : class {
            return obj == null;
        }

        public static T DeepClone<T>(this T obj) {
            using (var ms = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}