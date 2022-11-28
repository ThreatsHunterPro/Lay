using UnityEngine;

[System.Serializable]
public class L_CameraSettings
{
    [SerializeField] Camera camera = null;
    [SerializeField] Transform target = null;
    [SerializeField, Range(0.0f, 100.0f)] float moveSpeed = 50.0f, rotateSpeed = 70.0f;
    [SerializeField] bool canMove = true, canRotate = true;
    [SerializeField] CameraOffset offset = new CameraOffset(),
                            lookAtOffset = new CameraOffset();

    public bool IsValidCamera => camera;
    public Camera Camera => camera;
    public bool IsValidTarget => target;
    public Transform Target => target;
    public bool CanMove => canMove;
    public bool CanRotate => canRotate;
    public CameraOffset Offset => offset;
    public CameraOffset LookAtOffset => lookAtOffset;
    public float MoveSpeed => moveSpeed;
    public float RotationSpeed => rotateSpeed;
    public Vector3 CurrentPosition
    {
        get
        {
            return IsValidCamera ? camera.transform.position : Vector3.zero;
        }
    }
    public Quaternion CurrentRotation
    {
        get
        {
            return IsValidCamera ? camera.transform.rotation : Quaternion.identity;
        }
    }
    public Vector3 TargetPosition
    {
        get
        {
            return IsValidTarget ? target.transform.position : CurrentPosition;
        }
    }

    public void SetCameraRender(Camera _camera)
    {
        if (!_camera) return;
        camera = _camera;
    }
    public void SetTarget(Transform _target)
    {
        if (!_target) return;
        target = _target;
    }
}