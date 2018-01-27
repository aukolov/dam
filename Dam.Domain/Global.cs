using System;

namespace Dam.Domain
{
    public static class Global
    {
        private static ILogger _logger;

        public static ILogger Logger
        {
            get => _logger;
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