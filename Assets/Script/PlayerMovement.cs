using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float moveSPD;
    public float rotateSPD;

    public float sightRange;
    public LayerMask playerLayer;
    public LayerMask fridgeLayer;

    public GameObject fridge;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))  //allow forward and backward
        {
            rb.linearVelocity = transform.forward * moveSPD * Time.deltaTime;
           
        }
        if (Input.GetKey(KeyCode.S))  //allow forward and backward
        {
            rb.linearVelocity = -transform.forward * moveSPD * Time.deltaTime;
           
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))  //rotate
        {
            transform.Rotate(0, -rotateSPD * Time.deltaTime, 0);
           
        }
        if (Input.GetKey(KeyCode.D))  //rotate
        {
            transform.Rotate(0, rotateSPD * Time.deltaTime, 0);
            
        }

        DetectionRange();
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
            }
        }
    }
}
