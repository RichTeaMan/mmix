using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib
{
    public class ReflectionUtilities
    {
        /// <summary>
        /// Find classes that extend the given class and then returns an instance.
        /// 
        /// The default constructor will be used. If there is no default constructor the instance will be ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> FindExtendingClasses<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.GetConstructor(Type.EmptyTypes)?.Invoke(null))
                .Where(x => x != null)
                .Cast<T>();
        }
    }
}
