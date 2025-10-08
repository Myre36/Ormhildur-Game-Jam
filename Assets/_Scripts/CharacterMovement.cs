using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector2 moveDirection;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private SpriteRenderer sprite;

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

        if(moveDirection.x > 0f && moveDirection.y > 0f)
        {
            anim.SetBool("WalkingUp", false);
            anim.SetBool("WalkingDown", false);
            anim.SetBool("WalkingSide", false);
            anim.SetBool("RunningUp", false);
            anim.SetBool("RunningDown", false);
            anim.SetBool("RunningSide", false);
            anim.SetBool("RunningDiagonal", false);

            anim.SetBool("WalkingDiagonal", true);

            sprite.flipX = false;
        }
    }
}
