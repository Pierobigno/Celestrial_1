using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    private AudioManager audioManager;
    public int eventMusicID;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic(eventMusicID);
    }
}
