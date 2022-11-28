using System;
using UnityEngine;

[Serializable]
public class L_InputAxis : L_Input<float>
{
    [SerializeField] string axisName = "Default axis name";
    [SerializeField] float axisValue = 0.0f;
    [SerializeField] EAxisEvent axisEvent = EAxisEvent.NONE;
    [SerializeField] bool isActive = true;

    public void SetActive(bool _value) => isActive = _value;
    public float AxisValue => axisValue;
    public EAxisEvent AxisEvent => axisEvent;   
    public override float GetInputEvent
    {
        get
        {
            if (!isActive) return 0;
            try
            {
                return axisValue = Input.GetAxis(axisName);
            }
            catch (Exception _exception)
            {
                Debug.LogException(_exception);
                throw;
            }
        }
    }
}