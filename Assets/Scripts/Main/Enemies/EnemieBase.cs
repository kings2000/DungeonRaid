using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemiesBase : LivingEntity
{

    public EnemyProperties properties;
    [Range(0, 50)]
    public float scanRadius = 1;
    public float restingResetTime = 1f;
    public float stopDistance = 3;
    public float m_rotationSpeed = 500;

    protected EnemyAnimationController animationController;

    protected Enums.EntityState enemyState;

    protected Transform currentTarget;
    //protected NavMeshAgent agent;
    
    protected bool canMove;
    protected bool canAction;
    protected bool hasTarget;
    protected bool goingBackToBase;
    protected bool resting;

    protected Coroutine lockRoutine;
    protected Vector3 originPosition;
    protected Vector3 targetPosition;
    protected Quaternion restRotation;

    public virtual void Start()
    {
        originPosition = transform.position;
        //agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<EnemyAnimationController>();

        StartCoroutine(UpdateMovemnet());
        //agent.stoppingDistance = stopDistance - 0.5f;
        //agent.speed = properties.movementSpeed;
        restRotation = transform.localRotation;
        //agent.updateRotation = false;
        canMove = true;
        canAction = true;
        resting = true;
        animationController.ProcessLocomotionAnimation(Enums.Anim_ID_Map.Idle);
        enemyState = Enums.EntityState.Idle;
        targetPosition = originPosition;

        GetComponentInParent<EnemyRegion>().SetAreaSize(scanRadius);
    }

    public virtual void UpdateTarget()
    {
        if(GetClosetObject(Constants.PLAYER_TAG, out GameObject closestTarget))
        {
            if(Vector3.Distance(closestTarget.transform.position, originPosition) <= scanRadius)
            {
                currentTarget = closestTarget.transform;
                hasTarget = true;
            }
            else
            {
                hasTarget = false;
            }
            
        }
        else
        {
            hasTarget = false;
        }
        
        
        //agent.updateRotation = !hasTarget;
    }

    public void Lock(bool movement, bool action,bool unlockWithDelay, float lockTime)
    {
        if (movement) canMove = false;
        if (action) canAction = false;

        if (unlockWithDelay)
        {
            if (lockRoutine != null) StopCoroutine(lockRoutine);
            lockRoutine = StartCoroutine(_Lock(movement, action, lockTime));
        }
        
    }

    protected virtual void LocalUpdate()
    {

    }

    protected virtual void Update()
    {
        if(canMove)
            MoveWithPath();

    }

    

    Vector3[] path = new Vector3[0];
    int currentPathIndex = 1;

    public void MoveWithPath()
    {
        if (path.Length > 0 && currentPathIndex < path.Length)
        {
            float dis = Vector3.Distance(path[currentPathIndex], transform.position);
            float stopDistanceCheck = Vector3.Distance(transform.position, targetPosition);
            float _stopDis = (hasTarget) ? stopDistance - .3f : .1f;
            if (stopDistanceCheck > _stopDis)
            {
                Vector3 dir = path[currentPathIndex] - transform.position;
                Quaternion rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex], properties.movementSpeed * Time.deltaTime);

            }
            else
            {

                if (hasTarget)
                {
                    Vector3 dir = targetPosition - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
                    enemyState = Enums.EntityState.Action;
                }
                    
            }

            if (!hasTarget && Vector3.Distance(transform.position, originPosition) <= _stopDis)
            {
                transform.rotation = restRotation;
                //resting = true;
                enemyState = Enums.EntityState.Idle;
                //yield return delay;
                Lock(true, true, true, 0.5f);
            }

            if (dis <= 0.1f)
            {
                currentPathIndex++;
            }
        }
        
    }


    public IEnumerator UpdateMovemnet()
    {
        WaitForSeconds delay = new WaitForSeconds(restingResetTime);
        
       
        while (true)
        {
            if (canMove)
            {
                
                if (hasTarget)
                {
                    
                    targetPosition = currentTarget.position;
                    if (Vector3.Distance(targetPosition, transform.position) > 0.02f)
                    {
                        path = GeneratePathPoints(targetPosition);
                        currentPathIndex = 1;
                        
                    }

                    enemyState = Enums.EntityState.Motion;
                    if (Vector3.Distance(transform.position, originPosition) > scanRadius)
                    {
                        hasTarget = false;
                        UpdateTarget();
                        if (!hasTarget)
                        {
                            goingBackToBase = true;
                        }
                    }
                }
                else
                {
                    if (!goingBackToBase)
                    {
                        UpdateTarget();
                        if (hasTarget)
                        {
                            targetPosition = currentTarget.position;
                        }
                        else
                        {
                            targetPosition = originPosition;
                        }
                    }
                    else
                    {
                        Lock(true, true, true, 0.3f);
                        path = GeneratePathPoints(originPosition);
                        currentPathIndex = 1;
                        goingBackToBase = false;
                        enemyState = Enums.EntityState.Motion;
                    }
                }

            }
            yield return null;
        }
    }

    IEnumerator _Lock(bool movement, bool action, float lockTime)
    {
        if(lockTime > 0)
        {
            yield return new WaitForSeconds(lockTime);
        }
        UnLock(movement, action);
    }

    public void UnLock(bool movement, bool action)
    {
        if (movement) canMove = true;
        if (action) canAction = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GizmosManager.DrawCircle(originPosition + Vector3.up * .01f, scanRadius);
        for (int i = 0; i < path.Length - 1; i++)
        {
            Gizmos.DrawLine(path[i], path[i + 1]);
        }
    }
}
