using UnityEngine;
using System.Collections;

public class CrobsEnemy : EnemiesBase
{

    private float attackTime = 0;
    private bool attacking;
    private float attackSpeed;
  
    public override void Start()
    {
        base.Start();
        attacking = false;
        attackSpeed = 1f/ (float)properties.attackSpeed;
        
    }

    protected override void Update()
    {
        base.Update();
        if(enemyState == Enums.EntityState.Idle)
        {
            animationController.RunAnimation(Enums.Anim_ID_Map.Idle);

        }else if(enemyState == Enums.EntityState.Action)
        {
            if (!attacking)
            {
                attackTime = attackSpeed;
                attacking = true;
            }
            PlayAttack();
        }
        else if(enemyState == Enums.EntityState.Motion)
        {
            animationController.RunAnimation(Enums.Anim_ID_Map.Walk);
        }
    }


    public void PlayAttack()
    {
        if(attackTime > attackSpeed)
        {
            animationController.ProcessNonLocomotionAnimation(Enums.Anim_ID_Map.ID_00);
            attackTime = 0;
        }
        attackTime += Time.deltaTime;
    }
}
