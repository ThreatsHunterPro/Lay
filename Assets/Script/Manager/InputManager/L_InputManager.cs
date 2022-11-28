using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class L_InputManager : L_Singleton<L_InputManager>
{
    event Action OnInputsUpdated = null;

    [SerializeField] List<L_InputAxis> axes = new List<L_InputAxis>();
    [SerializeField] List<L_InputAction> actions = new List<L_InputAction>();

    private void Update() => OnInputsUpdated?.Invoke();
    private void OnDestroy()
    {
        OnInputsUpdated = null;
    }

    public void BindAxis(EAxisEvent _axisEvent, Action<float> _callback)
    {
        List<L_InputAxis> _axes = axes.Where((_axe) => _axe.AxisEvent == _axisEvent).ToList();
        _axes.ForEach((_axe) =>
        {
            _axe.SetActive(true);
            OnInputsUpdated += () => _callback.Invoke(_axe.GetInputEvent);
        });
    }
    public void UnBindAxis(EAxisEvent _axisEvent, Action<float> _callback)
    {
        List<L_InputAxis> _axes = axes.Where((_axe) => _axe.AxisEvent == _axisEvent).ToList();
        _axes.ForEach((_axe) =>
        {
            _axe.SetActive(false);
            OnInputsUpdated -= () => _callback.Invoke(_axe.GetInputEvent);
        });
    }
    public void BindAction(EButtonState _buttonState, EActionEvent _actionEvent, Action<bool> _callback)
    {
        List<L_InputAction> _actions = actions.Where((_action) => _action.ButtonState == _buttonState
                                                    && _action.ActionEvent == _actionEvent).ToList();
        _actions.ForEach((_action) => OnInputsUpdated += () => _callback.Invoke(_action.GetInputEvent));
    }
    public void UnBindAction(EButtonState _buttonState, EActionEvent _actionEvent, Action<bool> _callback)
    {
        List<L_InputAction> _actions = actions.Where((_action) => _action.ButtonState == _buttonState
                                                    && _action.ActionEvent == _actionEvent).ToList();
        _actions.ForEach((_action) => OnInputsUpdated -= () => _callback.Invoke(_action.GetInputEvent));
    }
}