namespace GenericCacheExcercise;

public class GenericCacheConfig
{
    public int Threshold { get; init; }

    public GenericCacheConfig(int threshold)
    {
        Threshold = threshold;
    }
}