using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class RadialMenu : MonoBehaviour
{
    [Header("External Components")] 
    [SerializeField] private RadialLayout radialScript;
    [SerializeField] private TMP_Text echoLabel;
    private EventSystem eventSystem;
    
    [Header("Input")]
    public InputActionProperty navigateAction;

    [Header("Other")]
    private float angleStep;
    private int selectedItemIndex = 0;
    
    void Start()
    {
        angleStep = 360f / radialScript.menuItems.Count;
        eventSystem = EventSystem.current;

        HighlightItem(selectedItemIndex);
        
        navigateAction.action.Enable(); 
    }

    void Update()
    {
        Vector2 input = navigateAction.action.ReadValue<Vector2>();
        
        if (input.magnitude > 0.5f)
        {
            float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            
            if (angle < 0)
            {
                angle += 360f; //Normalise angle
            }
            
            int newIndex = Mathf.RoundToInt(angle / angleStep) % radialScript.menuItems.Count;
            
            if (newIndex != selectedItemIndex)
            {
                selectedItemIndex = newIndex;
                HighlightItem(selectedItemIndex);
            }
        }
    }

    void HighlightItem(int index)
    {
        for (int i = 0; i < radialScript.menuItems.Count; i++)
        {
            radialScript.menuItems[i].transform.localScale = (i == index) ? Vector3.one * 1.2f : Vector3.one;
        }

        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(radialScript.menuItems[index].gameObject);
            echoLabel.text = eventSystem.currentSelectedGameObject.name;

        }
    }
    
    private void OnDisable()
    {
        navigateAction.action.Disable();
    }
}