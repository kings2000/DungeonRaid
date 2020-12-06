using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnActionBtnPressed(Enums.Action_ID action);

public class ActionInputController : SinglitonObjects<ActionInputController>
{

    public ActionButtonItem mainButton;
    private ActionButtonItem[] actionButoons;

    public Button exitBtn;

    public ArcUiPlacer actionButtonHolder;

    CharacterProps currentProps;

    private void Start()
    {
        exitBtn.onClick.AddListener(delegate { UnityEngine.SceneManagement.SceneManager.LoadScene(0); });
    }

    public void SetupControlls(CharacterProps props)
    {
        currentProps = props;
        actionButtonHolder.ClearItems();
        for (int i = 0; i < props.playerActions.Length; i++)
        {
            if(props.playerActions[i].action_ID == Enums.Action_ID.Default)
            {
                if(mainButton != null)
                {
                    
                    mainButton.Setup(OnActionPressed, props.playerActions[i]);
                }
            }
            else
            {
                ActionButtonItem item = FactoryManager.Instance.uiItemFactory.GetItem(FactoryManager.Instance.uiItemFactory.actionButtonItem);
                actionButtonHolder.AddItem(item.gameObject);
                item.Setup(OnActionPressed, props.playerActions[i]);
            }
        }
        actionButtonHolder.UpdatePositions();
    }
    private void Update()
    {
        if(currentProps != null)
        {
            for (int i = 0; i < currentProps.playerActions.Length; i++)
            {
                if (Input.GetKey(currentProps.playerActions[i].keyCode))
                {
                    OnActionPressed(currentProps.playerActions[i].action_ID);
                }
            }
        }
    }


    public void OnActionPressed(Enums.Action_ID action)
    {
        
        PlayerController.Instance.OnActionButtonPressed(action);
    }
}
