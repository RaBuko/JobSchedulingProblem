namespace Solver.Utils
{
    public class UserDefined : System.Attribute
    {
        public UserDefined(string parameterFormalName)
        {
            ParameterFormalName = parameterFormalName;
        }

        public string ParameterFormalName { get; }
    }
}
