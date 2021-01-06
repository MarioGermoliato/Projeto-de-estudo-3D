using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    private GameObject cameraFPS;
    Vector3 MoveDirection = Vector3.zero;
    private CharacterController controller;   
    public float moveSpeed, speedCam, jumpForce;
    private float rotationX, rotationY;
    

    
    public Transform groundCheck;




    // Start is called before the first frame update
    void Start()
    {
        cameraFPS = GetComponentInChildren<Camera>().transform.gameObject;
        controller = GetComponent<CharacterController>();

        cameraFPS.transform.localRotation = Quaternion.identity;
        cameraFPS.transform.localPosition = new Vector3(0, 0.7f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
        FPSCamController();

    }

    void Direction()
    {
        Vector3 directFront = new Vector3(cameraFPS.transform.forward.x, 0, cameraFPS.transform.forward.z);
        directFront *= Input.GetAxis("Vertical");


        Vector3 directRight = new Vector3(cameraFPS.transform.right.x, 0, cameraFPS.transform.right.z);
        directRight.Normalize();
        directRight *= Input.GetAxis("Horizontal");

        Vector3 directFinal = directFront += directRight;
        directFinal.Normalize();

        if (controller.isGrounded)
        {
            MoveDirection = new Vector3(directFinal.x, 0, directFinal.z) * moveSpeed;

            if(Input.GetButtonDown("Jump"))
            {
                MoveDirection.y = jumpForce;
                Debug.Log("Pulou");
            }
        }

        MoveDirection.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(MoveDirection * Time.deltaTime);
    }

    void FPSCamController()
    {
        rotationX += Input.GetAxis("Mouse X") * speedCam;
        rotationX = ClampAngle(rotationX, -360, 360);

        rotationY += Input.GetAxis("Mouse Y") * speedCam;
        rotationY = ClampAngle(rotationY, -80, 80);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        Quaternion finalRotation = Quaternion.identity * xQuaternion * yQuaternion;

        //cameraFPS.transform.localRotation = Quaternion.Lerp(cameraFPS.transform.localRotation, finalRotation, speedCam * Time.deltaTime);
        cameraFPS.transform.localRotation = finalRotation;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360)  { angle -= 360; }

        return Mathf.Clamp(angle, min, max);
    }
}
