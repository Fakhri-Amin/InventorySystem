using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollBarScript : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RectTransform scrollBarCircle;
    [SerializeField] private RectTransform scrollBarLine;
    
    [Header("Helix Demo")]
    [SerializeField] private bool useHelix = false;
    [SerializeField] private Transform echoList;
    
    [Header("Acceleration Demo")]
    [SerializeField] private HorizontalManager horizontalManager;

    private float normalizedPosition;
    
    void Update()
    {
        float lineWidth = scrollBarLine.rect.width;
        
        // Calculate the normalized position (0 to 1)
        if (useHelix)
        {
            normalizedPosition = (float)EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex() /
                                 echoList.childCount;
        }
        else
        {
            normalizedPosition = (float)horizontalManager.pointingAt / horizontalManager.echoesCount;
        }
        
        float targetX = normalizedPosition * lineWidth;
        
        Vector2 newPos = scrollBarCircle.anchoredPosition;
        newPos.x = targetX;
        scrollBarCircle.anchoredPosition = newPos;
    }
}
