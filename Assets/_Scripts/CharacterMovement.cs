using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintSpeed;

    private float moveSpeed;

    private bool sprinting;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool throwing = false;
    public bool hasBall = true;
    [SerializeField]
    private Camera cam;
    [SerializeField] 
    private GameObject throwPrefab;

    private bool feeding = false;
    [SerializeField]
    private GameObject foodPrefab;
    public bool hasFood = true;

    [SerializeField] 
    private float throwForce = 10f;
    [SerializeField]
    private AnimalMovement animal;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private LayerMask throwWalls;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(!throwing && hasBall && hasFood && !feeding)
            {
                throwing = true;
            }
            else
            {
                throwing = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (hasFood && !feeding && !throwing && hasBall)
            {
                feeding = true;
            }
            else
            {
                feeding = false;
            }
        }

        if (throwing)
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

            if (Input.GetMouseButtonDown(0))
            {
                Throw();
            }
        }
        else if(feeding)
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

            if (Input.GetMouseButtonDown(0))
            {
                Feed();
            }
        }
        else
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = sprintSpeed;
                sprinting = true;
            }
            else
            {
                moveSpeed = walkSpeed;
                sprinting = false;
            }

            //Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;
            //this.transform.position = newPosition;

            ChangeAnimation();
            SpeedControl();
        }
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        rb.AddForce(moveDirection * (moveSpeed * 10f) * Time.deltaTime, ForceMode.Force);
    }

    //A function that prvents the player from going too fast
    private void SpeedControl()
    {
        //Calculates the X and Z velocity that the player is moving in
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //Limit the velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    public void Throw()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, ~throwWalls))
        {
            GameObject thrownObject = Instantiate(throwPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                Vector3 direction = (hit.point - transform.position).normalized;

                rb.AddForce(direction * throwForce, ForceMode.Impulse);

                animal.ballPos = thrownObject.transform;
                animal.pickingUpItem = true;
                hasBall = false;
                throwing = false;
            }
            else
            {
                Debug.Log("No rigidbody found on the thrown object");
            }
        }
    }

    public void Feed()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, ~throwWalls))
        {
            GameObject thrownObject = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (hit.point - transform.position).normalized;

                rb.AddForce(direction * (throwForce / 3f), ForceMode.Impulse);

                animal.foodPos = thrownObject.transform;
                animal.eating = true;
                feeding = false;
                hasFood = false;
            }
            else
            {
                Debug.Log("No rigidbody found on the thrown object");
            }
        }
    }

    private void ChangeAnimation()
    {
        if (moveDirection == Vector3.zero)
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
                if (moveDirection.x > 0f && moveDirection.z > 0f)
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
                else if (moveDirection.x < 0f && moveDirection.z > 0f)
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
                else if (moveDirection.x > 0f && moveDirection.z < 0f)
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
                else if (moveDirection.x < 0f && moveDirection.z < 0f)
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
                else if (moveDirection.z > 0f)
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
                else if (moveDirection.z < 0f)
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
                if (moveDirection.x > 0f && moveDirection.z > 0f)
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
                else if (moveDirection.x < 0f && moveDirection.z > 0f)
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
                else if (moveDirection.x > 0f && moveDirection.z < 0f)
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
                else if (moveDirection.x < 0f && moveDirection.z < 0f)
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
                else if (moveDirection.z > 0f)
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
                else if (moveDirection.z < 0f)
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
