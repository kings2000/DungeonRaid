using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    void Awake()
    {
        Invoke(nameof(UpdateScene), .2f);
        //UpdateScene();
    }

    public void UpdateScene()
    {
        if (!SceneManager.GetSceneByBuildIndex(2).isLoaded)
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByBuildIndex(3).isLoaded)
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByBuildIndex(4).isLoaded)
            SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        
    }
}
