using System;
using UnityEngine;

namespace Manager.CameraManager
{
    public class L_CameraManager : L_Singleton<L_CameraManager>, IManager<string, L_CameraBehaviour>
    {
        public event Action OnCameraUpdated = null;

        [SerializeField] L_CustomDictionary<string, L_CameraBehaviour> cameras = new L_CustomDictionary<string, L_CameraBehaviour>();
        [SerializeField] L_CameraBehaviour mainCamera = null;

        public bool IsValid => mainCamera;
        public L_CustomDictionary<string, L_CameraBehaviour> Items => cameras;
        public L_CameraBehaviour MainCamera => mainCamera;

        private void Update() => OnCameraUpdated?.Invoke();
        private void OnDestroy() => OnCameraUpdated = null;

        public T CreateCamera<T>(Transform _owner, L_CameraSettings _settings, string _id = "Default ID") where T : L_CameraBehaviour, new()
        {
            GameObject _cameraObject = new GameObject(_id, typeof(T), typeof(Camera));
            T _cameraBehaviour = _cameraObject.GetComponent<T>();
            _cameraBehaviour.SetID(_id);
            _cameraBehaviour.SetSettings(_settings);
            Camera _camera = _cameraObject.GetComponent<Camera>();
            _cameraBehaviour.Settings.SetCameraRender(_camera);
            _cameraBehaviour.Settings.SetTarget(_owner);
            Add(_cameraBehaviour);
            return _cameraBehaviour;
        }
        public void Add(L_CameraBehaviour _value)
        {
            if (Exist(_value)) return;
            _value.name = $"{_value.name} [CAMERA]";
            cameras.Add(_value.ID.ToLower(), _value);
            mainCamera = _value;
        }
        public void Remove(string _key)
        {
            if (!Exist(_key.ToLower())) return;
            cameras.Remove(_key.ToLower());
            Get(_key.ToLower()).DestroyItem();
        }
        public void Remove(L_CameraBehaviour _value)
        {
            if (!Exist(_value)) return;
            cameras.Remove(_value.ID.ToLower());
            //_value.DestroyItem();
        }
        public L_CameraBehaviour Get(string _key)
        {
            if (!Exist(_key.ToLower())) return null;
            return cameras[_key.ToLower()];
        }
        public void Enable(string _key)
        {
            if (!Exist(_key.ToLower())) return;
            Get(_key.ToLower()).Active();
        }
        public void Enable(L_CameraBehaviour _value)
        {
            if (!Exist(_value)) return;
            Get(_value.ID.ToLower()).Active();
        }
        public void Disable(string _key)
        {
            if (!Exist(_key.ToLower())) return;
            Get(_key.ToLower()).Desactive();
        }
        public void Disable(L_CameraBehaviour _value)
        {
            if (!Exist(_value)) return;
            Get(_value.ID.ToLower()).Desactive();
        }
        public bool Exist(string _key) => cameras.ContainsKey(_key.ToLower());
        public bool Exist(L_CameraBehaviour _value) => cameras.ContainsKey(_value.ID.ToLower());
    }
}