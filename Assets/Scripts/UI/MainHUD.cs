using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private MenuInputManager menuInputManager;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            menuButton.gameObject.SetActive(false);
            menuUI.Show();
        });
    }

    void OnEnable()
    {
        menuInputManager.OnCloseMenuButtonPressed += OnCloseMenuButtonPressed;
    }

    void OnDisable()
    {
        menuInputManager.OnCloseMenuButtonPressed -= OnCloseMenuButtonPressed;
    }

    private void OnCloseMenuButtonPressed()
    {
        menuButton.gameObject.SetActive(true);
    }
}
