using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float blackFadeOnTime = 1f;
    public Animator blackFade;

    public void LoadScene(string sceneToLoad)
    {
        StartCoroutine(LoadSceneCoroutine(sceneToLoad));
    }

    IEnumerator LoadSceneCoroutine(string sceneToLoad)
    {
        blackFade.SetTrigger("BlackFadeOn");
        yield return new WaitForSeconds(blackFadeOnTime);
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Nouvelle scene: " + sceneToLoad);
    }
}
