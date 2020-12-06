using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovementController : MonoBehaviour
{

    
    public float m_rotationSpeed = 200f;
    [HideInInspector]
    public float m_movementSpeed = 10f;

    Vector3 m_forwarDirection;

    private AiController aiController;
    private CharacterAnimationController animationController;
    private CharacterControllerBase controllerBase;
    private bool canMove = true;
    private bool playLocomotionAinimation = true;
    private Rigidbody body;

    public bool autoMove;
    private Vector3[] path = new Vector3[0];
    private int currentPathIndex;
    private Transform target;

    void Start()
    {
        controllerBase = GetComponent<CharacterControllerBase>();
        animationController = GetComponent<CharacterAnimationController>();
        canMove = true;
        body = GetComponent<Rigidbody>();
        aiController = GetComponent<AiController>();
    }

    void FixedUpdate()
    {
        if (aiController.aiControllable)
        {
            aiController.Move();
        }
        else
        {
            
            Rotate();
            Move();
        }
        
        
    }


    public void Move(Vector3 direction)
    {
        if (!canMove)
        {
            if(direction.magnitude > 0 && autoMove)
            {
                controllerBase.UnLock(true, false, false);
            }
            m_forwarDirection =  Vector3.zero;
        }
        else
        {
            if (!autoMove)
            {
                if (direction.magnitude > 0)
                {

                    controllerBase.entityState = Enums.EntityState.Motion;
                }
                else
                {
                    controllerBase.entityState = Enums.EntityState.Idle;
                }
            }
            if(direction.magnitude > 0)
            {
                autoMove = false;
            }
            //print(controllerBase.entityState);
            direction.Normalize();
            m_forwarDirection = direction;
        }

        
        if (playLocomotionAinimation)
        {
            Enums.Anim_ID_Map anim = (controllerBase.entityState == Enums.EntityState.Motion) ? Enums.Anim_ID_Map.Walk : Enums.Anim_ID_Map.Idle;
            animationController.ProcessLocomotionAnimation(anim);
        }
        
    }

    public void AutoMove(LivingEntity _target, float stopingDistance)
    {
        target = _target.transform;
        currentPathIndex = 1;
        path = controllerBase.GeneratePathPoints(target.position);
        autoMove = true;
        
    }

    

    private void Rotate()
    {
        if (m_forwarDirection == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(m_forwarDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
    }

    public void UpdateRotationToTarget()
    {
        LivingEntity target = controllerBase.currentPlayerTerget;
        if(target != null)
        {
            Vector3 dir =  target.transform.position - transform.position;
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = rotation;
        }
    }

    internal void UnlockMovement()
    {
        canMove = true;
    }
    internal void LockMovement()
    {
        canMove = false;
    }

    internal void LockLocomotionAnimation()
    {
        playLocomotionAinimation = false;
    }
    internal void UnlockLocomotionAnimation()
    {
        playLocomotionAinimation = true;
    }

    private void Move()
    {
        if (!autoMove)
        {
            Vector3 position = body.position;
            Vector3 newPosition = position + m_forwarDirection * m_movementSpeed * Time.deltaTime;
            newPosition.y = position.y;
            body.MovePosition(newPosition);
        }
        else
        {

            if (path.Length > 0 && currentPathIndex < path.Length)
            {
                float dis = Vector3.Distance(path[currentPathIndex], transform.position);
                float stopDistanceCheck = Vector3.Distance(transform.position, target.position);

                if (stopDistanceCheck >= controllerBase.scanRadius)
                {
                    Vector3 dir = path[currentPathIndex] - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex], controllerBase.props.characterSpeed * Time.deltaTime);
                    controllerBase.entityState = Enums.EntityState.Motion;
                }
                else
                {
                    controllerBase.entityState = Enums.EntityState.Action;
                    //print("Attack time");
                }

                if (dis <= 0.1f)
                {
                    currentPathIndex++;
                }

                if (Vector3.Distance(target.position, transform.position) > 0.02f)
                {
                    path = controllerBase.GeneratePathPoints(target.position);
                    currentPathIndex = 1;

                }
            }

        }
        
        //transform.position = newPosition;
    }



    public void MoveTowards(Vector3 velocity)
    {
        transform.position += velocity * Time.deltaTime;
    }
}
