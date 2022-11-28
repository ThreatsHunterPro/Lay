using UnityEngine;

public class L_CameraTPS : L_CameraBehaviour
{
    protected override void MoveToTarget()
    {
        if (!Settings.CanMove) return;
        transform.position = GetPosition();
    }
    protected override Vector3 GetPosition()
    {
        return Vector3.MoveTowards(Settings.CurrentPosition, Settings.Offset.GetOffset(Settings.Target), Time.deltaTime * Settings.MoveSpeed * 0.5f);      
    }
    protected override void RotateToTarget()
    {
        if(!Settings.CanRotate) return;
        transform.rotation = GetRotation();
    }
    protected override Quaternion GetRotation()
    {
        Vector3 _direction = Settings.TargetPosition - Settings.CurrentPosition;
        if(_direction == Vector3.zero)return Quaternion.identity;
        Quaternion _lookAt = Quaternion.LookRotation(_direction + Settings.LookAtOffset.GetLookAtOffset(Settings.Target));
        return Quaternion.RotateTowards(Settings.CurrentRotation, _lookAt, Time.deltaTime * Settings.RotationSpeed * 5.0f);
    }
}