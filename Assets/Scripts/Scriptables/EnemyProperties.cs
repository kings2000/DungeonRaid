using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = Constants.EDITOR_MENU_PREFIX + "/Props/" + nameof(EnemyProperties))]
public class EnemyProperties : ScriptableObject
{
    public float attackSpeed;
    public float maxHealth;
    public float movementSpeed;
    public MonsterAnimationSet[] animationSet;


    public MonsterAnimationSet? GetAnimationClip(Enums.Anim_ID_Map anim_ID)
    {
        IDictionary<Enums.Anim_ID_Map, MonsterAnimationSet> set = animationSet.ToDictionary(x => x.anim_ID, x => x);
        if (set.ContainsKey(anim_ID))
        {
            return set[anim_ID];
        }
        return null;
    }
    
}

[System.Serializable]
public struct MonsterAnimationSet
{
    public Enums.Anim_ID_Map anim_ID;
    public AnimationClip clip;
}
