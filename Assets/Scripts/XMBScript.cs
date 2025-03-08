using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using MyBox;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

public class XMBScript : MonoBehaviour
{
    public static XMBScript Instance { get; private set; }
    
    [Header("Components")]
    [SerializeField] private RectTransform horizontalGrid;
    [SerializeField] private Transform parentWithEchoes;
    [SerializeField] private TMP_Text echoLabel;
    
    [Header("States")]
    private bool movingUI = false;
    public bool isHoldingLeft = false;
    public bool isHoldingRight = false;
    public bool isHoldingUp = false;
    public bool isHoldingDown = false;
    
    [Header("Echoes")]
    [SerializeField] public int echoesCount;
    [SerializeField] public int pointingAt = 0;

    [Header("Animation")]
    [SerializeField] private float moveAmount = 190;
    [SerializeField] private float moveSpeed = 0.05f;

    private void Awake()
    {
        Instance = this;
    }

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
            if (moveInput.y < 0)
                MoveDown();
            else if (moveInput.y > 0)
                MoveUp();
        }
    }

    public void HoldDirection(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (context.performed) // When key is pressed
        {
            if (moveInput.x < 0)
            {
                isHoldingLeft = true;
            }
            else if (moveInput.x > 0)
            {
                isHoldingRight = true;
            }
  
            if (moveInput.y < 0)
                isHoldingDown = true;
            else if (moveInput.y > 0)
                isHoldingUp = true;
            
        }
        else if (context.canceled) // When key is released
        {
            isHoldingLeft = false;
            isHoldingRight = false;
            isHoldingUp = false;
            isHoldingDown = false;
        }
    }

    private void Update()
    {
        if (!movingUI)
        {
            if (isHoldingLeft)
            {
                MoveLeft();
            }
            else if (isHoldingRight)
            {
                MoveRight();
            } else if (isHoldingUp)
            {
                //MoveUp();
            } else if (isHoldingDown)
            {
                //MoveDown();
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
            MoveSelection(MoveDirection.Left);
        }
        
        movingUI = true;
        ShiftHorizontalBar(numberToShift);
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
            MoveSelection(MoveDirection.Right);
        }
      
        movingUI = true;
        ShiftHorizontalBar(numberToShift);
    }

    private void ShiftHorizontalBar(int number)
    {
        float amountToShift = moveAmount * number;
        Vector2 currentPos = horizontalGrid.anchoredPosition;
        horizontalGrid.DOLocalMoveX(currentPos.x - amountToShift,moveSpeed).OnComplete(FinishedMoving);
    }

    private void MoveUp()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.parent.CompareTag("Vertical"))
        {
            var shifter = EventSystem.current.currentSelectedGameObject.transform.parent.parent
                .GetComponent<VerticalShifter>();

            if (shifter.CanMoveUp())
            {
                movingUI = true;
                MoveSelection(MoveDirection.Up);
                shifter.ShiftUp();
            }
            else
            {
                movingUI = true;
                shifter.Wraparound(true);
            }
        }
    }

    private void MoveDown()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.parent.CompareTag("Vertical"))
        {
            var shifter = EventSystem.current.currentSelectedGameObject.transform.parent.parent
                .GetComponent<VerticalShifter>();
            
            if (shifter.CanMoveDown())
            {
                movingUI = true;
                MoveSelection(MoveDirection.Down);
                shifter.ShiftDown();
            }
            else
            {
                movingUI = true;
                shifter.Wraparound(false);
            }
        }
    }

    public void FinishedMoving()
    {
        movingUI = false;
        echoLabel.text = EventSystem.current.currentSelectedGameObject.name;
    }
    
    private void MoveSelection(MoveDirection direction)
    {
        if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null)
            return;

        GameObject current = EventSystem.current.currentSelectedGameObject;
        AxisEventData axisEventData = new AxisEventData(EventSystem.current)
        {
            moveDir = direction
        };

        ExecuteEvents.Execute(current, axisEventData, ExecuteEvents.moveHandler);
    }
}
