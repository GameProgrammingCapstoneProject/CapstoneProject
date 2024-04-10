using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHover : MonoBehaviour
{
    private GameObject InteractKeyObject;

    void Start()
    {
        InteractKeyObject = transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision?");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("It's a player!");
            InteractKeyObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("No more collision?");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("It's a player but its leaving!");
            InteractKeyObject.SetActive(false);
        }
    }

}
