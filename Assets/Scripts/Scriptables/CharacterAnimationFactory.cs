using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = Constants.EDITOR_MENU_PREFIX + "/Factories/" + nameof(CharacterAnimationFactory))]
public class CharacterAnimationFactory : ScriptableObject
{

    //public RuntimeAnimatorController animatorController;

    public AnimationSet[] characterAnimations;


    public AnimationSet? GetAnimationSet(Anim_ID_Map animationId)
    {
        return characterAnimations.Where(x => x.animationId == animationId).FirstOrDefault();
    }


    [System.Serializable]
    public struct AnimationSet
    {
        public AnimationClip clip;
        public Anim_ID_Map animationId;
    }

}
