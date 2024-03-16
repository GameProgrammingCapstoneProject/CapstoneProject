using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class HiddenPath : MonoBehaviour
{
    private List<Transform> childrenObject;
    private void Start()
    {
        childrenObject = new List<Transform>();
        foreach (Transform child in transform)
        {
            childrenObject.Add(child);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            foreach (var child in childrenObject)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            foreach (var child in childrenObject)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
