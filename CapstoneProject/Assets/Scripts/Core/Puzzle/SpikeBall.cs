using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float radius = 2f;
    [SerializeField]
    private int numPathPoints = 20;
    [SerializeField]
    private GameObject pathPointPrefab;

    private Vector2 _centerPosition;
    private float angle;

    private void Start()
    {
        // Get the center position around which the spike ball will rotate
        _centerPosition = transform.position;
        // Initiate the angle
        angle = 0;
        // Generate path points
        GeneratePathPoints();
    }

    private void Update()
    {
        // Update the angle based on the rotation speed
        angle += rotationSpeed * Time.deltaTime;

        // Calculate the new position based on the angle and radius
        float x = _centerPosition.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = _centerPosition.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        Vector2 newPosition = new Vector2(x, y);

        // Move the spike ball to the new position
        transform.position = newPosition;
    }

    private void GeneratePathPoints()
    {
        // Generate new path points
        for (int i = 0; i < numPathPoints; i++)
        {
            // Calculate the angle for each path point
            float pointAngle = 360f / numPathPoints * i;

            // Calculate the position of the path point
            float x = _centerPosition.x + Mathf.Cos(pointAngle * Mathf.Deg2Rad) * radius;
            float y = _centerPosition.y + Mathf.Sin(pointAngle * Mathf.Deg2Rad) * radius;
            Vector2 pointPosition = new Vector2(x, y);

            // Create a path point object from the prefab
            GameObject pathPoint = Instantiate(pathPointPrefab, pointPosition, Quaternion.identity);
            pathPoint.name = "SpikeBallChain" + i;
        }
    }
}