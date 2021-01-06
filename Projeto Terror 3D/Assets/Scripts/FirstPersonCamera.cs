using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform characterBody;
    public Transform characterHead;    
    private GameController _gameController;

    private float rotationX;
    public float rotationY;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.currentState == GameState.Inventory || _gameController.currentState == GameState.ItemInfo)
        {
            return;
        }
       else
        {
            RotationMouse();
        }
    }

    private void LateUpdate()
    {
        transform.position = characterHead.position;
    }

    void RotationMouse()
    {
        float verticalDelta = Input.GetAxisRaw("Mouse Y");
        rotationY = ClampAngle(rotationY, -80, 80);

        float horizontalDelta = Input.GetAxisRaw("Mouse X");
        rotationX = ClampAngle(rotationX, -360, 360);

        rotationX += horizontalDelta;
        rotationY += verticalDelta;

        characterBody.localEulerAngles = new Vector3(0, rotationX, 0);
        

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360) { angle -= 360; }

        return Mathf.Clamp(angle, min, max);
    }
}
