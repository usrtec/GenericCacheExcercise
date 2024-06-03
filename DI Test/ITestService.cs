namespace GenericCacheExcercise;

public interface ITestService
{
    void Load(params int[] values);

    void PrintCache();
}