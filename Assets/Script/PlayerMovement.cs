using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rb;
    public float moveSPD;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    bool grounded;

    public float sightRange;
    public LayerMask playerLayer;
    public LayerMask fridgeLayer;

    public Camera firstPersonCamera;
    PlayerCam cam;

    public GameObject fridge;
    public Transform orientation;
    public GameObject textUI;
    public Transform handPosition;
    bool isHolding;
    GameObject heldObject;

    public bool fridgeActive;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    int ingredientAdded;

    public GameObject spaget;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       cam = firstPersonCamera.GetComponent<PlayerCam>();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {

        MovePlayer();
    }
    private void Update()
    {
     
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
        Debug.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward * sightRange, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out hit, sightRange, fridgeLayer))
        {
            Debug.Log("Something Found");

            if(!fridgeActive)
                textUI.SetActive(true);

            if (hit.collider != null) //if found something
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.tag == "Fridge")
                    {
                        fridgeActive = !fridgeActive;
                        fridge.SetActive(fridgeActive); //set fridge UI true
                        cam.canLook = !fridgeActive;
                        textUI.SetActive(false);
                    }

                    if (hit.collider.tag == "Pickup")
                    {
                        if (!isHolding)
                        {
                            heldObject = hit.collider.gameObject;

                            hit.transform.SetParent(handPosition);
                            hit.transform.localPosition = Vector3.zero;
                            hit.transform.localRotation = Quaternion.identity;
                            hit.transform.GetComponent<BoxCollider>().enabled = false;
                            Debug.Log("Take the cheese!!!");
                        }
                        //if we hit a place / pot
                        //if we're holding something
                        //take our held object and destroy it 
                        //set is holding to false.
                        //maybe a script on th epot to tell if we have all the ingredients?
                    }
                    if (hit.collider.tag == "Sauce")
                    {
                        if (!isHolding)
                        {
                            heldObject = hit.collider.gameObject;

                            hit.transform.SetParent(handPosition);
                            hit.transform.localPosition = Vector3.zero;
                            hit.transform.localRotation = Quaternion.identity;
                            hit.transform.GetComponent<BoxCollider>().enabled = false;

                            Debug.Log("Take the sauce!!!");
                        }
                       
                    }

                    if (hit.collider.tag == "Pot")
                    {
                        if (heldObject)
                        {
                            if (heldObject.tag == "Sauce")
                            {
                                if(ingredientAdded >= 2)
                                {
                                    Debug.Log("All done!");
                                    Destroy(heldObject);
                                    
                                }
                            }
                            else
                            {
                                ingredientAdded++;
                                Destroy(heldObject);
                            }
                        }
                    }
                }

            }
            
        }
        else
        {
            textUI.SetActive(false);
        }
    }
}
