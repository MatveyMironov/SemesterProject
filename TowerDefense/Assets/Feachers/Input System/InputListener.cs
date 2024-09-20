using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    private PlayerControls _playerControls;

    public static event Action OnLeftMouseClicked;
    public static event Action OnRightMouseClicked;
    public static event Action OnConstructionToggled;
    public static event Action OnPauseToggled;

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _playerControls.MainActionMap.LeftMouseClick.performed += OnLeftMouseClick;
        _playerControls.MainActionMap.RightMouseClick.performed += OnRightMouseClick;
        _playerControls.MainActionMap.ToggleConstruction.performed += OnConstructionToggleInput;
        _playerControls.MainActionMap.PauseAndResume.performed += OnPauseToggleInpyt;
    }

    private void OnLeftMouseClick(InputAction.CallbackContext context)
    {
        OnLeftMouseClicked?.Invoke();
    }

    private void OnRightMouseClick(InputAction.CallbackContext context)
    {
        OnRightMouseClicked?.Invoke();
    }

    private void OnConstructionToggleInput(InputAction.CallbackContext context)
    {
        OnConstructionToggled?.Invoke();
    }

    private void OnPauseToggleInpyt(InputAction.CallbackContext context)
    {
        OnPauseToggled?.Invoke();
    }

    private void OnEnable()
    {
        _playerControls.MainActionMap.Enable();
    }

    private void OnDisable()
    {
        _playerControls.MainActionMap.Disable();
    }
}
