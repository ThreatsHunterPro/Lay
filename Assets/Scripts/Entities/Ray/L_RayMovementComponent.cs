using System.Collections.Generic;
using UnityEngine;
using System;

public class L_RayMovementComponent : L_MovementComponent
{
    public event Action<L_Ray> OnLand = null;

    [SerializeField, Range(1, 100)] float definition = 10;
    [SerializeField, Range(1, 100)] float distance = 5;
    [SerializeField, Range(1, 100)] float amplitude = 5;
    [SerializeField, Range(-1, 1)] float directionX, directionY = 0;
    [SerializeField] bool drawDebugs = true;
    [SerializeField] GameObject mesh = null;
    [SerializeField] float Yaw = 0;
    [SerializeField] float MaxYaw = 30;
    [SerializeField] Vector3 landPosition = Vector3.zero;
    [SerializeField] bool isLanding = false;

    Vector3 FinalTargetPos
    {
        get
        {
            Vector3 _offset = Vector3.zero;
            _offset += transform.right * directionX * amplitude;
            _offset += transform.up * directionY * amplitude;
            _offset += transform.forward* distance;
            return transform.position + _offset;
        }
    }
    Vector3 TargetPos => GetMovementCurve(transform.position, transform.position + transform.forward * distance, FinalTargetPos, 1f);

    void Start()
    {
        InitInputs();
        DeleteInputs();
        mesh = transform.GetChild(1).gameObject;
        GetComponent<L_RayBehaviour>().onLandFound += InitLanding;
    }
    private void Update()
    {
        ConstantMovement();
        if (isLanding && Vector3.Distance(landPosition, transform.position) < 1)
            OnLand?.Invoke(GetComponent<L_Ray>());
    }
    private void OnDrawGizmos()
    {
        if (!drawDebugs) return;
        for (float i = 0; i <= 1; i += 1.0f / definition)
        {
            Gizmos.color = Color.red;
            Vector3 _first = GetMovementCurve(transform.position, transform.position + transform.forward * distance, FinalTargetPos, i);
            Vector3 _second = GetMovementCurve(transform.position, transform.position + transform.forward * distance, FinalTargetPos, i + 1.0f / definition);
            Gizmos.DrawLine(_first, _second);
        }
        Gizmos.DrawSphere(GetMovementCurve(transform.position, transform.position + transform.forward * distance, FinalTargetPos, 1), 0.1f);
    }
    private void OnDestroy()
    {
        OnLand = null;
    }
    public override void InitInputs()
    {
        L_InputManager.Instance.BindAxis(EAxisEvent.RAY_HORIZONTAL_AXIS, SetForwardMovement);
        L_InputManager.Instance.BindAxis(EAxisEvent.RAY_VERTICAL_AXIS, SetUpMovement);
    }
    public override void DeleteInputs()
    {
        L_InputManager.Instance.UnBindAxis(EAxisEvent.RAY_HORIZONTAL_AXIS, SetForwardMovement);
        L_InputManager.Instance.UnBindAxis(EAxisEvent.RAY_VERTICAL_AXIS, SetUpMovement);
    }
    public override void SetForwardMovement(float _axis)
    {
        directionX = _axis;
        SetPitch(_axis);
    }
    public override void SetPitch(float _axis)
    {
        if (!mesh) return;
        float _targetYaw = Yaw + _axis * MaxYaw;
        if (_axis == 0)
            _targetYaw = 0;
       
        Yaw = Mathf.Lerp(Yaw, _targetYaw, Time.deltaTime * rotateSpeed / 30 );
            mesh.transform.localEulerAngles  = new Vector3(Yaw, mesh.transform.localEulerAngles.y, mesh.transform.localEulerAngles.z);
    }
    void ConstantMovement()
    {
        if(!CanMove) return;
        Vector3 _nextPos = Vector3.MoveTowards(transform.position, isLanding ? landPosition : TargetPos, Time.deltaTime * movementSpeed);
        transform.position = _nextPos;
        Vector3 _direction = TargetPos - transform.position;
        if (_direction == Vector3.zero) return;
        Quaternion _lookAt = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookAt, Time.deltaTime * rotateSpeed);
    }
    Vector3 GetMovementCurve(Vector3 P1, Vector3 P2, Vector3 P3, float _time)
    {
        Vector3 V1 = Vector3.Lerp(P1, P2, _time);
        Vector3 V2 = Vector3.Lerp(P2, P3, _time);
        return Vector3.Lerp(V1, V2, _time);
    }
    void InitLanding(Vector3 _position)
    {
        isLanding = true;
        landPosition = _position;
    }
    public void SetUpMovement(float _axis) => directionY = _axis ;
}