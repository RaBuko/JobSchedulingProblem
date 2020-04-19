using System;
using System.ComponentModel;

namespace Solver.Utils
{
    public class UserDefined : Attribute
    {
        public UserDefined(string parameterFormalName, Type type, object defaultValue = null)
        {
            ParameterFormalName = parameterFormalName;
            Type = type;
            DefaultValue = defaultValue;
        }

        public string ParameterFormalName { get; }

        public object DefaultValue { get; set; }

        public Type Type { get; set; }
    }
}
