using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector2 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0.0f);

        Vector3 newPosition = transform.position + movement * Time.deltaTime;
        this.transform.position = newPosition;
    }
}
