namespace GenericCacheExcercise;

public class GenericCacheValue<TKey, TValue>
{
    internal TKey Key { get; }

    internal TValue Value { get; set; }

    internal DateTime UsedLast { get; set; }

    public GenericCacheValue(TKey key, TValue value, DateTime usedLast)
    {
        Key = key;
        Value = value;
        UsedLast = usedLast;
    }

    public override string ToString()
    {
        return $"Value: {Value}";
    }
}