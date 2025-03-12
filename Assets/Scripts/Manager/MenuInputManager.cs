using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{
    public event Action OnNextMenuButtonPressed;
    public event Action OnPreviousMenuButtonPressed;

    [Header("Input Actions")]
    [SerializeField] private InputAction nextMenuAction;
    [SerializeField] private InputAction previousMenuAction;

    private void OnEnable()
    {
        nextMenuAction.Enable();
        previousMenuAction.Enable();

        nextMenuAction.performed += _ => MoveToNextMenu();
        previousMenuAction.performed += _ => MoveToPreviousMenu();
    }

    private void OnDisable()
    {
        nextMenuAction.performed -= _ => MoveToNextMenu();
        previousMenuAction.performed -= _ => MoveToPreviousMenu();

        nextMenuAction.Disable();
        previousMenuAction.Disable();
    }

    private void MoveToNextMenu()
    {
        OnNextMenuButtonPressed?.Invoke();
    }

    private void MoveToPreviousMenu()
    {
        OnPreviousMenuButtonPressed?.Invoke();
    }
}
