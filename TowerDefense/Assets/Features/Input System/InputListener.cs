using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    private InputActions _inputActions;

    public static event Action OnPauseToggled;
    public static event Action OnConstructionToggled;
    public static event Action OnSelected;

    private void Awake()
    {
        _inputActions = new InputActions();

        _inputActions.MainActionMap.TogglePause.performed += OnTogglePause;
        _inputActions.MainActionMap.ToggleConstrucrion.performed += OnToggleConstruction;
        _inputActions.MainActionMap.Select.performed += OnSelect;
    }

    private void OnTogglePause(InputAction.CallbackContext callbackContext)
    {
        OnPauseToggled?.Invoke();
    }

    private void OnToggleConstruction(InputAction.CallbackContext callbackContext)
    {
        OnConstructionToggled?.Invoke();
    }

    private void OnSelect(InputAction.CallbackContext callbackContext)
    {
        OnSelected?.Invoke();
    }

    private void OnEnable()
    {
        _inputActions.MainActionMap.Enable();
    }

    private void OnDisable()
    {
        _inputActions.MainActionMap.Disable();
    }
}
