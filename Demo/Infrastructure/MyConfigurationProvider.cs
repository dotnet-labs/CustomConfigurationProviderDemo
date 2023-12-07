using System.Data.SQLite;

namespace Demo.Infrastructure;

public class MyConfigurationProvider(MyConfigurationSource source) : ConfigurationProvider
{
    public override void Load()
    {
        using var conn = new SQLiteConnection(source.ConnectionString);
        conn.Open();
        using var cmd = new SQLiteCommand(source.Query, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Set(reader.GetString(0), reader.GetString(1));
        }
    }
}

public class MyConfigurationOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
}

public class MyConfigurationSource(MyConfigurationOptions options) : IConfigurationSource
{
    public string ConnectionString { get; set; } = options.ConnectionString;
    public string Query { get; set; } = options.Query;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new MyConfigurationProvider(this);
    }
}

public static class MyConfigurationExtensions
{
    public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder configuration, Action<MyConfigurationOptions> options)
    {
        _ = options ?? throw new ArgumentNullException(nameof(options));
        var myConfigurationOptions = new MyConfigurationOptions();
        options(myConfigurationOptions);
        configuration.Add(new MyConfigurationSource(myConfigurationOptions));
        return configuration;
    }
}