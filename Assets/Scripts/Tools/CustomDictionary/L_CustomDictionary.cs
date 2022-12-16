using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MyKeyValuePair<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public MyKeyValuePair(TKey _key, TValue _value)
    {
        Key = _key;
        Value = _value;
    }

    public override string ToString() => $"{Key} - {Value}";
}

[Serializable]
public class L_CustomDictionary<TKey, TValue> : IEnumerable
{
    [SerializeField] List<TKey> keys = new List<TKey>();
    [SerializeField] List<TValue> values = new List<TValue>();

    public TValue this[TKey key]
    {
        get
        {
            if (!ContainsKey(key)) return default(TValue);
            return values[keys.IndexOf(key)];
        }
    }
    public int KeysCount => keys.Count;
    public int ValuesCount => values.Count;

    public L_CustomDictionary() { }
    public L_CustomDictionary(List<TKey> _keys, List<TValue> _values)
    {
        keys = _keys;
        values = _values;
    }

    public void Add(TKey _key, TValue _value)
    {
        keys.Add(_key);
        values.Add(_value);
    }
    public bool ContainsKey(TKey _key) => keys.Contains(_key);
    public bool ContainsValue(TValue _value) => values.Contains(_value);
    public bool Remove(TKey _key)
    {
        if (!ContainsKey(_key)) return false;
        keys.Remove(_key);
        values.Remove(this[_key]);
        return true;
    }
    public void Clear()
    {
        keys.Clear();
        values.Clear();
    }
    public IEnumerator GetEnumerator() => ((IEnumerable)keys).GetEnumerator();
}