using System;

namespace Dam.Domain
{
    public interface ILogger : IDisposable
    {
        void Info(string message, params object[] args);
        void Exception(Exception exception, string message, params object[] args);
        void Error(string message, params object[] args);
    }
}