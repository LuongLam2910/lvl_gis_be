using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace App.Core.Common;

public static class ConfigurationTelerikReportHelper
{
    public static IConfiguration ResolveConfiguration(IWebHostEnvironment environment)
    {
        var reportingConfigFileName = Path.Combine(environment.ContentRootPath, "appsettings.json");
        return new ConfigurationBuilder()
            .AddJsonFile(reportingConfigFileName, true)
            .Build();
    }
}