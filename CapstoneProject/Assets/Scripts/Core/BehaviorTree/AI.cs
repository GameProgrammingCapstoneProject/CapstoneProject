using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Core.Components;
using Core.Entity;
using Unity.VisualScripting;

public class AI : MonoBehaviour
{
    Enemy enemy;
    RigidbodyComponent _rb;
    Animator _anim;
    float initialSpeed = 2.0f;
    float stopSpeed = 0f;
    float movementSpeed;
    float offsetY = 1.0f;

    [SerializeField]
    Transform playerPos;
    RigidbodyComponent playerRB;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        _anim = enemy.animator;
        _rb = enemy.rb;
        movementSpeed = initialSpeed;
        playerPos = GameObject.FindWithTag("Player").transform;
        playerRB = GameObject.FindWithTag("Player").gameObject.GetComponent<RigidbodyComponent>();
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

    //[Task]
    //public bool IsWithinView()
    //{
    //    RaycastHit2D hit;
    //    hit = Physics2D.Raycast(transform.position, Vector2.right);
    //    if(hit.collider.CompareTag("Player"))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    [Task]
    public bool IsWithinRange()
    {
        bool result = false;
        float distanceToTarget = Vector2.Distance(playerPos.position, transform.position);
        float attackRange = 1.5f;

        if(distanceToTarget <= attackRange)
        {
            if(playerPos.position.x < transform.position.x )
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
    public void Attack()
    {
        //this.GetComponent<EnemyHealthComponent>().DoDamage(5);
        movementSpeed = stopSpeed;
        AnimStateUpdate();
        Task.current.Succeed();
    }

    void AnimStateUpdate()
    {
        if (movementSpeed > 0.1f || movementSpeed < -0.1f)
        {
            _anim.SetInteger("state", 1);
        }
        else
        {
            _anim.SetInteger("state", 0);
        }

        if(IsWithinRange() && movementSpeed == stopSpeed)
        {
            _anim.SetInteger("state", 2);
        }
    }
}
