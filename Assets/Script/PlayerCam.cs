using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float senX;
    public float senY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public bool canLook;

    private void Start()
    {
       
        
    }

    private void Update()
    {
        if (!canLook)
            return;

        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
