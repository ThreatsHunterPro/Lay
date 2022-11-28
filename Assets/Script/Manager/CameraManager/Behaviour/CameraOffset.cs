using UnityEngine;

[System.Serializable]
public struct CameraOffset
{
    [SerializeField] Vector3 offset;

    public Vector3 GetOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        Vector3 _x = _target.right * offset.x;
        Vector3 _y = _target.up * offset.y;
        Vector3 _z = _target.forward * offset.z;
        return _target.position + _x + _y + _z;
    }
    public Vector3 GetLookAtOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        Vector3 _x = _target.right * offset.x;
        Vector3 _y = _target.up * offset.y;
        Vector3 _z = _target.forward * offset.z;
        return _x + _y + _z;
    }
}