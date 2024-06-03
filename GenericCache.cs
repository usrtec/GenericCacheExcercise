using System.Collections;

namespace GenericCacheExcercise;

public class GenericCache<TKey, TValue> : IEnumerable where TKey : notnull
{
    private static GenericCacheConfig _config;

    private readonly Dictionary<TKey, GenericCacheValue<TKey, TValue>> _cachedData = new(_config.Threshold);

    private static GenericCache<TKey, TValue>? _instance;    

    public event EventHandler<CacheFullEventArgs<TValue>>? CacheFull;

    public TValue this[TKey key]
    {
        get
        {
            if (_cachedData.ContainsKey(key))
            {
                _cachedData[key].UsedLast = DateTime.UtcNow;

                return _cachedData[key].Value;
            }

            throw new KeyNotFoundException("Supplied key was not found in the cache.");
        }
        set
        {
            if (!_cachedData.ContainsKey(key))
            {
                _cachedData[key] = new GenericCacheValue<TKey, TValue>(key, value, DateTime.UtcNow);
            }
            else
            {
                _cachedData[key].Value = value;
                _cachedData[key].UsedLast = DateTime.UtcNow;
            }

            if (_cachedData.Count >= _config.Threshold)
            {
                var itemToRemove = _cachedData.Values.MinBy(x => x.UsedLast);

                if (itemToRemove != null)
                {
                    CacheFull?.Invoke(this, new CacheFullEventArgs<TValue>(itemToRemove.Value));

                    _cachedData.Remove(itemToRemove.Key);
                }
            }
        }
    }

    private GenericCache() { }

    public static GenericCache<TKey, TValue> GetInstance(GenericCacheConfig config)
    {
        _config = config;

        if (_instance == null)
        {
            _instance = new GenericCache<TKey, TValue>();
        }

        return _instance;
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var item in _cachedData)
        {
            yield return item;
        }
    }
}