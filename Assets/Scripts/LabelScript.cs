using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabelScript : MonoBehaviour
{
    [SerializeField] private TMP_Text labelText;
    
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            labelText.text = EventSystem.current.currentSelectedGameObject.name;
        }
    }
}
