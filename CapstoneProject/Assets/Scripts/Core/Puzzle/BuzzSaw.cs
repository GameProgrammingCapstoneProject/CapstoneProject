using System;
using Core.Entity;
using UnityEngine;

public class BuzzSaw : MonoBehaviour
{
    public Vector3[] destinations;
    public float speed = 5f;

    private int currentDestinationIndex;

    private void Start()
    {
        // Start moving towards the first destination
        currentDestinationIndex = 0;
        transform.position = destinations[0];
    }

    private void Update()
    {
        // Move towards the current destination
        transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestinationIndex], speed * Time.deltaTime);

        // If the buzzsaw reaches the current destination, switch to the other destination
        if (transform.position == destinations[currentDestinationIndex])
        {
            if (currentDestinationIndex == destinations.Length - 1)
            {
                currentDestinationIndex = 0;
            }
            else
            {
                currentDestinationIndex += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            Player player = other.GetComponent<Player>();
            player.GetComponent<PlayerHealthComponent>().TakeDamage(1);
        }
    }
}