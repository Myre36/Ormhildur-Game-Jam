using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private float maxDistanceVertical = 10f;
    [SerializeField]
    private float maxDistanceHorizontal = 10f;

    private float timeBetweenMovements = 5f;
    private float currentTimeBetween;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    private Vector3 newPosition;

    [SerializeField]
    private GameObject playerChar;
    [SerializeField]
    private GameObject emoticons;

    public bool pickingUpItem = false;
    private bool returningItem = false;
    public bool eating = false;

    public Transform ballPos;

    public Transform foodPos;

    void Start()
    {
        currentTimeBetween = Time.realtimeSinceStartup + timeBetweenMovements;
        playerChar = GameObject.Find("Player");
        agent.updateRotation = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerChar.transform.position) < 5f) {
            animator.SetBool("IsWalking", false);
            newPosition = transform.position;
            emoticons.SetActive(true);
        } else emoticons.SetActive(false);

        if(pickingUpItem)
        {
            FetchItem();
            Debug.Log("Fetching item");
        }
        else if(returningItem)
        {
            BringItem();
            Debug.Log("Bringing item back");
        }
        else if(eating)
        {
            GoEat();
            Debug.Log("Going to eat");
        }
        else if (currentTimeBetween < Time.realtimeSinceStartup && !pickingUpItem) {
            Reposition();
            Debug.Log("Repositioning");
        }

            agent.SetDestination(newPosition);
        if (Vector3.Distance(newPosition, transform.position) < 0.1f) animator.SetBool("IsWalking", false );
    }

    void Reposition() {
        Debug.Log("Finding New Position");
        newPosition = new Vector3
            (Random.Range(maxDistanceVertical, -maxDistanceVertical), transform.position.y, Random.Range(maxDistanceHorizontal, -maxDistanceHorizontal));
        Debug.Log(newPosition);
        animator.SetBool("IsWalking", true);
        currentTimeBetween = Time.realtimeSinceStartup + timeBetweenMovements;
        if (newPosition.x < transform.position.x) { sprite.transform.rotation = Quaternion.Euler(sprite.transform.rotation.x, 180f, sprite.transform.rotation.z); }
        else sprite.transform.rotation = Quaternion.Euler(sprite.transform.rotation.x, 0f, sprite.transform.rotation.z);
    }

    private void FetchItem() 
    {
        newPosition = new Vector3(ballPos.position.x, 0f, ballPos.position.z);
        animator.SetBool("IsWalking", true);
        if (Vector3.Distance(newPosition, transform.position) < 4.5f) {
            Debug.Log("Reached destination");
            returningItem = true;
            pickingUpItem = false;
            Destroy(ballPos.gameObject);
        }
    }

    private void BringItem()
    {
        newPosition = new Vector3(playerChar.transform.position.x, 0f, playerChar.transform.position.z);
        animator.SetBool("IsWalking", true);
        if (Vector3.Distance(newPosition, transform.position) < 4.5f)
        {
            Debug.Log("Brought item back");
            returningItem = false;
            pickingUpItem = false;
            playerChar.GetComponent<CharacterMovement>().hasBall = true;
        }
    }

    private void GoEat()
    {
        newPosition = new Vector3(foodPos.position.x, 0f, foodPos.position.z);
        animator.SetBool("IsWalking", true);

        if (Vector3.Distance(newPosition, transform.position) < 4.5f)
        {
            Debug.Log("Finished eating");
            eating = false;
            playerChar.GetComponent<CharacterMovement>().hasFood = true;
            Destroy(foodPos.gameObject);
        }
    }
}
