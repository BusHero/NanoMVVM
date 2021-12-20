
using System;
using System.Collections.Generic;

namespace NanoMVVM.Utils;

internal static class DictionaryExtenssions
{
    public static Value GetValueOrSet<Key, Value>(this Dictionary<Key, Value> dictionary, Key key, Value @default)
        where Key : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);

        if (dictionary.TryGetValue(key, out var value))
            return value;

        dictionary[key] = @default;
        return @default;
    }

    public static Value GetValueOrSet<Key, Value>(this Dictionary<Key, Value> dictionary, Key key, Func<Value> @default)
        where Key : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);

        if (dictionary.TryGetValue(key, out var value))
            return value;

        var defaultValue = @default();
        dictionary[key] = defaultValue;
        return defaultValue;
    }
}