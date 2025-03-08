using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private AudioSource loadSceneSFX;
    [SerializeField] private CanvasGroup mainGroup;
    
    [Header("Animations")]
    [SerializeField] private float fadeOutDuration = 0.5f;
    private bool loadingScene = false;

    public void LoadScene(string sceneName)
    {
        if (!loadingScene)
        {
            loadingScene = true;
            EventSystem.current.sendNavigationEvents = false;
            loadSceneSFX.Play();

            mainGroup.DOFade(0, fadeOutDuration).OnComplete(() =>
                SceneManager.LoadScene(sceneName)
            );
        }
    }
    
}
