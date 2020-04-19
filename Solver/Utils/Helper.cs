using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Solver.Methods;
using System.Collections;

namespace Solver.Utils
{
    public static class Helper
    {
        public static List<(Type, Type)> GetMethodAndOptionsTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = GetTypesInNamespace(assembly, "Solver.Methods");
            var methods = types.Where(x => typeof(IMethod).IsAssignableFrom(x));
            var options = types.Where(x => typeof(IMethodOptions).IsAssignableFrom(x));

            var methodOptionRelations = new List<(Type method, Type methodOptions)>();
            foreach (var method in methods)
            {
                string methodName = method.Name.Replace("Method", string.Empty);
                methodOptionRelations.Add((method, options.First(x => x.Name.StartsWith(methodName))));
            }
            return methodOptionRelations;
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes()
                  .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                  .Where(t => !t.IsInterface)
                  .ToArray();
        }

        public static List<(PropertyInfo, string)> GetParametersValues<T>(T options) where T : IMethodOptions
        {
            var toReturn = new List<(PropertyInfo, string)>(); 
            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(options);
                if (prop.PropertyType.GetInterface(nameof(IEnumerable)) == null)
                {
                    toReturn.Add((prop, value.ToString()));
                }
            }
            return toReturn;
        }
    }
}
