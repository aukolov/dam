using System;

namespace Dam.Domain
{
    internal class EmptyLogger : ILogger
    {
        public void Dispose()
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Exception(Exception exception, string message, params object[] args)
        {
        }

        public void Error(string message, params object[] args)
        {
        }
    }
}