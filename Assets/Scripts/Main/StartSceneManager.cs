using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{

    public Button button;
    public Button close;

    void Start()
    {
        button.onClick.AddListener(delegate { MoveToMainScene(); });
        close.onClick.AddListener(delegate {
            Application.Quit();
        });
    }

    void MoveToMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
