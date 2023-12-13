using Serilog.Events;
using Serilog;

namespace EsMasBarato.Excepciones
{
    public class ExcepcionLogger
    {
        public static void ConfigurarLogger(string logFilePath)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.File(
                path: logFilePath,
                restrictedToMinimumLevel: LogEventLevel.Error,
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    }
}

