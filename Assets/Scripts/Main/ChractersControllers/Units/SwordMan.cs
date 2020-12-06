using UnityEngine;
using System.Collections;

public class SwordMan : CharacterControllerBase
{

    public float spiercingDisplacment = 20;
    public float spiercingSpeed = 20;
    public float spinSpeed = 5;
    public float spinTime = 3;
    public Transform body;

     
    Coroutine spinRoutine;

    private bool attacking;

    protected override void Start()
    {
        base.Start();
    }

    public override void ProcessAction(Enums.Action_ID actionId)
    {
        if (!canAction) return;
        base.ProcessAction(actionId);
        switch (actionId)
        {
            case Enums.Action_ID.Default:
                //PerformDefaultActtack();
                break;
            case Enums.Action_ID.ID_00:
                PerformSwordPiercing();
                break;
            case Enums.Action_ID.ID_01:
                PerformSpinning();
                break;
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        AttackLoop();
    }

    #region Action1

    protected override void PerformDefaultActtack(bool loop)
    {

        base.PerformDefaultActtack(loop);
        if (loop)
        {
            entityState = Enums.EntityState.Action;
        }
        else
        {

            if (canAction)
            {
                //Lock(true, true, true, true, 0, characterBuffController.GetChearacterAttackSpeed());
                Attack();
            }
        }
        
        
    }

    
    public void AttackLoop()
    {

        if (entityState == Enums.EntityState.Action)
        {
            if (canAction)
            {
                Attack();
            }
           
        }
        lastAttackTime += Time.deltaTime;
    }

    float lastAttackTime = float.MaxValue;
    
    public void Attack()
    {
        if(lastAttackTime >= characterBuffController.GetChearacterAttackSpeed())
        {
            //print("Hello");
            animationController.ProcessNonLocomotionAnimation(Enums.Anim_ID_Map.ID_00, (e) => {
                Lock(true, true, true, true, 0, e);
            });
            lastAttackTime = 0;
            movementController.UpdateRotationToTarget();

        }
        
    }

    IEnumerator DefaultAttackLoop()
    {
        animationController.ProcessNonLocomotionAnimation(Enums.Anim_ID_Map.ID_00, null);

        yield return null;
    }

    #endregion Action1

    #region Action2
    void PerformSwordPiercing()
    {
        Lock(true, true, true, true, 0, 6);
        StartCoroutine(Spiercing());
    }
    IEnumerator Spiercing()
    {

        Vector3 maxVelocity = transform.forward * spiercingDisplacment;
        Vector3 maxDistance = transform.position + maxVelocity;
        float dis = Vector3.Distance(transform.position, maxDistance);
        animationController.ProcessNonLocomotionAnimation(Enums.Anim_ID_Map.ID_01, (e) => {
            Lock(true, true, true, true, 0, e);
        });
        yield return new WaitForSeconds(.15f);
        while (dis > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, maxDistance, Time.deltaTime * spiercingSpeed);
            dis = Vector3.Distance(transform.position, maxDistance);
            yield return null;
        }


    }
    #endregion Action2


    #region Attack3

    public void PerformSpinning()
    {
        Lock(false, true, true, true, 0, 10);
        spinRoutine = StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        yield return new WaitForSeconds(.15f);
        animationController.ProcessNonLocomotionAnimation(Enums.Anim_ID_Map.ID_02, null);
        float s = 0;
        while (s <= spinTime)
        {
            s += Time.deltaTime;
            body.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            yield return null;
        }
        body.localEulerAngles = Vector3.zero;
        UnLock(false, true, true);
    }

    #endregion Attack3

}
