﻿using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Demo.IntegrationTests;

public class TestHostFixture : IDisposable
{
    public HttpClient Client { get; }
    public IServiceProvider ServiceProvider { get; }

    public TestHostFixture()
    {
        var builder = Program.CreateHostBuilder([])
            .ConfigureWebHost(webHost => webHost.UseTestServer());
        var host = builder.Start();
        ServiceProvider = host.Services;
        Client = host.GetTestClient();
        Console.WriteLine("TEST Host Started.");
    }

    public void Dispose()
    {
        Client.Dispose();
        GC.SuppressFinalize(this);
    }
}