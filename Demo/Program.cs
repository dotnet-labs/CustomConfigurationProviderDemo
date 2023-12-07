namespace Demo;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddMyConfiguration(options =>
                {
                    options.ConnectionString = "Data Source=sqlite.db";
                    options.Query = "SELECT Key, Value FROM SYS_CONFIGS";
                });
            });
}