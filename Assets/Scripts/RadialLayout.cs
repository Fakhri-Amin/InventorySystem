using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class RadialLayout : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] float radius = 100f;
    [SerializeField] float startAngle = 0;
    [SerializeField] public List<RectTransform> menuItems = new List<RectTransform>();

    void Update()
    {
        ArrangeItemsInCircle();
    }

    void ArrangeItemsInCircle()
    {
        if (menuItems.Count == 0) return;

        float angleStep = 360f / menuItems.Count;

        for (int i = 0; i < menuItems.Count; i++)
        {
            float angle = startAngle + (angleStep * i);
            float radian = angle * Mathf.Deg2Rad;

            Vector2 position = new Vector2(
                Mathf.Cos(radian) * radius,
                Mathf.Sin(radian) * radius
            );

            menuItems[i].anchoredPosition = position;
        }
    }

    void OnValidate()
    {
        Update();
    }
    
    
}