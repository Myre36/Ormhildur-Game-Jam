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

    private bool pickingUpItem = false;

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

        if (currentTimeBetween < Time.realtimeSinceStartup && !pickingUpItem) {
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

    public void FetchItem(Vector3 itemPos) {
        pickingUpItem = !pickingUpItem;
        newPosition = itemPos;
        if ((itemPos.magnitude * transform.position.magnitude) > 3f) {
            newPosition = playerChar.transform.position;
        }
    }
}
