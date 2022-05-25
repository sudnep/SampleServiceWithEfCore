using ArtGallaryManager.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class SingleTonService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IRepository _repoService;
    private readonly SecondService _secondService;

    public string ID { get; set; }
    public SingleTonService(IServiceScopeFactory serviceScopeFactory, IRepository repoService, SecondService secondService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _repoService = repoService;
        _secondService = secondService;
        this.ID = Guid.NewGuid().ToString();

    }


    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var i = 1;
        while (true)
        {

            //using (var scope = _serviceScopeFactory.CreateScope())
            //{
            //    var repo = scope.ServiceProvider.GetRequiredService<IRepository>();

            //    var secondService = scope.ServiceProvider.GetRequiredService<SecondService>();
            //    var setttings = await _repoService.GetSettings();
            //    await secondService.MethodInSecondServiceAsync();
            //    var settings2 = await _repoService.GetSettings();
            //}

            var setttings = await _repoService.GetSettings();
            await _secondService.MethodInSecondServiceAsync();
            var settings2 = await _repoService.GetSettings();

            Console.WriteLine("Loop " + i.ToString());
            Task.Delay(2000).Wait();
            i++;

        }


    }
}
// registered scoped

// registered transient
public class SecondService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IRepository _repoService;

    public SecondService(IServiceScopeFactory serviceScopeFactory, IRepository repoService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _repoService = repoService;
    }

    public async Task MethodInSecondServiceAsync()
    {

        //using (var scope = _serviceScopeFactory.CreateScope())
        //{
        //    var repo = scope.ServiceProvider.GetRequiredService<IRepository>();
        //    Console.WriteLine("Scoped started");
        //    var eventSetting1 = await repo.AddSettings(new EventSetting() { Key = "Initial", Value = "Initial" });
        //    var setttingss = await repo.GetSettings();
        //    var updatedFromDb1 = setttingss.Where(x => x.Value == "DB").FirstOrDefault();
        //    if (updatedFromDb1 != null)
        //    {
        //        Console.WriteLine("found");
        //    }
        //    var set2 = setttingss.Where(x => x.Id == eventSetting1.Id).FirstOrDefault();
        //    set2.Value = "Updated";
        //    await repo.AddSettings(set2);
        //    Console.WriteLine("Scoped ended");
        //}



        var eventSetting = await _repoService.AddSettings(new EventSetting() { Key = "Initial", Value = "Initial" });
        var setttings = await _repoService.GetSettings();
        var setttingsss = await _repoService.GetSettings();
        var settings3 = _repoService.GetSettings(); // should have thrown error  but does not due to context being already disposed.
        var settings4 = _repoService.GetSettings(); // should have thrown error  but does not due to context being already disposed.
        var settings5 = _repoService.GetSettings();


        var updatedFromDb = setttings.Where(x => x.Value == "DB").FirstOrDefault();
        if (updatedFromDb != null)
        {
            Console.WriteLine("found");
        }
        var set1 = setttings.Where(x => x.Id == eventSetting.Id).FirstOrDefault();
        set1.Value = "Updated";
        await _repoService.AddSettings(set1);



    }



}
