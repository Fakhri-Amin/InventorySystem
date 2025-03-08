using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TabUIScript : MonoBehaviour
{

    [SerializeField] private List<Image> tabImages = new List<Image>();
    [SerializeField] private List<UIOutline> tabOutlines = new List<UIOutline>();

    public void DisableTab(int tabID, float duration)
    {
        tabImages[tabID].DOFade(0, duration);
        tabOutlines[tabID].DOFade(1, duration);
    }
    
    public void EnableTab(int tabID, float duration)
    {
        tabImages[tabID].DOFade(1, duration);
        tabOutlines[tabID].DOFade(0, duration);
    }
}
