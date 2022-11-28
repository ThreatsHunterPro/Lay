using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class L_PlayerMovementComponent : L_MovementComponent
{
    [SerializeField] L_Player owner = null;
    [SerializeField] float gravity = -1;
    public bool IsValid => owner;

    private void Start() => Init();

    void Init()
    {
        InitInputs();
        owner = GetComponent<L_Player>();
    }
    public override void InitInputs()
    {
        L_InputManager.Instance.BindAxis(EAxisEvent.HORIZONTAL_AXIS, SetPitch);
        L_InputManager.Instance.BindAxis(EAxisEvent.VERTICAL_AXIS, SetForwardMovement);
    }
    public override void DeleteInputs()
    {
        L_InputManager.Instance.UnBindAxis(EAxisEvent.HORIZONTAL_AXIS, SetPitch);
        L_InputManager.Instance.UnBindAxis(EAxisEvent.VERTICAL_AXIS, SetForwardMovement);
    }
    public override void SetForwardMovement(float _axis)
    {
        if (!IsValid || !canMove) return;
        Vector3 _direction = transform.forward * _axis * movementSpeed * Time.deltaTime;
        _direction.y = gravity * Time.deltaTime;
        owner.Controller.Move(_direction);
        owner.Animator.SetFloat(AnimParam.playerSpeedParam, _axis);
    }
    public override void SetPitch(float _axis)
    {
        if (!IsValid) return;
        Vector3 _direction = transform.up * _axis * rotateSpeed * Time.deltaTime;
        transform.eulerAngles += _direction;
    }
    public void SetGravity(float _gravity) => gravity = _gravity;
}