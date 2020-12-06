using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour
{

    EnemiesBase enemiesBase;

    public Animator animator;
    private Enums.Anim_ID_Map currentAnimation;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        currentAnimation = Enums.Anim_ID_Map.Idle;
        enemiesBase = GetComponent<EnemiesBase>();
    }
    public void RunAnimation(Enums.Anim_ID_Map anim_ID)
    {
        switch (anim_ID)
        {
            case Enums.Anim_ID_Map.Idle:
            case Enums.Anim_ID_Map.Walk:
                ProcessLocomotionAnimation(anim_ID);
                break;
            case Enums.Anim_ID_Map.Die:
                break;
            case Enums.Anim_ID_Map.ID_00:
                ProcessLocomotionAnimation(anim_ID);
                break;
            
        }
    }
    
    public void ProcessLocomotionAnimation(Enums.Anim_ID_Map anim_ID)
    {
        if(currentAnimation != anim_ID)
        {
            currentAnimation = anim_ID;
            MonsterAnimationSet? animationSet = enemiesBase.properties.GetAnimationClip(anim_ID);
            if(animationSet != null)
            {
                animator.CrossFadeInFixedTime(animationSet.Value.clip.name, 0.001f);
            }
            
        }
        
    }

    public void ProcessNonLocomotionAnimation(Enums.Anim_ID_Map anim_ID)
    {
        MonsterAnimationSet? animationSet = enemiesBase.properties.GetAnimationClip(anim_ID);
        if (animationSet != null)
        {
           
            animator.CrossFadeInFixedTime(animationSet.Value.clip.name, 0.001f);
        }

    }
}
