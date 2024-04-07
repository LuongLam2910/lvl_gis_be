using System.Reflection;

namespace Authorize.Services
{
    public class ServiceAssembly
    {
        public static Assembly Assembly => typeof(ServiceAssembly).Assembly;
    }
}