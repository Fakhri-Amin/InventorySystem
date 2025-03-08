using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialLayoutGroup : MonoBehaviour
{
    [Header("Menu Settings")] public float radius = 100f; // Distance from center
    public float startAngle = 90f; // Where the first item starts (0 = right, 90 = top)
    public List<RectTransform> menuItems = new List<RectTransform>();
    public float elementSpacing = 30f; // Spacing between elements
    public float verticalSpacing = 30f;

    private void Update()
    {
        ArrangeItemsInCircle();
    }

    private void ArrangeItemsInCircle()
    {

        float totalItems = menuItems.Count;
        float angleIncrement = elementSpacing / radius; // Spacing between elements in terms of angle

        for (int i = 0; i < totalItems; i++)
        {
            float angle = i * angleIncrement; // Angle of current menu item
            float z = i * verticalSpacing; // Vertical offset (to make it coil)

            // Calculate the X and Y position based on the angle
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Apply the new position to the menu item
            menuItems[i].transform.localPosition = new Vector3(x, y, z);

        }
    }
}















