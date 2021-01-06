using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControl : MonoBehaviour
{
    private FirstPersonCamera _firstPersonCamera;

    public Transform cotovelo;
    public GameObject flashLight;

    public bool flashlightOn;
    public float rotationX, rotationY;
    public float speedCam;

    // Start is called before the first frame update
    void Start()
    {
        _firstPersonCamera = FindObjectOfType(typeof(FirstPersonCamera)) as FirstPersonCamera;
    }

    // Update is called once per frame
    void Update()
    {
        //rotationY += Input.GetAxis("Mouse Y") * speedCam;
       // rotationY = ClampAngle(rotationY, -80, 80);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            flashlightOn = !flashlightOn;
            flashLight.SetActive(flashlightOn);
        }

        Naosei();
    }

    public void Naosei()
    {
        if(flashlightOn)
        {
            //cotovelo.localRotation = Quaternion.AngleAxis(rotationY + 65f, new Vector3(1, 0, 1));
            cotovelo.localRotation = Quaternion.AngleAxis(_firstPersonCamera.rotationY + 65f, new Vector3(1, 0, 1));
            
        }
        else
        {
            cotovelo.localRotation = Quaternion.Euler(0.13f, -0.402f, 5.142f);
        }
    }

    /*float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360) { angle -= 360; }

        return Mathf.Clamp(angle, min, max);
    }*/
}
