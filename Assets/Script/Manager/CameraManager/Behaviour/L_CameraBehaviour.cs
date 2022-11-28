using UnityEngine;

public abstract class L_CameraBehaviour : MonoBehaviour, IManagedItem<string>
{
    [SerializeField] string id = "Default ID";
    [SerializeField] L_CameraSettings settings = new L_CameraSettings();

    public string ID => id;
    public L_CameraSettings Settings => settings;

    private void Start() => InitItem();
    private void OnDestroy() => DestroyItem();
    public void InitItem()
    {
        settings?.SetCameraRender(Settings.Camera);
        L_CameraManager.Instance?.Add(this);
        L_CameraManager.Instance.OnCameraUpdated += () =>
        {
            MoveToTarget();
            RotateToTarget();
        };
    }
    public void DestroyItem()
    {
        L_CameraManager.Instance?.Remove(this);
        Destroy(gameObject);
    }
    public void Active()
    {
        gameObject.SetActive(true);
    }
    public void Desactive()
    {
        gameObject.SetActive(false);
    }
    public void SetID(string _id)
    {
        if (string.IsNullOrEmpty(_id)) return;
        id = _id;
    }
    public void SetSettings(L_CameraSettings _settings) => settings = _settings;

    protected abstract void MoveToTarget();
    protected abstract Vector3 GetPosition();
    protected abstract void RotateToTarget();
    protected abstract Quaternion GetRotation();
}