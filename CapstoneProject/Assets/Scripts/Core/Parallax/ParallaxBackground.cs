using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float parallaxEffectX;
    [SerializeField] private float parallaxEffectY;
    private float xPosition;
    private float yPosition;
    private float length;
    [SerializeField]
    private SpriteRenderer _middleBackground;

    private void Start()
    {
        _camera = Camera.main;
        length = _middleBackground.bounds.size.x;
        xPosition = transform.position.x;
        xPosition = transform.position.y;
    }

    private void Update()
    {
        float distanceMoved = _camera.transform.position.x * (1 - parallaxEffectX);
        float distanceToMoveX = _camera.transform.position.x * parallaxEffectX;
        float distanceToMoveY = _camera.transform.position.y * parallaxEffectY;
        transform.position = new Vector3(xPosition + distanceToMoveX, yPosition + distanceToMoveY, transform.position.z);
        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }
    }
}
