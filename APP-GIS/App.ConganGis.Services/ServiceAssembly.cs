using System.Reflection;

namespace App.CongAnGis.Services;

public class ServiceAssembly
{
    public static Assembly Assembly => typeof(ServiceAssembly).Assembly;
}