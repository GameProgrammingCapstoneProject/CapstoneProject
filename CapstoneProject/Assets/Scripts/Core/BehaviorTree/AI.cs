using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Core.Components;
using Core.Entity;

public class AI : MonoBehaviour
{
    Enemy enemy;
    RigidbodyComponent _rb;
    Animator _anim;
    float movementSpeed = 2.0f;

    [SerializeField]
    Transform CastPos;
    [SerializeField]
    float baseCastDistance;

    RigidbodyComponent.FacingDirections currentFacingDirection;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        _anim = enemy.animator;
        _rb = enemy.rb;
        currentFacingDirection = _rb.CurrentFacingDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsHittingWallOrEdge()
    {
        bool result;

        currentFacingDirection = _rb.CurrentFacingDirection;
        // Define the cast distance for left and right
        float castingDistance = baseCastDistance;
        if (currentFacingDirection == RigidbodyComponent.FacingDirections.LEFT )
        {
            castingDistance = -baseCastDistance;
        }

        // determine the target destination based on the cast distance
        Vector3 targetPos = CastPos.position;
        targetPos.x += castingDistance;

        Debug.DrawLine(CastPos.position, targetPos, Color.red);
        Debug.DrawRay(CastPos.position, Vector2.down, Color.red);
        if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        if (Physics2D.Raycast(CastPos.position, -Vector2.up, baseCastDistance, 1 << LayerMask.NameToLayer("Ground")))
        {
            result = false;
        }
        else
        {
            result = true;
        }

        return result;
    }

    [Task]
    public void Roam()
    {
        movementSpeed = (IsHittingWallOrEdge() ? -movementSpeed : movementSpeed);
        if(movementSpeed > 0.1f || movementSpeed < -0.1f)
        {
            _anim.SetInteger("state", 1);
        }
        _rb.SetVelocity(movementSpeed, _rb.velocity.y);
        Task.current.Succeed();
    }
}
