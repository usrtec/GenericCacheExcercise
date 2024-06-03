namespace GenericCacheExcercise;

public class GenericCacheConfig
{
    internal int Threshold { get; init; }

    public GenericCacheConfig(int threshold)
    {
        Threshold = threshold;
    }
}