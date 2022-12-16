using Entities.Player;
using Manager.CameraManager;
using UnityEngine;

namespace Manager.PlayerManager
{
    public class L_EntityManager : L_Singleton<L_EntityManager>
    {
        // [SerializeField] L_Ray defaultRay = null;
        // L_Player firstPlayer = null;
        // L_Ray firstRay = null;
        //
        // public void SpawnRay(Transform _target, Vector3 _position)
        // {
        //     if (!defaultRay || firstRay || !_target) return;
        //     L_Ray _ray = Instantiate(defaultRay, _position, _target.rotation);
        //     L_CameraManager.Instance.CreateCamera<L_CameraTPS>(_ray.transform, defaultRay.Settings, "Ray Camera");
        //     firstRay = _ray;
        //     firstRay.name = $"{firstRay.name} [RAY]";
        //     _ray.RayBehaviour.SetTarget(_target);
        // }
        //
        // public void SetFirstPlayer(L_Player _player)
        // {
        //     if (!_player) return;
        //     firstPlayer = _player;
        //     firstPlayer.name += " [PLAYER]";
        // }
    }
}