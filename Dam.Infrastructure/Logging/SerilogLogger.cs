using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using ILogger = Dam.Domain.ILogger;

namespace Dam.Infrastructure.Logging
{
    public class SerilogLogger : ILogger
    {
        private readonly Logger _logger;

        public SerilogLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                .CreateLogger();
        }

        public void Info(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public void Dispose()
        {
            _logger?.Dispose();
        }
    }
}