using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class FightEngaged : MonoBehaviour
{
    private Animator fightTransitionAnimator;
    private InfosBox infosBox;
    public GameObject fightTransition;
    public bool fightEngaged;
    public int fightMusicIndex;

    void Awake()
    {
        fightTransitionAnimator = fightTransition.GetComponent<Animator>();
        infosBox = FindObjectOfType<InfosBox>();
    }

    public void OnEngageFight()
    {
        if(!fightEngaged)
        {
            StartCoroutine(EngageFight());
        }

        else if(fightEngaged)
        {
            StartCoroutine(DisengageFight());
        }
    }

    public IEnumerator EngageFight()
    {
        fightEngaged = true;
        fightTransition.SetActive(true);
        FindObjectOfType<AudioManager>().StopMusic();
        fightTransitionAnimator.SetTrigger("FightEngaged");
        yield return new WaitForSeconds(0.8f);
        FindObjectOfType<AudioManager>().PlayMusic(fightMusicIndex);
    }

public IEnumerator DisengageFight()
{
    fightEngaged = false;
    
    // Réduire graduellement le volume de la musique
    AudioManager audioManager = FindObjectOfType<AudioManager>();
    float fadeDuration = 2f;
    float startVolume = audioManager.musicSource.volume;
    float endVolume = 0f;
    float elapsedTime = 0f;
    while (elapsedTime < fadeDuration)
    {
        audioManager.musicSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / fadeDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    audioManager.StopMusic();
    audioManager.PlayMusic(0);
    fightTransition.SetActive(true);
}

}
