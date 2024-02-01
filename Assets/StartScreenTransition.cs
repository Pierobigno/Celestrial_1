using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenTransition : MonoBehaviour
{
    private Animator blackFade;
    public float tempo;

    public void Start()
    {
        StartCoroutine(BlackFadeOff(tempo));
    }

    IEnumerator BlackFadeOff(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        blackFade = GameObject.FindGameObjectWithTag("BlackFade").GetComponent<Animator>();
        blackFade.SetTrigger("BlackFadeOff");
    }
}
