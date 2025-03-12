using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [System.Serializable]
    public class MenuReference
    {
        public Button Button;
        public CanvasGroup MenuPage;
        [HideInInspector] public MenuTabUI TabUI;
    }

    [SerializeField] private MenuInputManager menuInputManager;
    [SerializeField] private List<MenuReference> menuReferences = new List<MenuReference>();

    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 0.2f;

    private int currentIndex = 0;

    private void Awake()
    {
        InitializeMenu();
    }

    void OnEnable()
    {
        menuInputManager.OnNextMenuButtonPressed += MoveToNextMenu;
        menuInputManager.OnPreviousMenuButtonPressed += MoveToPreviousMenu;
        menuInputManager.OnCloseMenuButtonPressed += CloseMenu;
    }

    void OnDisable()
    {
        menuInputManager.OnNextMenuButtonPressed -= MoveToNextMenu;
        menuInputManager.OnPreviousMenuButtonPressed -= MoveToPreviousMenu;
        menuInputManager.OnCloseMenuButtonPressed -= CloseMenu;
    }

    private void InitializeMenu()
    {
        for (int i = 0; i < menuReferences.Count; i++)
        {
            if (!menuReferences[i].Button) continue;

            menuReferences[i].TabUI = menuReferences[i].Button.GetComponent<MenuTabUI>();
            menuReferences[i].TabUI?.DeselectTabAtFirst();

            int index = i; // Capture index for correct referencing
            menuReferences[i].Button.onClick.AddListener(() => OpenMenu(index));
        }

        HideAllMenus();
        SelectFirstTab();
    }

    private void SelectFirstTab()
    {
        menuReferences[0].Button.GetComponent<MenuTabUI>().SelectTab(fadeInDuration);
        if (menuReferences[0].MenuPage)
        {
            menuReferences[0].MenuPage.gameObject.SetActive(true);
            menuReferences[0].MenuPage.DOFade(1, fadeInDuration);
        }
    }

    private void OpenMenu(int index)
    {
        if (index < 0 || index >= menuReferences.Count) return;

        HideAllMenus();
        currentIndex = index;

        var selectedItem = menuReferences[currentIndex];

        if (selectedItem.TabUI != null)
        {
            selectedItem.TabUI.SelectTab(fadeInDuration);
        }

        if (selectedItem.MenuPage != null)
        {
            selectedItem.MenuPage.gameObject.SetActive(true);
            selectedItem.MenuPage.DOFade(1, fadeInDuration);
        }
    }

    private void HideAllMenus()
    {
        foreach (var page in menuReferences)
        {
            if (page.MenuPage == null) continue;
            page.MenuPage.gameObject.SetActive(false);
            page.MenuPage.alpha = 0;
        }
    }

    private void MoveToNextMenu()
    {
        int nextIndex = (currentIndex + 1) % menuReferences.Count;
        OpenMenu(nextIndex);
    }

    private void MoveToPreviousMenu()
    {
        int prevIndex = (currentIndex - 1 + menuReferences.Count) % menuReferences.Count;
        OpenMenu(prevIndex);
    }

    private void CloseMenu()
    {
        Hide();
    }

    public void Show()
    {
        AudioManager.Instance.PlayClickSound();
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, fadeInDuration);
    }

    public void Hide()
    {
        AudioManager.Instance.PlayClickSound();
        GetComponent<CanvasGroup>().DOFade(0, fadeInDuration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
