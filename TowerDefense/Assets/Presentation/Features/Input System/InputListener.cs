using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    private InputActions _inputActions;

    [SerializeField] private Construction construction;

    private void Awake()
    {
        _inputActions = new InputActions();

        _inputActions.MainActionMap.TogglePause.performed += OnTogglePause;
        _inputActions.MainActionMap.ToggleConstrucrion.performed += OnToggleConstruction;
        _inputActions.MainActionMap.Select.performed += OnSelect;
    }

    private void OnTogglePause(InputAction.CallbackContext callbackContext)
    {
        
    }

    private void OnToggleConstruction(InputAction.CallbackContext callbackContext)
    {
        construction.ToggleConstructionMode();
    }

    private void OnSelect(InputAction.CallbackContext callbackContext)
    {
        construction.SelectConstructionSite();
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
