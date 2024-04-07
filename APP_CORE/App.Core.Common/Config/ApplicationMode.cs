using System;

namespace App.Core.Common.Config;

public class ApplicationMode
{
    /// <summary>
    ///     system enviroment of ASPNETCORE_ENVIRONMENT is different "Production" & "Staging" (case-sensitive)
    /// </summary>
    /// <returns></returns>
    public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    public static bool IsDevelopment => EnvironmentName == "Development";
}