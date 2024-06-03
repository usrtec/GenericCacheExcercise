using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GenericCacheExcercise;

internal class Program
{
    static void Main(string[] args)
    {
        var provider = new ServiceCollection()
            .AddTransient<ITestService, TestService>()
            .AddLogging(configure => configure.AddLog4Net("log4net.config"))
            .Configure<LoggerFilterOptions>(o => o.MinLevel = LogLevel.Debug)
            .BuildServiceProvider();

        var logger = provider.GetRequiredService<ILogger<Program>>();

        //DI Test
        var service = provider.GetRequiredService<ITestService>();        

        try
        {
            service.Load([2, 4, 5, 7, 2, 1, 9]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        service.PrintCache();

        //Local Test
        var config = new GenericCacheConfig(4);
        var cache = GenericCache<int, string>.GetInstance(config);

        cache.CacheFull += (o, a) => Console.WriteLine($"Removed:[{a.Value}]");

        try
        {
            Console.WriteLine(cache[9]);

            Console.WriteLine(cache[1] = "Loading local data for Id: 1");
            Console.WriteLine(cache[2] = "Loading local data for Id: 2");
            Console.WriteLine(cache[1] = "Loading local data for Id: 1");
            Console.WriteLine(cache[4] = "Loading local data for Id: 4");

            Console.WriteLine(cache[6]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        Console.WriteLine("Latest Cache:");
        foreach (var item in cache)
        {
            Console.WriteLine(item);
        }
    }
}
