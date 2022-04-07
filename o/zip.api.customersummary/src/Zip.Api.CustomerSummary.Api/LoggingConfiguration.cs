﻿using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Zip.Api.CustomerSummary.Api
{
    [ExcludeFromCodeCoverage]
    public static class LoggingConfiguration
    {
        public static LoggerConfiguration ConfigureLogger()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var loggerConfiguration = new LoggerConfiguration().MinimumLevel.Debug()
                                                               .MinimumLevel
                                                               .Override("Microsoft", LogEventLevel.Information)
                                                               .Enrich.FromLogContext();

            if (environment == "Production")
            {
                loggerConfiguration
                    .WriteTo.Console(new CustomElasticsearchJsonFormatter());
            }
            else
            {
                loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.File(new CustomElasticsearchJsonFormatter(), "logs/log.txt", rollingInterval: RollingInterval.Day);
            }

            return loggerConfiguration;
        }

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
