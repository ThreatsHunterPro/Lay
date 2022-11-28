using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class L_PlayerBehaviour : MonoBehaviour
{
    event Action onAir = null;
    public event Action OnFalling = null;

    [SerializeField] L_Player owner = null;
    [SerializeField] LayerMask wallLayer = 0, rayLayer = 0;
    [SerializeField, Range(0.0f, 50.0f)] float radius = 5.0f, timeBeforeSpawning = 5.0f;
    [SerializeField] bool isGrounded = true, isAlreadySpawned = false, isAlreadySat = false;

    public bool IsValid => owner;
    public Vector3 CurrentPosition => transform.position;

    private void Start() => Init();
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.white;
    }
    private void OnDestroy()
    {
        onAir = null;
        OnFalling = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        L_Ray _ray = other.GetComponent<L_Ray>();
        if (_ray)
        {
            if (isAlreadySat) return;
            owner.Movement.CanMove = false;
            owner.Movement.DeleteInputs();
            isAlreadySat = true;
            _ray.RayBehaviour.AttachTo(transform);
            _ray.RayMovement.CanMove = true;
            _ray.RayMovement.InitInputs();
            _ray.RayMovement.OnLand += QuitRay;
        }
    }
    void QuitRay(L_Ray _ray)
    {
        owner.Movement.CanMove = true;
        isAlreadySat = false;
        _ray.RayBehaviour.Detatch(transform);
        GetComponent<L_PlayerMovementComponent>().InitInputs();
        _ray.RayMovement.DeleteInputs();
        _ray.RayMovement.OnLand -= QuitRay;
        Destroy(_ray.gameObject);
        L_CameraManager.Instance?.Remove(L_CameraManager.Instance?.MainCamera);
        //L_CameraManager.Instance.MainCamera.Active = false;
    }

    void Init()
    {
        owner = GetComponent<L_Player>();
        onAir += DetectWalls;
        InvokeRepeating(nameof(UpdateIsGrounded), 0.0f, 0.1f);
    }
    void UpdateIsGrounded()
    {
        if (!IsValid) return;
        isGrounded = owner.Controller.isGrounded;
        if (!isGrounded) DetectWalls();
        owner.Animator.SetBool(AnimParam.isFalling, !isGrounded);
    }
    void DetectWalls()
    {
        bool hit = Physics.SphereCast(CurrentPosition, radius, Vector3.down, out RaycastHit _hitInfos, Mathf.Infinity, wallLayer);
        if (!hit)
        {
            Invoke(nameof(SpawnRay), timeBeforeSpawning);
        }
    }
    void SpawnRay()
    {
        if (isAlreadySpawned) return;
        OnFalling?.Invoke();
        isAlreadySpawned = true;
    }
}