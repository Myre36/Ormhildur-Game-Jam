using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceVertical = 10f;
    [SerializeField]
    private float maxDistanceHorizontal = 10f;

    [SerializeField]
    private float moveSpeed;

    private float timeBetweenMovements = 5f;
    private float currentTimeBetween;
    private float step;

    private Vector2 newPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       transform.position = new Vector2 
            (Random.Range(maxDistanceVertical, -maxDistanceVertical), Random.Range(maxDistanceHorizontal, -maxDistanceHorizontal));

        currentTimeBetween = Time.realtimeSinceStartup + timeBetweenMovements;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;
        step += moveSpeed * deltaTime;
        if (currentTimeBetween < Time.realtimeSinceStartup) {
            Reposition();
        }
    
        transform.position = Vector2.Lerp (transform.position, newPosition, step);
    }

    void Reposition() {
        step = 0f;
        newPosition = new Vector2
            (Random.Range(maxDistanceVertical, -maxDistanceVertical), Random.Range(maxDistanceHorizontal, -maxDistanceHorizontal));
        Debug.Log(newPosition);
        currentTimeBetween = Time.realtimeSinceStartup + timeBetweenMovements;
    }
}
