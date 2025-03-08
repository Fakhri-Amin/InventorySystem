using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using MyBox;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

public class HorizontalManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform horizontalGrid;
    [SerializeField] private Transform parentWithEchoes;
    [SerializeField] private TMP_Text echoLabel;
    
    [Header("Animation")]
    [SerializeField] private float moveAmount = 190;
    [SerializeField] private float moveSpeed = 0.05f;

    [Header("Acceleration")] 
    [SerializeField] private bool useAcceleration = true;
    [SerializeField] private float acceleration = 0;
    [SerializeField] private float accelerationSpeedUp = 0.1f;
    [SerializeField] private float accelerationUpperClamp = 0.04f;
    private float acceleratedMoveSpeed = 0;
    
    [Header("States")]
    private bool movingUI = false;
    private bool isHoldingLeft = false;
    private bool isHoldingRight = false;
    
    [Header("Echoes")]
    [ReadOnly] public int echoesCount;
    [ReadOnly] public int pointingAt = 0;
    
    void Start()
    {
        echoesCount = parentWithEchoes.childCount;
    }
    
    public void PressDirection(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        
        if (context.performed && !movingUI)
        {
            if (moveInput.x < 0)
                MoveLeft();
            else if (moveInput.x > 0)
                MoveRight();
        }
    }

    public void HoldDirection(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (context.performed)
        {
            if (moveInput.x < 0)
            {
                isHoldingLeft = true;
            }
            else if (moveInput.x > 0)
            {
                isHoldingRight = true;
            }
        }
        else if (context.canceled)
        {
            isHoldingLeft = false;
            isHoldingRight = false;
            acceleration = 0;
        }
    }

    private void Update()
    {
        if (!movingUI)
        {
            if (isHoldingLeft)
            {
                MoveLeft();
                acceleration += accelerationSpeedUp;
            }
            else if (isHoldingRight)
            {
                MoveRight();
                acceleration += accelerationSpeedUp;
            }
        }
    }
    private void MoveLeft()
    {
        int numberToShift = -1;
        
        if (pointingAt == 0) // Hit Left Limit
        {
            int finalEcho = parentWithEchoes.childCount - 1;

            numberToShift = finalEcho;
            pointingAt = finalEcho;
            EventSystem.current.SetSelectedGameObject((parentWithEchoes.GetChild(finalEcho).gameObject));
        }
        else
        {
            pointingAt--;
            EventSystem.current.SetSelectedGameObject(parentWithEchoes.GetChild(pointingAt).gameObject);
        }
        
        movingUI = true;
        ShiftHorizontalBar(numberToShift);
        SFXPlayer.Instance.PlaySFX();
    }

    private void MoveRight()
    {
        int numberToShift = 1;

        if (pointingAt == echoesCount - 1)
        {
            numberToShift = -(parentWithEchoes.childCount - 1);
            pointingAt = 0;
            EventSystem.current.SetSelectedGameObject(parentWithEchoes.GetChild(0).gameObject);
        }
        else
        {
            pointingAt++;
            EventSystem.current.SetSelectedGameObject(parentWithEchoes.GetChild(pointingAt).gameObject);
        }
      
        movingUI = true;
        ShiftHorizontalBar(numberToShift);
        SFXPlayer.Instance.PlaySFX();
    }

    private void ShiftHorizontalBar(int number)
    {
        if (useAcceleration)
        {
            acceleratedMoveSpeed = Mathf.Clamp(moveSpeed / acceleration, accelerationUpperClamp, moveSpeed);
        }
        else
        {
            acceleratedMoveSpeed = moveSpeed;
        }
        
        float amountToShift = moveAmount * number;
        Vector2 currentPos = horizontalGrid.anchoredPosition;
        horizontalGrid.DOLocalMoveX(currentPos.x - amountToShift, acceleratedMoveSpeed).OnComplete(FinishedMoving);
    }

    private void FinishedMoving()
    {
        movingUI = false;
        echoLabel.text = EventSystem.current.currentSelectedGameObject.name;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}