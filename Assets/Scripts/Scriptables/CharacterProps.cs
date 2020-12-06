using UnityEngine;
using System.Collections;
using Enums;

[CreateAssetMenu(menuName = Constants.EDITOR_MENU_PREFIX + "/Props/" + nameof(CharacterProps))]
public class CharacterProps : ScriptableObject
{
    public float characterSpeed;
    public float initialAttackSpeed;
    public Actions[] playerActions;


    [System.Serializable]
    public struct Actions
    {
        public Sprite actionImage;
        public Action_ID action_ID;
        public KeyCode keyCode;
    }
    
}
