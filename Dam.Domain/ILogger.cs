using System;

namespace Dam.Domain
{
    public interface ILogger : IDisposable
    {
        void Info(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);
    }
}