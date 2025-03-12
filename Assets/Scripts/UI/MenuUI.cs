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

    [SerializeField] private List<MenuReference> menuReferences = new List<MenuReference>();

    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 0.2f;

    private void Awake()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        foreach (var item in menuReferences)
        {
            if (!item.Button) continue;

            item.TabUI = item.Button.GetComponent<MenuTabUI>();
            item.TabUI?.DeselectTabAtFirst();

            item.Button.onClick.AddListener(() => OpenMenu(item));
        }

        HideAllMenus();
    }

    private void OpenMenu(MenuReference selectedItem)
    {
        HideAllMenus();

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
}
