using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterBuffController
{

    public float attackSpeedIncreamentPercent;
    public int healthIncreamnet;
    public float speedIncreament;


    CharacterControllerBase controllerBase;

    public CharacterBuffController(CharacterControllerBase characterControllerBase)
    {
        controllerBase = characterControllerBase;
    }

    public float GetChearacterAttackSpeed()
    {
        float speed = controllerBase.props.initialAttackSpeed * ((100 + attackSpeedIncreamentPercent) / 100);
        speed = 1 / speed;
        return speed;
    }

}
