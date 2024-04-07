using System.Reflection;

namespace App.QTHTGis.Services;

public class ServiceAssembly
{
    public static Assembly Assembly => typeof(ServiceAssembly).Assembly;
}