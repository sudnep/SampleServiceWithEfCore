// See https://aka.ms/new-console-template for more information

using ArtGallaryManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

var connectionString = configuration.GetConnectionString("AppDataSVC");

Console.WriteLine("Hello, World!");
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddTransient<IRepository, Repository>()
            .AddDbContextFactory<AppDBContext>(x => x.UseSqlServer(connectionString))
            .AddHostedService<SingleTonService>()
            .AddTransient<SecondService>())
    .Build();
//Run(host.Services);
await host.RunAsync();

//static void Run(IServiceProvider services)
//{
//    using IServiceScope serviceScope = services.CreateScope();
//    IServiceProvider provider = serviceScope.ServiceProvider;

//    var singleTonService = provider.GetRequiredService<SingleTonService>();
//    singleTonService.Run();


//    Console.WriteLine();
//}