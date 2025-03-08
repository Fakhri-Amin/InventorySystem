using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance { get; private set; }
    private AudioSource player;
    
    private void Awake()
    {
        Instance = this;
        player = GetComponent<AudioSource>();
    }

    public void PlaySFX()
    {
        player.Play();
    }
}
