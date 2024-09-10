
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_3D_mov : MonoBehaviour
{
    public float speed = 5.0f;

    public float jumpForce = 10.0f;

    private Vector3 moveDirection = Vector3.zero;

    private float gravity = 20.0f;

    CharacterController controller;

    public bool isWalking;


    //public GameObject player;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        isWalking = false;
  
    }


    void Update()
{
    // Check if the player is grounded
    if (controller.isGrounded)
    {
        // Get movement input
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        // Handle jump input
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpForce;
        }
    }


    if (moveDirection.x != 0 || moveDirection.z != 0)
    {
        // Compute the rotation only on the horizontal plane (ignore y-axis for movement rotation)
        Vector3 flatMovement = new Vector3(moveDirection.x, 0.0f, moveDirection.z);
        Quaternion targetRotation = Quaternion.LookRotation(flatMovement);
        
        // Rotate the player smoothly towards the target direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    // Apply gravity
    moveDirection.y -= gravity * Time.deltaTime;

    // Move the character controller
    controller.Move(moveDirection * Time.deltaTime);
}

    


}
