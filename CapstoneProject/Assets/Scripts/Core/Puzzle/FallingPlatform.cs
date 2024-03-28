using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool once = false;

    public float upForce;
    void Start()
    {
        upForce = 20f;
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            if(once == false)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                once = true;
                Invoke("StopFalling", 0.05f);
                Invoke("StartFallAndDestroy", 1f);
            }
            
        }
    }

    public void StopGoingUp()
    {
        // upForce = 0f;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void StopFalling()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void StartFallAndDestroy()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        Destroy(gameObject, 3f);
    }
}
