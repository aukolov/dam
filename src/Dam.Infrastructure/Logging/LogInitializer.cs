using Dam.Domain;

namespace Dam.Infrastructure.Logging
{
    public static class LogInitializer
    {
        public static void InitializeLogger()
        {
            Global.Logger = new SerilogLogger();
        }
    }
}