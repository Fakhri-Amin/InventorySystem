using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class VerticalShifter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform verticalGrid;
    private int echoesCount;
    private int pointingAt = 0;

    [Header("Animation")]
    [SerializeField] private float moveAmount = 190;
    [SerializeField] private float moveSpeed = 0.1f;
    
    private void Start()
    {
        echoesCount = transform.GetChild(0).childCount;
    }

    public bool CanMoveUp()
    {
        if (pointingAt == 0) return false;
        else
        {
            return true;
        }
    }
    
    public bool CanMoveDown()
    {
        if (pointingAt == echoesCount - 1) return false;
        else
        {
            return true;
        }
    }

    public void ShiftUp()
    {
        if (pointingAt == 0) return;
        
        Vector2 currentPos = verticalGrid.localPosition;
        verticalGrid.DOLocalMoveY(currentPos.y - moveAmount, moveSpeed).OnComplete(FinishedMoving);
        pointingAt--;
    }

    public void ShiftDown()
    {
        if (pointingAt == echoesCount - 1) return;
        
        Vector2 currentPos = verticalGrid.localPosition;
        verticalGrid.DOLocalMoveY(currentPos.y + moveAmount, moveSpeed).OnComplete(FinishedMoving);
        pointingAt++;
    }

    void FinishedMoving()
    {
        XMBScript.Instance.FinishedMoving();
    }

    public void Wraparound(bool up)
    {
        if (up == true)
        {
            int bottomEcho = echoesCount - 1;
            Vector2 currentPos = verticalGrid.localPosition;
            float wrapAmount = moveAmount * bottomEcho;
            pointingAt = bottomEcho;
            verticalGrid.DOLocalMoveY(currentPos.y + wrapAmount, moveSpeed).OnComplete(FinishedMoving);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).GetChild(0).gameObject);
        }
        else
        {
            int topEcho = echoesCount - 1;
            Vector2 currentPos = verticalGrid.localPosition;
            float wrapAmount = moveAmount * topEcho;
            pointingAt = 0;
            verticalGrid.DOLocalMoveY(currentPos.y - wrapAmount, moveSpeed).OnComplete(FinishedMoving);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).GetChild(topEcho).gameObject);
        }
     
    }
}
