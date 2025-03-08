using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridNavigation : MonoBehaviour
{
    [Header("Tab Management")]
    [SerializeField] private List<GameObject> tabs = new List<GameObject>();
    private List<CanvasGroup> tabCanvasGroups = new List<CanvasGroup>();
    private int currentTab = 0; 
    private int maximumTabs = 0;
    [SerializeField] private List<GameObject> firstObject = new List<GameObject>();
    private bool changingTabs;
    
    [Header("Input")]
    [SerializeField] InputActionProperty nextTab;
    
    [Header("Animation")]
    [SerializeField] private float shiftAmount = 20f;
    [SerializeField] private float fadeOutDuration = 0.2f;
    [SerializeField] private float fadeInDuration = 0.2f;

    [Header("External Components")]
    [SerializeField] private CursorScript theCursor;
    [SerializeField] private TabUIScript tabIcons;
    [SerializeField] private AudioSource tabSFX;
    
    void Start()
    {
        nextTab.action.Enable();
        maximumTabs = tabs.Count;

        foreach (var tab in tabs)
        {
            //Create a list of the canvas group objects from the tabs
            tabCanvasGroups.Add(tab.GetComponent<CanvasGroup>());
        }
    }
    
    void Update()
    {
        if (nextTab.action.WasPressedThisFrame())
        {
            NextTab();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstObject[currentTab]);
        }
    }
    
    void NextTab()
    {
        if (!changingTabs)
        {
            tabSFX.Play();
            tabIcons.DisableTab(currentTab, fadeOutDuration);
            theCursor.HideCursor();
            
            EventSystem.current.sendNavigationEvents = false;
            changingTabs = true;
            
            tabs[currentTab].transform.DOLocalMoveX(shiftAmount, fadeInDuration);
            tabCanvasGroups[currentTab].DOFade(0, fadeOutDuration).OnComplete(CompleteFadeOutTab);
        }
    }

    void CompleteFadeOutTab()
    {
        tabs[currentTab].SetActive(false);
        tabs[currentTab].transform.DOLocalMoveX(0, 0);
        
        if (currentTab < maximumTabs - 1)
        {
            currentTab++;
        }
        else
        {
            currentTab = 0;
        }

        tabCanvasGroups[currentTab].alpha = 0;
        tabs[currentTab].SetActive(true);
        
        tabCanvasGroups[currentTab].DOFade(1, fadeInDuration).OnComplete(CompleteFadeInTab);
    }

    void CompleteFadeInTab()
    {
        EventSystem.current.SetSelectedGameObject(firstObject[currentTab]);
   
        EventSystem.current.sendNavigationEvents = true;
        changingTabs = false;
        theCursor.ShowCursor();
        
        tabIcons.EnableTab(currentTab, fadeInDuration);
    }
    
    
    
    
    
    
    
    
    
    
}
