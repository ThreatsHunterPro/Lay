using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController),typeof(L_PlayerMovementComponent),typeof(L_PlayerBehaviour))]
[RequireComponent(typeof(Animator))]
public class L_Player : MonoBehaviour
{
    [SerializeField] CharacterController controller = null;
    [SerializeField] L_PlayerMovementComponent movement = null;
    [SerializeField] L_PlayerBehaviour behaviour = null;
    [SerializeField] L_CameraSettings settings = new L_CameraSettings();
    [SerializeField] Animator animator = null;

    public CharacterController Controller => controller;
    public L_PlayerMovementComponent Movement => movement;
    public L_PlayerBehaviour Behaviour => behaviour;
    public L_CameraSettings CameraSettings => settings;
    public Animator Animator => animator;

    public bool IsValid => controller && movement && behaviour && animator;

    private void Start() => Init();
    void Init()
    {
        L_PlayerManager.Instance?.SetFirstPlayer(this);
        InitComponents();
        InitCameras();
        InitEvents();
    }
    void InitComponents()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<L_PlayerMovementComponent>();
        behaviour = GetComponent<L_PlayerBehaviour>();
        animator = GetComponent<Animator>();
    }
    void InitCameras()
    {
        L_CameraManager.Instance?.CreateCamera<L_CameraTPS>(transform, settings, "TPSCamera");
    }

    void InitEvents()
    {
        if (!IsValid) return;
        behaviour.OnFalling += () =>
        {
            movement.SetGravity(-0.8f);
            SpawnRayAtPosition();
        };
    }
    void SpawnRayAtPosition()
    {
        Vector3 _position2 = transform.position + Vector3.forward * -30.0f; 
        L_PlayerManager.Instance?.SpawnRay(transform, _position2);
    }
}