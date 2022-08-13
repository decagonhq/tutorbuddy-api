public class DatabaseSetup
{
    /// <summary>
    /// Checks if ASPNETCORE Environment is Development
    /// <returns>boolean</returns>
    /// </summary>
    private static bool IsDevelopment =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    /// <summary>
    /// gets the port which depends on ASPNETCORE_ENVIRONMENT
    /// <returns>5000 or the hosting provider PORT</returns>
    /// </summary>
    public static string? HostPort =>
        IsDevelopment
            ? "5000"
            : Environment.GetEnvironmentVariable("PORT");

    /// <summary>
    /// Gets the Heroku Postgres Connection string for Production environment
    /// </summary>
    /// <returns>string</returns>
    private static string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetValue<string>("ConnectionStrings:ConnectionStr");
    }

    /// <summary>
    /// Gets the connection string for either Development or Production environments
    /// </summary>
    /// <returns>string</returns>
    public static string DatabaseConnectionString(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        return webHostEnvironment.IsDevelopment()
            ? configuration.GetConnectionString("ConnectionStr")
            : GetConnectionString(configuration);
    }
}