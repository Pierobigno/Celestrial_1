using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    bool gameHasEnded = false;

    public float restartDelay = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame()
    {
    if (gameHasEnded == false)
    {
        gameHasEnded = true;
        Debug.Log("Game Over");
        Invoke("Restart", restartDelay);
        Restart();
           
        }
    }
        
    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Le joueur recommence");
    }  
}