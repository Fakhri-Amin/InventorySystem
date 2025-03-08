using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EchoScript : MonoBehaviour
{
    [SerializeField] private Image myImage;
    [SerializeField] private TMP_Text myLabel;
    
    public void GenerateEcho(Sprite incomingSprite)
    {
        //Attaches the correct sprite and name to the newly created echo object
        myImage.sprite = incomingSprite;
        string modifiedString = incomingSprite.name.Replace('_', ' ');
        gameObject.name = modifiedString;
    }
}
