using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.GameStates;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Vector3 _firstPosition;
    [SerializeField] private Vector3 _secondPosition;
    [SerializeField] private float _speed = 2f;
    private Vector3 _targetPosition;
    private bool _isAtFirstPosition = true;
    private bool _isMoving = false;

    private void Start()
    {
        _firstPosition = transform.position;
        _targetPosition = _secondPosition;
        _isAtFirstPosition = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, _targetPosition);
            
            transform.position += direction * _speed * Time.deltaTime;
            if (distance <= 0.01f)
            {
                _isMoving = false;
                if (_isAtFirstPosition)
                {
                    _targetPosition = _firstPosition;
                    _isAtFirstPosition = false;
                }
                else
                {
                    _targetPosition = _secondPosition;
                    _isAtFirstPosition = true;
                }
                GameState.Instance.CurrentGameState = GameState.States.Gameplay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            _isMoving = true;
        }
    }
}
