namespace GenericCacheExcercise;

public class CacheFullEventArgs<TValue> : EventArgs
{
    public TValue Value { get; init; }

    public CacheFullEventArgs(TValue value)
    {
        Value = value;
    }
}
