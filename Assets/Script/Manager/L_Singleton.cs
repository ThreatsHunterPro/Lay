using UnityEngine;

public abstract class L_Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = null;
    
    public static T Instance => instance;

    protected virtual void Awake() => InitInstance();
    void InitInstance()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }
        instance = this as T;
        instance.name = $"{instance.name} [LOGIC]";
    }
}