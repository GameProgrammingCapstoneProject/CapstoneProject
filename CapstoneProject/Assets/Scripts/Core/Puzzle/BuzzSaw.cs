using System;
using Core.Entity;
using UnityEngine;

public class BuzzSaw : MonoBehaviour
{
    [SerializeField]
    private GameObject pathObjectPrefab;
    public Vector3[] destinations;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float distanceBetweenPathObjects = 2f;

    private int currentDestinationIndex;

    private void Start()
    {
        // Start moving towards the first destination
        currentDestinationIndex = 0;
        transform.position = destinations[0];
        // Generate path objects from the last index position to the first index position
        Vector3 startPosition = destinations[^1];
        Vector3 endPosition = destinations[0];
        Vector3 direction = (endPosition - startPosition).normalized;
        float distance = Vector3.Distance(startPosition, endPosition);
        int numPathObjects = Mathf.CeilToInt(distance / distanceBetweenPathObjects);

        for (int j = 0; j < numPathObjects; j++)
        {
            // Calculate the position of the path object
            Vector3 position = startPosition + direction * (j * distanceBetweenPathObjects);
                
            // Instantiate the path object
            GameObject pathObject = Instantiate(pathObjectPrefab, position, Quaternion.identity);
        }
        // Instantiate path objects along the buzzsaw's path
        for (int i = 0; i < destinations.Length - 1; i++)
        {
            startPosition = destinations[i];
            endPosition = destinations[i + 1];
            direction = (endPosition - startPosition).normalized;
            distance = Vector3.Distance(startPosition, endPosition);
            numPathObjects = Mathf.CeilToInt(distance / distanceBetweenPathObjects);

            for (int j = 0; j < numPathObjects; j++)
            {
                // Calculate the position of the path object
                Vector3 position = startPosition + direction * (j * distanceBetweenPathObjects);
                // Instantiate the path object
                GameObject pathObject = Instantiate(pathObjectPrefab, position, Quaternion.identity);
            }
        }
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