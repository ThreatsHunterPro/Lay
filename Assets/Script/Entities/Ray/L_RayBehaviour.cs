using UnityEngine;
using System;

public class L_RayBehaviour : MonoBehaviour
{
    public event Action<Vector3> onLandFound = null;

    [SerializeField] float rate = 1;
    [SerializeField] float range = 1000;
    [SerializeField] LayerMask groundLayer = 0;
    [SerializeField] Transform target = null;
    [SerializeField] Transform socket = null;
    [SerializeField, Range(0.0f, 100.0f)] float moveSpeed = 5.0f, rotateSpeed = 2.0f;
    [SerializeField] bool follow = true;

    private void Start() => Init();
    public bool IsValid => target;
    public Transform Socket => socket;

    private void Update()
    {
        if (IsValid && follow)
        {
            MoveToTarget();
            RotateToTarget();
        }
    }
    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
    }
    void RotateToTarget()
    {
        Vector3 _direction = target.position - transform.position;
        if (_direction == Vector3.zero) return;
        Quaternion _lookAt = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookAt, Time.deltaTime * rotateSpeed * 5.0f);
    }
    public void SetTarget(Transform _target)
    {
        if (!_target) return;
        target = _target;
    }
    public void AttachTo(Transform _toAttach)
    {
        _toAttach.SetParent(socket);
        L_Player _player = socket.GetComponentInChildren<L_Player>();
        _player.transform.localPosition = Vector3.zero;
        follow = false;
    }
    public void Detatch(Transform _toAttach)
    {
        _toAttach.SetParent(null);
        follow = true;
    }
    void Init()
    {
        InvokeRepeating(nameof(UpdateGroundUnder), 0f, rate);
    }
    void UpdateGroundUnder()
    {

        bool _hit = Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out RaycastHit _rayHit, range, groundLayer);
        if (_hit)
        {
            onLandFound?.Invoke(_rayHit.point);
            if (_rayHit.distance < 1) Debug.Log("LAND");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (-transform.up + transform.forward).normalized * range);

    }

}