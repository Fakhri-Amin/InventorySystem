using MoreMountains.Feedbacks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private MMFeedbacks clickSound;
    [SerializeField] private MMFeedbacks hoverSound;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayClickSound()
    {
        clickSound.PlayFeedbacks();
    }

    public void PlayHoverSound()
    {
        hoverSound.PlayFeedbacks();
    }
}
