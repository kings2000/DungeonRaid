using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{

    public MapButton[] mapButtons; 

    void Start()
    {
        for (int i = 0; i < mapButtons.Length; i++)
        {
            MapButton map = mapButtons[i];
            map.button.onClick.AddListener(delegate { OnMapButtonCliked(map); });
        }
    }

    
    public void OnMapButtonCliked(MapButton button)
    {
        //Make scene transfer here
        Debug.Log(button.locationId);

        SceneManager.LoadSceneAsync((int)button.locationId).completed += OnSceneLoaded;
    }

    void OnSceneLoaded(AsyncOperation op)
    {
        SceneManager.LoadSceneAsync((int)Enums.ScenesID.UIScene, LoadSceneMode.Additive);
    }

    [System.Serializable]
    public struct MapButton
    {
        public Button button;
        public Enums.ScenesID locationId;

    }
}
