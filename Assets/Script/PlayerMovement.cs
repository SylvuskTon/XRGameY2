using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rb;
    public float moveSPD;
    //public float rotateSPD;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    bool grounded;

    public float sightRange;
    public LayerMask playerLayer;
    public LayerMask fridgeLayer;

    public GameObject fridge;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        // if (Input.GetKey(KeyCode.W))  //allow forward and backward
        // {
        //    rb.linearVelocity = transform.forward * moveSPD * Time.deltaTime;

        //  }
        //  if (Input.GetKey(KeyCode.S))  //allow forward and backward
        //  {
        //      rb.linearVelocity = -transform.forward * moveSPD * Time.deltaTime;

        //  }
        MovePlayer();
    }
    private void Update()
    {
        // if (Input.GetKey(KeyCode.A))  //rotate
        // {
        //     transform.Rotate(0, -rotateSPD * Time.deltaTime, 0);

        //  }
        // if (Input.GetKey(KeyCode.D))  //rotate
        //  {
        //      transform.Rotate(0, rotateSPD * Time.deltaTime, 0);

        //   }
        DetectionRange();

        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        MyInput();
        SpeedControl();

        //handle drag
        if (grounded)
        {
            rb.linearDamping = groundDrag;

        }
        else
            rb.linearDamping = 0;
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSPD * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        //limit velocity if needed
        if(flatVel.magnitude > moveSPD)
        {
            Vector3 limitedVel = flatVel.normalized * moveSPD;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    void DetectionRange()
    {
        Debug.DrawRay(transform.position, transform.forward * sightRange, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, sightRange, fridgeLayer))
        {
            Debug.Log("Something Found");
            if (hit.collider != null) //if found something
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("EEEEEEEEEEEEEEE");
                    fridge.SetActive(true); //set fridge UI true
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("RRRRRRRRRR");
                    fridge.SetActive(false); //set fridge UI false
                }
            }
        }
    }
}
