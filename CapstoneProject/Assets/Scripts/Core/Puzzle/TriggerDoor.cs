using System.Collections;
using System.Collections.Generic;
using Core.GameStates;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private bool _isOpening = false;
    private float openingSpeed = 5f;
    public bool IsOpening
    {
        get { return _isOpening; }
        set
        {
            if (_isOpening != value)
            {
                _isOpening = value;
            }
        }
        
    }

    private Vector3 _originalPosition;
    [SerializeField] private Vector3 openingPosition;
    void Start()
    {
        _originalPosition = transform.position;
    }

    void Update()
    {
        if (_isOpening && Vector3.Distance(transform.position, openingPosition) > 0.1f)
        {
            // Calculate the new position towards the opening position
            Vector3 newPosition = Vector3.MoveTowards(transform.position, openingPosition, Time.deltaTime * openingSpeed);

            // Update the door's position
            transform.position = newPosition;
        }
        else if (!_isOpening && Vector3.Distance(transform.position, _originalPosition) > 0.1f)
        {
            // Calculate the new position towards the opening position
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _originalPosition, Time.deltaTime * openingSpeed);

            // Update the door's position
            transform.position = newPosition;
        }
    }
}
