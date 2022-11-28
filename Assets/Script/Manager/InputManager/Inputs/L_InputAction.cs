using UnityEngine;

[System.Serializable]
public class L_InputAction : L_Input<bool>
{
    [SerializeField] KeyCode keyCode = KeyCode.None;
    [SerializeField] bool inputStatus = false;
    [SerializeField] EButtonState buttonState = EButtonState.NONE;
    [SerializeField] EActionEvent actionEvent = EActionEvent.NONE;

    public bool InputStatus => inputStatus;
    public EButtonState ButtonState => buttonState;
    public EActionEvent ActionEvent => actionEvent;

    public override bool GetInputEvent
    {
        get
        {
            if (buttonState == EButtonState.PRESSED)
                return inputStatus = Input.GetKeyDown(keyCode);
            else if (buttonState == EButtonState.RELEASED)
                return inputStatus = Input.GetKeyUp(keyCode);
            return inputStatus = Input.GetKey(keyCode);
        }
    }
}
