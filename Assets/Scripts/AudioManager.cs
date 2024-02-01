using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    public AudioClip[] soundEffects;
    public AudioClip[] voiceEffects;

    [HideInInspector] public AudioSource musicSource;
    [HideInInspector] public AudioSource sfxSource;
    [HideInInspector] public AudioSource voiceSource;

    private void Awake()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        voiceSource = gameObject.AddComponent<AudioSource>();
        voiceSource.playOnAwake = false;

    }

    public void PlayMusic(int trackIndex)
    {
        musicSource.clip = musicTracks[trackIndex];
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySfx(int sfxIndex)
    {
        sfxSource.PlayOneShot(soundEffects[sfxIndex]);
    }

    public void PlayVoice(int voiceIndex)
    {
        voiceSource.PlayOneShot(soundEffects[voiceIndex]);
    }
}