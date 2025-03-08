using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }
    
    [Header("Components")]
    [SerializeField] private CanvasGroup mainGroup;
    [SerializeField] private AudioSource exitSFX;
    
    [Header("Animation")]
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] public bool transitioning = false;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        transitioning = true;
        mainGroup.alpha = 0;
        EventSystem.current.sendNavigationEvents = false;
        mainGroup.DOFade(1, fadeSpeed).OnComplete(TransitionInComplete);
    }

    void TransitionInComplete()
    {
        EventSystem.current.sendNavigationEvents = true;
        transitioning = false;
    }

    public void ReturnToTitle()
    {
        if (!transitioning)
        {
            exitSFX.Play();
            transitioning = true;
            EventSystem.current.sendNavigationEvents = false;
            mainGroup.DOFade(0, fadeSpeed).OnComplete(TransitionOutComplete);
        }
    }
    
    void TransitionOutComplete()
    {
        SceneManager.LoadScene(0);
    }
}
