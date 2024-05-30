using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditiveSceneOnCollision2D : MonoBehaviour
{
    public string sceneName; // Nombre de la escena aditiva
    private bool isSceneLoaded = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isSceneLoaded)
        {
            LoadAdditiveScene();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isSceneLoaded)
        {
            UnloadAdditiveScene();
        }
    }

    void LoadAdditiveScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        isSceneLoaded = true;
    }

    void UnloadAdditiveScene()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        isSceneLoaded = false;
    }
}
