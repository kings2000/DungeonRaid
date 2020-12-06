using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SinglitonObjects<PlayerController>
{

    CharacterMovementController characterMovement;
    public CharacterControllerBase currentCharacter;

    public CharacterControllerBase[] controllableCharacters;

    Joystick joystick;
    private bool pauseControlls = true;

    private void Start()
    {
        Invoke(nameof(Init), .5f);
        //Init();
    }

    public void Init()
    {
        pauseControlls = true;
        joystick = FindObjectOfType<Joystick>();
        OnChangeCharacter(currentCharacter);
    }

    public void OnChangeCharacter(int index)
    {

        for (int i = 0; i < controllableCharacters.Length; i++)
        {
            if (index == i) continue;
            controllableCharacters[i].GetComponent<AiController>().ActivateAiAgent(true, controllableCharacters[index]);
        }
        SetCurrentCharacter(controllableCharacters[index]);
    }

    public void OnChangeCharacter(CharacterControllerBase _character)
    {

        for (int i = 0; i < controllableCharacters.Length; i++)
        {
            if (controllableCharacters[i] == _character) continue;
            controllableCharacters[i].GetComponent<AiController>().ActivateAiAgent(true, _character);
        }
        SetCurrentCharacter(_character);
    }

    public void SetCurrentCharacter(CharacterControllerBase _character)
    {
        if (_character == null) return;
        if(characterMovement != null)
        {
            characterMovement.Move(Vector3.zero);
            characterMovement.GetComponent<AiController>().ActivateAiAgent(true, _character);
        }

        pauseControlls = true;
        currentCharacter = _character;
        CameraController.Instance.SetCurrentTarget(_character.transform);
        characterMovement = _character.GetComponent<CharacterMovementController>();
        characterMovement.GetComponent<AiController>().ActivateAiAgent(false, _character);
        ActionInputController.Instance.SetupControlls(_character.props);

        pauseControlls = false;
    }

    void Update()
    {
        if (!pauseControlls)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (joystick.direction != Vector2.zero)
            {
                direction = new Vector3(joystick.direction.x, 0, joystick.direction.y);
            }
            characterMovement.Move(direction);
        }
        
    }

    public void OnActionButtonPressed(Enums.Action_ID action_ID)
    {
        if (pauseControlls) return;

        currentCharacter.ProcessAction(action_ID);
    }

}
