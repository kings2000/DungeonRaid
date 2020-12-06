using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    [Range(.1f, 10)]
    public float maxFollowRadius = 1f;
    public float maxDistanceToStartFollow = 6f;

    //NavMeshAgent agent;
    CharacterMovementController movementController;
    CharacterAnimationController animationController;
    CharacterControllerBase controllerBase;
    [HideInInspector]public bool aiControllable;
    private bool canPathfind;
    private bool founEnemy;

    private float pathSerchRefreshRate = 1f;
    CharacterControllerBase mainCharacter;
    public Vector3[] path;

    protected void Start()
    {
        path = new Vector3[0];
        movementController = GetComponent<CharacterMovementController>();
        animationController = GetComponent<CharacterAnimationController>();
        controllerBase = GetComponent<CharacterControllerBase>();
       // agent = GetComponent<NavMeshAgent>();
        //agent.speed = movementController.m_movementSpeed;
        canPathfind = true;
        StartCoroutine(UpdateLocation());
        
    }

    public void ActivateAiAgent(bool value, CharacterControllerBase newFocusChrater)
    {
        aiControllable = value;
        if (value)
        {
            //GetComponent<NavMeshObstacle>().enabled = !value;
            //GetComponent<NavMeshAgent>().enabled = value;
        }
        else
        {
            //GetComponent<NavMeshAgent>().enabled = value;
            //GetComponent<NavMeshObstacle>().enabled = !value;
        }
        
        mainCharacter = newFocusChrater;
       
    }

    public void Rotate()
    {
        Vector3 dir = rotateTargetPoint - transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, movementController.m_rotationSpeed * Time.deltaTime);
    }

    public void Move()
    {

        if (aiControllable)
        {
            
            if (path.Length > 0 && currentPathIndex < path.Length)
            {
                rotateTargetPoint = path[currentPathIndex];
                transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex], controllerBase.props.characterSpeed * Time.deltaTime);
                controllerBase.entityState = Enums.EntityState.Motion;

                float distance = Vector3.Distance(path[currentPathIndex], transform.position);
                float stopingDistance = (founEnemy) ? controllerBase.scanRadius : 0.1f;

                if (founEnemy)
                {
                    if (distance <= stopingDistance)
                    {
                        currentPathIndex++;
                        if (currentPathIndex == path.Length)
                        {
                            controllerBase.entityState = Enums.EntityState.Action;
                        }
                    }
                }
                else
                {
                    if (distance <= 0.1f)
                    {
                        currentPathIndex++;
                        if (currentPathIndex == path.Length)
                        {
                            controllerBase.entityState = Enums.EntityState.Idle;
                        }
                    }
                }

            }
            else
            {
                if(controllerBase.entityState == Enums.EntityState.Action && founEnemy)
                {
                    rotateTargetPoint = currentEnemyTarget.transform.position;
                }
            }


            if (controllerBase.entityState == Enums.EntityState.Motion)
            {
                //print("Yesh pabe");
                animationController.ProcessLocomotionAnimation(Enums.Anim_ID_Map.Walk);
            }
            else if(controllerBase.entityState == Enums.EntityState.Idle)
            {
                animationController.ProcessLocomotionAnimation(Enums.Anim_ID_Map.Idle);
            }
            Rotate();
        }
        

    }

    int currentPathIndex = 1;
    private LivingEntity currentEnemyTarget;
    private Vector3 rotateTargetPoint;

    IEnumerator UpdateLocation()
    {
        WaitForSeconds sec = new WaitForSeconds(1);
        while (true)
        {
            if (aiControllable && canPathfind)
            {
                
                if (!founEnemy)
                {
                    ScanForEnemy();
                }
                else
                {
                    //calculate maxdistance for the ai to switch back to the main character
                    float dis = Vector3.Distance(mainCharacter.transform.position, currentEnemyTarget.transform.position);
                    if(dis > mainCharacter.maxRadiusForAiToAttakEnemy)
                    {
                        print("Left Ennemy");
                        founEnemy = false;
                    }
                }

                Transform target = (founEnemy) ? currentEnemyTarget.transform : mainCharacter.transform;
                float pathResetThreshold = (founEnemy) ? 1 : maxDistanceToStartFollow;

                float distance = Vector3.Distance(target.position, transform.position);
                if (distance > maxDistanceToStartFollow)
                {
                    Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    Vector3 point = mainCharacter.transform.position + dir * Random.Range(maxFollowRadius - 1, maxFollowRadius);
                    point = (founEnemy) ? target.position : point;
                    path = controllerBase.GeneratePathPoints(point);
                    currentPathIndex = 1;
                    yield return sec;
                }
            }
            yield return null;
        }
    }


    public void ScanForEnemy()
    {
        if (controllerBase.GetClosetObject(Constants.ENEMY_TAG, out GameObject target))
        {
            float dist = Vector3.Distance(target.transform.position, mainCharacter.transform.position);
            if (dist <= controllerBase.maxRadiusForAiToAttakEnemy)
            {
                currentEnemyTarget = target.GetComponent<LivingEntity>();
                founEnemy = true;
                path = controllerBase.GeneratePathPoints(target.transform.position);
                currentPathIndex = 1;
                print("Found Ennemy");
            }
        }
    }


    
}
