using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ChromeDriver>();
    })
    .Build();

await host.RunAsync();
