using UnityEngine;
using System.Collections;
using System;

public class CharacterControllerBase : LivingEntity
{
    
    
    public CharacterProps props;
    [Range(0, 50)]
    public float scanRadius = 1;
    [Range(0, 50)]
    public float searchRaduis = 1;
    [Range(0, 50)]
    public float maxRadiusForAiToAttakEnemy = 1;

    public CharacterBuffController characterBuffController;

    public Enums.EntityState entityState { get; set; }
    protected CharacterMovementController movementController;
    protected CharacterAnimationController animationController;
    protected bool canAction;
    

    private Coroutine LockRoutine;
    [HideInInspector]public LivingEntity currentPlayerTerget;

    protected virtual void Start()
    {
        characterBuffController = new CharacterBuffController(this);
        canAction = true;
        animationController = GetComponent<CharacterAnimationController>();
        movementController = GetComponent<CharacterMovementController>();
        movementController.m_movementSpeed = props.characterSpeed;
    }

   
    public virtual void ProcessAction(Enums.Action_ID actionId)
    {
       
        if (actionId == Enums.Action_ID.Default)
        {
            //find close target
            bool sucess = false;
            if (GetClosetObject(Constants.ENEMY_TAG, out GameObject target))
            {
                
                float dist = Vector3.Distance(target.transform.position, transform.position);
                
                if (dist <= searchRaduis)
                {
                    sucess = true;
                    currentPlayerTerget = target.GetComponent<LivingEntity>();
                    movementController.AutoMove(currentPlayerTerget, scanRadius);
                }
                    
                
            }
            
            if (!sucess)
            {
                PerformDefaultActtack(false);
            }
        }
    }

    protected virtual void PerformDefaultActtack(bool loop)
    {
        
    }

    protected virtual void LateUpdate()
    {
        
    }

    public void Lock(bool lockMovement, bool lockAction, bool lockLocomotionAnimation, bool timed, float delayTime, float lockTime)
    {
        if(LockRoutine != null)
            StopCoroutine(LockRoutine);
        LockRoutine = StartCoroutine(_Lock(lockMovement, lockAction, lockLocomotionAnimation, timed, delayTime, lockTime));
    }

    //Timed -1 = infinite, 0 = no, 1 = yes.
    public IEnumerator _Lock(bool lockMovement, bool lockAction, bool lockLocomotionAnimation, bool timed, float delayTime, float lockTime)
    {
        if (delayTime > 0)
        {
            yield return new WaitForSeconds(delayTime);
        }
        if (lockMovement)
        {
            movementController.LockMovement();
        }
        if (lockAction)
        {
            canAction = false;
        }
        if (timed)
        {
            if (lockTime > 0)
            {
                yield return new WaitForSeconds(lockTime);
            }
            UnLock(lockMovement, lockAction, lockLocomotionAnimation);
        }
    }

    public void UnLock(bool movement, bool actions, bool lockLocomotionAnimation)
    {
        if (movement)
        {
            movementController.UnlockMovement();
        }
        if (actions)
        {
            canAction = true;
        }
        if (lockLocomotionAnimation)
        {
            movementController.UnlockLocomotionAnimation();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GizmosManager.DrawCircle(transform.position + Vector3.up * .01f, scanRadius);
        Gizmos.color = Color.yellow;
        GizmosManager.DrawCircle(transform.position + Vector3.up * .01f, searchRaduis);
        Gizmos.color = Color.blue;
        GizmosManager.DrawCircle(transform.position + Vector3.up * .01f, maxRadiusForAiToAttakEnemy);
    }
}
