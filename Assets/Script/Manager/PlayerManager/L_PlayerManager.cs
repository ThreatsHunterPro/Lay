using UnityEngine;

public class L_PlayerManager : L_Singleton<L_PlayerManager>
{
    [SerializeField] L_Ray defaultRay = null;
    L_Player firstPlayer = null;
    L_Ray firstRay = null;

    public bool IsValidRay => defaultRay != null;

    public void SpawnRay(Transform _target, Vector3 _position)
    {
        if (!defaultRay || firstRay || !_target) return;
        L_Ray _ray = Instantiate(defaultRay, _position, _target.rotation);
        L_CameraManager.Instance?.CreateCamera<L_CameraTPS>(_ray.transform, defaultRay.Settings, "Ray Camera");
        firstRay = _ray;
        firstRay.name = $"{firstRay.name} [RAY]";
        _ray.RayBehaviour.SetTarget(_target);
    }
    public void SetFirstPlayer(L_Player _player)
    {
        if (!_player) return;
        firstPlayer = _player;
        firstPlayer.name = $"{firstPlayer.name} [PLAYER]";
    }
}