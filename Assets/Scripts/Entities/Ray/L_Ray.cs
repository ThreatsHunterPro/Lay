using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(L_RayMovementComponent),typeof(Animator))]
public class L_Ray : MonoBehaviour
{
    [SerializeField] L_RayMovementComponent rayMovement = null;
    [SerializeField] L_RayBehaviour rayBehaviour = null;
    [SerializeField] L_CameraSettings settings = new L_CameraSettings();
    [SerializeField] Animator animator = null;

    public bool IsValid => rayMovement && rayBehaviour && animator; 
    public L_RayMovementComponent RayMovement => rayMovement;
    public L_RayBehaviour RayBehaviour => rayBehaviour;
    public L_CameraSettings Settings => settings;
    public Animator Animator => animator;

    private void Start() => InitComponent();
    void InitComponent()
    {
        rayMovement = GetComponent<L_RayMovementComponent>();
        rayBehaviour = GetComponent<L_RayBehaviour>();
        animator = GetComponent<Animator>();
    }
}
