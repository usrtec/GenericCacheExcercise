using Microsoft.Extensions.Logging;

namespace GenericCacheExcercise;

public class TestService : ITestService
{
    private readonly GenericCache<int, string> _cache = GenericCache<int, string>.GetInstance(new GenericCacheConfig(5));
    private readonly ILogger<TestService> _log;

    public TestService(ILogger<TestService> log)
    {
        _cache.CacheFull += OnCacheFull;
        _log = log;
    }    

    public void Load(params int[] values)
    {
        foreach (int v in values)
        {
            Console.WriteLine(_cache[v] = $"Loading service data for Id: {v}");
        }
    }

    public void PrintCache()
    {
        Console.WriteLine("Latest Cache:");
        foreach (var item in _cache)
        {
            Console.WriteLine(item);
        }
    }

    private void OnCacheFull(object? sender, CacheFullEventArgs<string> e)
    {
        Console.WriteLine($"{GetType().Name} service. Cache Full, least recently used item with value: [{e.Value}] removed");
    }
}
