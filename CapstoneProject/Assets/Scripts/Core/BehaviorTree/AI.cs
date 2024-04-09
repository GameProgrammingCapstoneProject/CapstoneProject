using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Core.Components;
using Core.Entity;
using Unity.VisualScripting;
using Panda.Examples.Shooter;

public class AI : MonoBehaviour
{
    
    Enemy enemy;
    RigidbodyComponent _rb;
    [SerializeField]
    Animator _anim;
    float stopSpeed = 0.0f;
    float movementSpeed;
    float dashSpeed;
    float offsetY = 1.0f;

    bool isDashing;

    Vector2 SpawnPoint = Vector2.zero;

    [SerializeField]
    float initialSpeed;
    [SerializeField]
    Transform playerPos;
    [SerializeField]
    float enemyRange;
    RigidbodyComponent playerRB;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        //_anim = enemy.animator;
        _rb = enemy.rb;
        movementSpeed = initialSpeed;
        playerPos = GameObject.FindWithTag("Player").transform;
        playerRB = GameObject.FindWithTag("Player").gameObject.GetComponent<RigidbodyComponent>();
        SpawnPoint = transform.position;
        dashSpeed = movementSpeed * 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    [Task]
    public bool HasReachedObstacle()
    {
        bool result = false;

        if(enemy.IsHittingWall() || enemy.IsHittingEdge())
        {
            //initialSpeed = -movementSpeed;
            movementSpeed = stopSpeed;
            AnimStateUpdate();
            result = true;
        }

        return result;
    }

    [Task]
    public void Turn()
    {
        movementSpeed = initialSpeed;
        if(_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
        {
            movementSpeed = -initialSpeed;
        }
        _rb.SetVelocity(movementSpeed, _rb.velocity.y);
        Task.current.Succeed();
    }

    [Task]
    public void Move()
    {
        if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
        {
            movementSpeed = initialSpeed;
        }
        else
        {
            movementSpeed = -initialSpeed;
        }
        AnimStateUpdate();
        _rb.SetVelocity(movementSpeed, _rb.velocity.y);
        Task.current.Succeed();
    }

    [Task]
    public void Idle()
    {
        movementSpeed = stopSpeed;
        AnimStateUpdate();
        Task.current.Succeed();
    }

    //[Task]
    //public bool IsWithinView()
    //{
    //    RaycastHit2D hit;
    //    hit = Physics2D.Raycast(transform.position, Vector2.right * 5.0f);
    //    Debug.DrawRay(transform.position, Vector2.right * 5.0f);
    //    if (hit.collider.CompareTag("Player"))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    [Task]
    public bool IsDead()
    {
        if(enemy.GetComponent<EnemyHealthComponent>().isDead)
        {
            movementSpeed = stopSpeed;
            AnimStateUpdate();
            return true;
        }

        return false;

    }

    [Task]
    public bool IsWithinView(float distance)
    {
        bool result = false;
        Vector2 direction = Vector2.zero;
        Vector2 directionR = Vector2.zero;
        Vector2 directionL = Vector2.zero;

        directionR = Vector2.right;
        directionL = Vector2.left;
        direction = (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT ? Vector2.right : Vector2.left);

        /* if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
         {
             direction = Vector2.right;
         }
         else
         {
             direction = Vector2.left;
         }*/


        //Debug.DrawRay(transform.position, directionR * 15, Color.green);
        //Debug.DrawRay(transform.position, directionL * 15, Color.green);
        Debug.DrawRay(transform.position, direction * 15, Color.green);
        if (Physics2D.Raycast(transform.position, direction, distance, 1 << LayerMask.NameToLayer("Player")))
        {
            result = true;
        }
        //if (Physics2D.Raycast(transform.position, directionR, 15.0f, 1 << LayerMask.NameToLayer("Player")))
        //{
        //    result = true;
        //}
        //if (Physics2D.Raycast(transform.position, directionL, 15.0f, 1 << LayerMask.NameToLayer("Player")))
        //{
        //    result = true;
        //}

        return result;
    }

    //[Task]
    //public bool IsWithinRange()
    //{
    //    bool result = false;
    //    float distanceToTarget = Vector2.Distance(playerPos.position, transform.position);
    //    float attackRange = 1.5f;

    //    if(distanceToTarget <= attackRange)
    //    {
    //        if(playerPos.position.x < transform.position.x )
    //        {
    //            if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
    //            {
    //                Turn();
    //            }
    //        }
    //        else
    //        {
    //            if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.LEFT)
    //            {
    //                Turn();
    //            }

    //        }
    //        result = true;
    //    }

    //    return result;
    //}

    [Task]
    public bool IsWithinRange(float range)
    {
        bool result = false;
        float distanceToTarget = Vector2.Distance(playerPos.position, transform.position);
        float attackRange = range;

        if (distanceToTarget <= attackRange)
        {
            if (playerPos.position.x < transform.position.x)
            {
                if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
                {
                    Turn();
                }
            }
            else
            {
                if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.LEFT)
                {
                    Turn();
                }

            }
            result = true;
        }
        return result;
    }

    [Task]
    public void Chase()
    {
        movementSpeed = initialSpeed;
        AnimStateUpdate();
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, movementSpeed * Time.deltaTime);
        Task.current.Succeed();
    }
    [Task]
    public void ReturnToSpawnPoint()
    {
        movementSpeed = initialSpeed;
        AnimStateUpdate();
        transform.position = Vector2.MoveTowards(transform.position, SpawnPoint, movementSpeed * Time.deltaTime);
        Task.current.Succeed();
    }

    [Task]
    public bool IsOnSamePlatform()
    {
        bool result;
        if((playerPos.position.y <= (transform.position.y + offsetY)) && (playerPos.position.y >= (transform.position.y - offsetY)))
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    [Task]
    public void MeleeAttack()
    {
        //this.GetComponent<EnemyHealthComponent>().DoDamage(5);
        movementSpeed = stopSpeed;
        AnimStateUpdate();
        Task.current.Succeed();
    }

    [Task]
    public void Dash()
    {
        isDashing = true;
        if (_rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
        {
            movementSpeed = dashSpeed;
            _rb.ApplyForceToObject(transform.right, dashSpeed);
        }
        else
        {
            movementSpeed = -dashSpeed;
            _rb.ApplyForceToObject(transform.right, dashSpeed);
        }
        AnimStateUpdate();
        //_rb.SetVelocity(movementSpeed, _rb.velocity.y);
        Task.current.Succeed();
    }

    [Task]
    public void RangedAttack()
    {
        movementSpeed = stopSpeed;
        AnimStateUpdate();
        Task.current.Succeed();
    }

    void AnimStateUpdate()
    {
        switch (enemy.enemyType)
        {
            case (int)Enemy.EnemyType.MELEE_ENEMY:
                if (movementSpeed > 0.1f || movementSpeed < -0.1f)
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.WALK);
                }
                else
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.IDLE);
                }

                if (IsWithinRange(enemyRange) && movementSpeed == stopSpeed)
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.ATTACK);
                }

                if (enemy.GetComponent<EnemyHealthComponent>().isDead == true)
                {
                    _anim.SetInteger("state", -1);
                    _anim.SetTrigger("death");
                }
                break;

            case (int)Enemy.EnemyType.RANGED_ENEMY:

                if (IsWithinRange(enemyRange))
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.ATTACK);
                }
                else
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.IDLE);
                }

                if (enemy.GetComponent<EnemyHealthComponent>().isDead == true)
                {
                    _anim.SetInteger("state", -1);
                    _anim.SetTrigger("death");
                }
                break;

            case (int)Enemy.EnemyType.FLYING_ENEMY:

                if (IsWithinRange(enemyRange))
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.ATTACK);
                }
                else
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.IDLE);
                }

                if (enemy.GetComponent<EnemyHealthComponent>().isDead == true)
                {
                    _anim.SetInteger("state", -1);
                    _anim.SetTrigger("death");
                }
                break;

            case (int)Enemy.EnemyType.CHASING_ENEMY:

                
                break;

            case (int)Enemy.EnemyType.DASHING_ENEMY:
                if((movementSpeed > 0.1f || movementSpeed < -0.1f) && !isDashing)
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.WALK);
                }
                else if((movementSpeed > 0.1f || movementSpeed < -0.1f) && isDashing)
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.WALK);
                }
                else
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.IDLE);
                }

                if (IsWithinRange(enemyRange) && movementSpeed == stopSpeed)
                {
                    _anim.SetInteger("state", (int)Enemy.AnimationState.ATTACK);
                }

                if (enemy.GetComponent<EnemyHealthComponent>().isDead == true)
                {
                    _anim.SetInteger("state", -1);
                    _anim.SetTrigger("death");
                }
                break;
        }
    }
}
