using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private float _existTime;
    private Vector3 _targetPos;
    private Vector3 _startPos;
    public float speed = 15f;
    private int currentPointIndex = 0;
    private List<Vector2> pathPoints;
    private bool _isImpact = false;
    private const float _permissibleDistance = 0.1f;
    private float _height = 0f;
    private int _numberOfParabolaPoints = 20;
    [SerializeField]
    private int _damage = 20;
    private void Start()
    {
        if (pathPoints.Count > 0)
        {
            transform.position = pathPoints[0];
        }
    }

    private void Update()
    {
        if (_isImpact) return;
        _existTime -= Time.deltaTime;
        if (_existTime < 0)
            SelfDestroy();
        if (currentPointIndex < pathPoints.Count)
        {
            Vector2 targetPosition = pathPoints[currentPointIndex];
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            // Move the arrow
            transform.position += (Vector3)moveDirection * (speed * Time.deltaTime);

            // Rotate the arrow to look in the move direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Check if the arrow has reached the current point
            if (Vector2.Distance(transform.position, targetPosition) < _permissibleDistance)
            {
                currentPointIndex++;
            }
        }
    }

    public void Setup(float existTime, Vector3 startPos, Vector3 targetPos)
    {
        _existTime = existTime;
        _startPos = startPos;
        _targetPos = targetPos;
        SetupArrowHeightAndParabolaPoints();
        pathPoints = GenerateParabolaPoints(_startPos, _targetPos, _height, _numberOfParabolaPoints);
    }
    private Vector3 CreateParabola(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        Vector2 travelDirection = end - start;
        Vector2 result = start + t * travelDirection;
        result.y += (-parabolicT * parabolicT + 1) * height;
        return result;
    }
    
    private List<Vector2> GenerateParabolaPoints(Vector3 start, Vector3 end, float height, int numPoints)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1); // Normalize t between 0 and 1
            Vector2 point = CreateParabola((Vector2)start, (Vector2)end, height, t);
            points.Add(point);
        }
        return points;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground") || 
            col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitCollision(col.gameObject);
        }
    }

    private void HitCollision(GameObject target)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _animator.SetTrigger("Impact");
        if (target.GetComponent<EnemyHealthComponent>() != null)
        {
            target.GetComponent<EnemyHealthComponent>().TakeDamage(_damage);
        }
        _isImpact = true;
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

    private void SetupArrowHeightAndParabolaPoints()
    {
        // Player is standing on a higher position than the enemy
        if (Vector2.Distance(_startPos, _targetPos) > 5.0f)
        {
            _height = 1f;
            _numberOfParabolaPoints = 20;
        }
        else if (Vector2.Distance(_startPos, _targetPos) > 10.0f)
        {
            _height = 2f;
            _numberOfParabolaPoints = 30;
        }
        else
        {
            _height = 0f;
            _numberOfParabolaPoints = 10;
        }
    }
}
