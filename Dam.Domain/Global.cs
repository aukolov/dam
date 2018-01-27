using System;

namespace Dam.Domain
{
    public static class Global
    {
        private static readonly ILogger EmptyLogger = new EmptyLogger();
        private static ILogger _logger;

        public static ILogger Logger
        {
            get => _logger ?? EmptyLogger;
            set
            {
                if (_logger != null)
                {
                    throw new InvalidOperationException("Logger has already been initialized.");
                }
                _logger = value;
            }
        }
    }
}