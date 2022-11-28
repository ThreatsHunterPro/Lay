using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class L_MovementComponent : MonoBehaviour
{
    [SerializeField, Range(0.0f, 500.0f)] protected float movementSpeed = 10.0f, rotateSpeed = 10.0f;
    [SerializeField] protected bool canMove = true, canRotate = true;

    public bool CanMove { get => canMove; set => canMove = value; }
    public bool CanRotate { get => canRotate; set => canRotate = value; }

    public abstract void InitInputs();
    public abstract void DeleteInputs();
    public abstract void SetForwardMovement(float _axis);
    public abstract void SetPitch(float _axis);
}
