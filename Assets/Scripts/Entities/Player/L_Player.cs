using Manager.CameraManager;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(CharacterController),typeof(Animator))]
    [RequireComponent(typeof(L_PlayerMovementComponent),typeof(L_PlayerBehaviour))]
    public class L_Player : MonoBehaviour
    {
        [Header("Player values")]
        [Header("Components")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private CharacterController controller = null;
        [SerializeField] private L_PlayerMovementComponent movement = null;
        [SerializeField] private L_PlayerBehaviour behaviour = null;
        [SerializeField] private L_CameraSettings settings = new L_CameraSettings();
        
        [Header("Ray values")]
        [SerializeField] L_Ray rayModel = null;
        [SerializeField] private L_Ray ray = null;
        [SerializeField, Range(0.0f, 100.0f)] private float offset = 30.0f;

        private bool IsValid => controller && movement && behaviour && animator;
        public Animator Animator => animator;
        public CharacterController Controller => controller;
        public L_PlayerMovementComponent Movement => movement;

        private void Start() => Init();
        void Init()
        {
            // L_EntityManager.Instance.SetFirstPlayer(this);
            L_CameraManager.Instance.CreateCamera<L_CameraTPS>(transform, settings, "Player Camera");

            if (!IsValid)
            {
                animator = GetComponent<Animator>();
                controller = GetComponent<CharacterController>();
                movement = GetComponent<L_PlayerMovementComponent>();
                behaviour = GetComponent<L_PlayerBehaviour>();
            }
        
            behaviour.OnFalling += () =>
            {
                movement.SetGravity(-0.8f);
                CallRay();
            };
        }
    
        private void CallRay()
        {
            if (ray || !rayModel) return;
            
            Transform _transform = transform;
            Vector3 _position = _transform.position - Vector3.forward * offset;
            
            ray = Instantiate(rayModel, _position, _transform.rotation);
            ray.name += " [RAY]";
            ray.RayBehaviour.SetTarget(_transform);
            L_CameraManager.Instance.CreateCamera<L_CameraTPS>(ray.transform, rayModel.Settings, "Ray Camera");
        }
    }
}