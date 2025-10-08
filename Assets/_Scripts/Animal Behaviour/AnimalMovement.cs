using UnityEngine;
using UnityEngine.UIElements;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceVertical = 10f;
    [SerializeField]
    private float maxDistanceHorizontal = 10f;

    [SerializeField]
    private float moveSpeed;
    private float timeTillDest;

    private float timeBetweenMovements = 5f;
    private float currentTimeBetween;
    private float step;

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 newPosition;

    [SerializeField]
    private GameObject playerChar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTimeBetween = Time.realtimeSinceStartup + timeBetweenMovements;
        playerChar = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerChar.transform.position) < 1f) {
            return;
        }
        else {
            if (currentTimeBetween < Time.realtimeSinceStartup) {
                Reposition();
            }

            if (Vector2.Distance(transform.position, newPosition) > 0.1f) {
                transform.position = Vector2.MoveTowards(transform.position, newPosition, timeTillDest);
            }
            //transform.position = Vector2.Lerp (transform.position, newPosition, timeTillDest * Time.deltaTime);
        }
    }

    void Reposition() {
        
        newPosition = new Vector2
            (Random.Range(maxDistanceVertical, -maxDistanceVertical), Random.Range(maxDistanceHorizontal, -maxDistanceHorizontal));
        Debug.Log(newPosition);
        timeTillDest = 0f;
        timeTillDest = Vector3.Distance(transform.position, newPosition) * Time.deltaTime;
        if (newPosition.x < transform.position.x) { gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z); }
        else gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            currentTimeBetween = Time.realtimeSinceStartup + timeBetweenMovements;
    }
}
