using System.Collections.Generic;
using UnityEngine;

public interface IManager<TKey, TValue> where TValue : IManagedItem<TKey>
{
    public L_CustomDictionary<TKey, TValue> Items { get; }
    
    public void Add(TValue _value);
    public void Remove(TKey _key);
    public void Remove(TValue _value);
    public TValue Get(TKey _key);
    public void Enable(TKey _key);
    public void Enable(TValue _value);
    public void Disable(TKey _key);
    public void Disable(TValue _value);
    public bool Exist(TKey _key);
    public bool Exist(TValue _value);
}