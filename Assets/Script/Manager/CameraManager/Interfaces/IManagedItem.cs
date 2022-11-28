using UnityEngine;

public interface IManagedItem<TKey>
{
    public TKey ID { get; }

    public void InitItem();
    public void DestroyItem();
    public void Active();
    public void Desactive();
}