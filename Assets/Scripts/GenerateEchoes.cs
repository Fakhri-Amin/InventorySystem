using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEchoes : MonoBehaviour
{
    [SerializeField] private GameObject echoPrefab;
    [SerializeField] private Transform echoList;
    [SerializeField] private List<Sprite> allEchoes = new List<Sprite>();

    [ButtonMethod]
    void GenerateEchoesObjects()
    {
        foreach (var sprite in allEchoes)
        {
            GameObject newEcho = Instantiate(echoPrefab, echoList);
            EchoScript newEchoScript = newEcho.GetComponent<EchoScript>();
            newEchoScript.GenerateEcho(sprite);
        }
    }
    
    
    
    
}
