using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;

namespace Zip.Api.CustomerProfile.Helpers
{
    public static class LoggingConfiguration
    {
        public static LoggerConfiguration ConfigureLogger()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Information)
                .Enrich.WithExceptionData()
                .Enrich.FromLogContext();
#pragma warning disable CS0618
            if (environment == EnvironmentName.Development)
                loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.File(new CustomElasticsearchJsonFormatter(), "logs/log.txt",
                        rollingInterval: RollingInterval.Day);
            else
                loggerConfiguration
                    .WriteTo.Console(new CustomElasticsearchJsonFormatter());

            return loggerConfiguration;
        }
#pragma warning restore CS0618
        private class CustomElasticsearchJsonFormatter : ElasticsearchJsonFormatter
        {
            protected override void WriteTimestamp(DateTimeOffset timestamp, ref string delim, TextWriter output)
            {
                // default field name @timestamp clashes with Fluentd
                WriteJsonProperty("source-timestamp", timestamp, ref delim, output);
            }
        }
    }
}