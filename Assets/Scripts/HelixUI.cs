using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using MyBox;
using TMPro;

public class HelixUI : MonoBehaviour
{
    [Header("Echoes")]
    public List<GameObject> menuItems;
    public List<CanvasGroup> menuItemsCanvasGroups = new List<CanvasGroup>();
    public int selectedItemIndex = 0;

    [Header("Helix Settings")]
    public float radius = 300f;  // Radius of the helix
    public float elementSpacing = 30f;  // Spacing between echoes
    public int range = 5; //How many echoes are shown at one time
    public float spiralFactor = 20f; //How much the spiral bends
    
    [Header("Input")]
    public InputActionProperty navigateAction;
    private EventSystem eventSystem;

    [Header("UI")]
    [SerializeField] private TMP_Text echoLabel;

    [Header("Juice")] 
    public float smallestSize = 0.8f;
    public float largestSize = 1.2f;
    public float alphaMultipier = 0.1f;

    private void OnValidate()
    {
        ArrangeItemsInHelix();
    }

    void Start()
    {
        foreach (var t in menuItems)
        {
            menuItemsCanvasGroups.Add(t.GetComponent<CanvasGroup>());
        }
        
        eventSystem = EventSystem.current;
        
        ArrangeItemsInHelix();
        
        HighlightItem(selectedItemIndex);
        
        navigateAction.action.Enable();
    }

    void Update()
    {
        //Read the data from the analogue stick
        Vector2 input = navigateAction.action.ReadValue<Vector2>();

        if (input.magnitude > 0.5f)  // Ensure input is strong enough to avoid jitter
        {
            //Normalise it
            Vector3 inputDir = new Vector3(input.x, input.y, 0).normalized;

            //Pick the object nearest to our current direction, within the range
            int bestIndex = selectedItemIndex;
            float bestDot = -1f;
            
            for (int i = -range; i <= range; i++)
            {
                int checkIndex = (selectedItemIndex + i + menuItems.Count) % menuItems.Count;
            
                if (!menuItems[checkIndex].activeSelf) continue; // Skip hidden elements

                Vector3 itemDir = menuItems[checkIndex].transform.localPosition.normalized;
                float dot = Vector3.Dot(inputDir, itemDir);

                if (dot > bestDot)
                {
                    bestDot = dot;
                    bestIndex = checkIndex;
                }
            }
            
            if (bestIndex != selectedItemIndex)
            {
                selectedItemIndex = bestIndex;
                HighlightItem(selectedItemIndex);
            }
        }

        //Recalculate the helix
        ArrangeItemsInHelix();
    }

    void HighlightItem(int index)
    {
        selectedItemIndex = index; // Keep track of selected item
    
        for (int i = 0; i < menuItems.Count; i++)
        {
            // Show items within the visible range
            bool isVisible =
                Mathf.Abs(i - index) <= range ||
                Mathf.Abs(i - (index + menuItems.Count)) <= range ||
                Mathf.Abs(i - (index - menuItems.Count)) <= range;

            menuItems[i].SetActive(isVisible);
        
            int distance = i - index;

            // Handle wrap-around for circular menu
            if (distance > menuItems.Count / 2)
            {
                distance -= menuItems.Count;
            }
            else if (distance < -menuItems.Count / 2)
            {
                distance += menuItems.Count;
            }
        
            // Adjust scale: items above are larger, below are smaller
            float depthFactor = Mathf.InverseLerp(-menuItems.Count / 2, menuItems.Count / 2, distance);
            float scale = Mathf.Lerp(smallestSize, largestSize, depthFactor);
            menuItems[i].transform.localScale = Vector3.one * scale;

            // Adjust fade: items above are opaque, items below fade out
            float fadeFactor = 0;
            if (distance >= 0) 
            {
                // Items above (including selected) should be fully opaque
                fadeFactor = 0;
            } 
            else 
            {
                // Items below should fade out within the visible range
                fadeFactor = Mathf.InverseLerp(-1, -range, distance);
            }

            // Apply fade effect
            menuItemsCanvasGroups[i].alpha = Mathf.Clamp01(1 - fadeFactor) * alphaMultipier;
        }
    
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(menuItems[index]);
            echoLabel.text = eventSystem.currentSelectedGameObject.name;
        }
    }
    
    void ArrangeItemsInHelix()
    {
        float totalItems = menuItems.Count;
        float angleIncrement = elementSpacing / radius;

        for (int i = 0; i < totalItems; i++)
        {
            float angle = i * angleIncrement;

            // Calculate offset considering circular wrapping
            int offsetFromSelected = i - selectedItemIndex;

            // Wrap around the offset for smooth transitions
            if (offsetFromSelected > totalItems / 2)
            {
                offsetFromSelected -= (int)totalItems;
            }
            else if (offsetFromSelected < -totalItems / 2)
            {
                offsetFromSelected += (int)totalItems;
            }

            // Adjust radius based on the wrapped offset
            float adjustedRadius = radius + offsetFromSelected * spiralFactor;

            // Calculate positions using the adjusted radius
            float x = Mathf.Cos(angle) * adjustedRadius;
            float y = Mathf.Sin(angle) * adjustedRadius;

            // Apply the new position
            menuItems[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }
    private void OnDisable()
    {
        navigateAction.action.Disable();
    }
}