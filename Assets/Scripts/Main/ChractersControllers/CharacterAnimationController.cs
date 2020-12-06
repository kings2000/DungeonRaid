using UnityEngine;
using System.Collections;
using System;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator characterAnimation;
    public CharacterAnimationFactory animationFactory;
    private CharacterMovementController movementController;
    private CharacterControllerBase controllerBase;

    private Coroutine stateTransition;
    private Enums.Anim_ID_Map currentAnimation = Enums.Anim_ID_Map.Idle;

    private void Start()
    {
        controllerBase = GetComponent<CharacterControllerBase>();
        movementController = GetComponent<CharacterMovementController>();
    }

    public float GetAnimationLenght()
    {
        
        return 1;
    }

    public void ProcessLocomotionAnimation(Enums.Anim_ID_Map anim_ID)
    {
        if (currentAnimation == anim_ID) return;
        
        if (anim_ID != Enums.Anim_ID_Map.Idle && anim_ID != Enums.Anim_ID_Map.Walk) return;
        
        CharacterAnimationFactory.AnimationSet? animation = animationFactory.GetAnimationSet(anim_ID);
        currentAnimation = anim_ID;
        
        if (animation != null)
        {
            characterAnimation.speed = 1;
            characterAnimation.CrossFadeInFixedTime(animation.Value.clip.name, 0.01f);
        }
    }

    public void ProcessNonLocomotionAnimation(Enums.Anim_ID_Map anim_ID, Action<float> callback)
    {
        movementController.LockLocomotionAnimation(); //temporary lock the movement so we can extract animation state for better unlocking
          
        CharacterAnimationFactory.AnimationSet? animation = animationFactory.GetAnimationSet(anim_ID);
        currentAnimation = anim_ID;
        
        if (animation != null)
        {
            characterAnimation.CrossFadeInFixedTime(animation.Value.clip.name, 0.01f);
            characterAnimation.speed = 1;
            callback?.Invoke(animation.Value.clip.length/characterAnimation.speed); // devide through by the speed of the animation
        }
    }

    IEnumerator _ProcessNonLocomotionAnimation(string clipName, Action<float> callback)
    {
        
        yield return new WaitForSeconds(.01f);
       // AnimatorClipInfo[] clipInfo =  characterAnimation.GetCurrentAnimatorClipInfo(0);
        AnimatorStateInfo info = characterAnimation.GetCurrentAnimatorStateInfo(0);
        print(info.length);
        callback?.Invoke(info.length);
    }
}
