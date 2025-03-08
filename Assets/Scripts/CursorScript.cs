using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private GameObject cursorGo;
    
    void Update()
    {
        FollowCurrentSelected();
    }

    public void FollowCurrentSelected()
    {
        //Matches the position of the cursor to the currently selected game object
        //Used in the radial, spiral, and grid demos
        
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            transform.position = new Vector3(EventSystem.current.currentSelectedGameObject.transform.position.x,
                EventSystem.current.currentSelectedGameObject.transform.position.y, 0);
        }
    }
    
    public void HideCursor()
    {
        cursorGo.SetActive(false);   
    }
    
    public void ShowCursor()
    {
        FollowCurrentSelected();
        cursorGo.SetActive(true);   
    }
}
