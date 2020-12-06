using UnityEngine;
using System.Collections;

public class EnityHealthBar : MonoBehaviour
{

    public bool isPlayerChearacter;
    CharacterControllerBase controllerBase;

    private void Start()
    {
        controllerBase = GetComponentInParent<CharacterControllerBase>();
        GetComponent<Canvas>().worldCamera = Camera.main;

    }


}
