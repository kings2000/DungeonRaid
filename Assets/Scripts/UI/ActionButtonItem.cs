using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonItem : MonoBehaviour
{

    public Button button;
    public Image image;
    private Enums.Action_ID action;
    

    public void Setup(OnActionBtnPressed callback, CharacterProps.Actions _action)
    {
        
        action = _action.action_ID;
        image.sprite = _action.actionImage;
        print(button.name);
        button.onClick.AddListener(delegate {
            callback(action);
            
        });
    }
}
