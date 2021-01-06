using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    Vector3 foward, strafe, vertical;
    
    public float gravity, jumpSpeed, maxJumpHeight, timeToMaxHeight;
    public float speedWalk;

    public int intAnimator;
    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();        
        JumpSettings();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void JumpSettings()
    {
        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;
    }
    void Movement()
    {
        float fowardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        foward = fowardInput * speedWalk * transform.forward;
        strafe = strafeInput * speedWalk * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if(controller.isGrounded)
        {
            vertical = Vector3.down;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //vertical = jumpSpeed * Vector3.up;
            }

            if (fowardInput !=0 || strafeInput !=0)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    intAnimator = 2;
                    speedWalk = 7;
                }
                else
                {
                    intAnimator = 1;
                    speedWalk = 4;
                }
            }
            else
            {
                intAnimator = 0;
            }
        }

        if(vertical.y > 0 && (controller.collisionFlags &CollisionFlags.Above) !=0)
        {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = foward + strafe + vertical;
        controller.Move(finalVelocity * Time.deltaTime);

        playerAnimator.SetInteger("intWalk", intAnimator);
    }
}
