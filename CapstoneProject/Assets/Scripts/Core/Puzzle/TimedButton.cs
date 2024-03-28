using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TimedButton : MonoBehaviour
{
    // Start is called before the first frame update
   public TimedDoor door;




    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            door.StartTimer();
        }
        
        Debug.Log(gameObject.name);
       
    }
}
