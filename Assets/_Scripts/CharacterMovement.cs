using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintMultiplier;

    private float moveSpeed;

    private bool sprinting;

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

        Vector3 movement = new Vector3(moveDirection.x, 0f, moveDirection.y);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed * sprintMultiplier;
            sprinting = true;
        }
        else
        {
            moveSpeed = walkSpeed;
            sprinting = false;
        }

        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;
        this.transform.position = newPosition;

        if(moveDirection == Vector2.zero)
        {
            anim.SetBool("WalkingUp", false);
            anim.SetBool("WalkingDown", false);
            anim.SetBool("WalkingSide", false);
            anim.SetBool("RunningUp", false);
            anim.SetBool("RunningDown", false);
            anim.SetBool("RunningSide", false);
            anim.SetBool("RunningDiagonal", false);
            anim.SetBool("WalkingDiagonal", false);
        }
        else
        {
            if (!sprinting)
            {
                if (moveDirection.x > 0f && moveDirection.y > 0f)
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
                else if (moveDirection.x < 0f && moveDirection.y > 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingDiagonal", true);

                    sprite.flipX = true;
                }
                else if (moveDirection.x > 0f && moveDirection.y < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingSide", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.x < 0f && moveDirection.y < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingSide", true);

                    sprite.flipX = true;
                }
                else if (moveDirection.x > 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingSide", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.x < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingSide", true);

                    sprite.flipX = true;
                }
                else if (moveDirection.y > 0f)
                {
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingUp", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.y < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("WalkingDown", true);

                    sprite.flipX = false;
                }
                else
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    sprite.flipX = false;
                }
            }
            else
            {
                if (moveDirection.x > 0f && moveDirection.y > 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);

                    anim.SetBool("RunningDiagonal", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.x < 0f && moveDirection.y > 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("WalkingDiagonal", false);

                    anim.SetBool("RunningDiagonal", true);

                    sprite.flipX = true;
                }
                else if (moveDirection.x > 0f && moveDirection.y < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("RunningSide", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.x < 0f && moveDirection.y < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("RunningSide", true);

                    sprite.flipX = true;
                }
                else if (moveDirection.x > 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("RunningSide", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.x < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("RunningSide", true);

                    sprite.flipX = true;
                }
                else if (moveDirection.y > 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("RunningUp", true);

                    sprite.flipX = false;
                }
                else if (moveDirection.y < 0f)
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    anim.SetBool("RunningDown", true);

                    sprite.flipX = false;
                }
                else
                {
                    anim.SetBool("WalkingUp", false);
                    anim.SetBool("WalkingDown", false);
                    anim.SetBool("WalkingSide", false);
                    anim.SetBool("WalkingDiagonal", false);
                    anim.SetBool("RunningUp", false);
                    anim.SetBool("RunningDown", false);
                    anim.SetBool("RunningSide", false);
                    anim.SetBool("RunningDiagonal", false);

                    sprite.flipX = false;
                }
            }
        }
    }
}
