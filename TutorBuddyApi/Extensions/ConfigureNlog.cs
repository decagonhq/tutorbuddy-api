using NLog.Web;

public static class ConfigureNlogExtenstion
{
    public static void ConfigureNlog(this WebApplicationBuilder builder)
    {
        
        var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
        logger.Debug("Hi Testing Logging");
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.Host.UseNLog();
    }
}