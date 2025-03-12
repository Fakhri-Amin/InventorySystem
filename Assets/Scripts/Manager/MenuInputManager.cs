using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{
    public event Action OnNextMenuButtonPressed;
    public event Action OnPreviousMenuButtonPressed;
    public event Action OnCloseMenuButtonPressed;

    [Header("Input Actions")]
    [SerializeField] private InputAction nextMenuAction;
    [SerializeField] private InputAction previousMenuAction;
    [SerializeField] private InputAction closeMenuAction;

    private void OnEnable()
    {
        nextMenuAction.Enable();
        previousMenuAction.Enable();
        closeMenuAction.Enable();

        nextMenuAction.performed += _ => MoveToNextMenu();
        previousMenuAction.performed += _ => MoveToPreviousMenu();
        closeMenuAction.performed += _ => CloseMenu();
    }

    private void OnDisable()
    {
        nextMenuAction.performed -= _ => MoveToNextMenu();
        previousMenuAction.performed -= _ => MoveToPreviousMenu();
        closeMenuAction.performed -= _ => CloseMenu();

        nextMenuAction.Disable();
        previousMenuAction.Disable();
        closeMenuAction.Disable();
    }

    private void MoveToNextMenu()
    {
        OnNextMenuButtonPressed?.Invoke();
    }

    private void MoveToPreviousMenu()
    {
        OnPreviousMenuButtonPressed?.Invoke();
    }

    private void CloseMenu()
    {
        OnCloseMenuButtonPressed?.Invoke();
    }
}
