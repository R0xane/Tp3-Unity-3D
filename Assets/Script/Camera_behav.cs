using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_behav : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    private float min = 55f;
    private float max = 95f;
    private float scrollSpeed = 10f;
    
    private Vector2 turn;
    private float verticalAngle; // Variable to track vertical angle

    private float maxVerticalAngle = 80f; // Max upward angle
    private float minVerticalAngle = -10f; // Max downward angle (limiting how far below the player the camera can go)

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = max;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView -= scroll * scrollSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, min, max);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            movingCamera_wt_player();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            movingCamera_w_player();
        }
    }

    private void movingCamera_wt_player()
    {
        turn.x = Input.GetAxis("Mouse X") * Time.deltaTime * 10f;
        turn.y = Input.GetAxis("Mouse Y") * Time.deltaTime * 10f;

        float newVerticalAngle = verticalAngle - turn.y; 

        if (newVerticalAngle < maxVerticalAngle && newVerticalAngle > minVerticalAngle)
        {
            transform.RotateAround(player.transform.position, transform.right, -turn.y);
            verticalAngle = newVerticalAngle; 
        }

        transform.RotateAround(player.transform.position, Vector3.up, turn.x);
    }

    private void movingCamera_w_player()
    {
        turn.x = Input.GetAxis("Mouse X") * Time.deltaTime * 10f;
        player.transform.Rotate(Vector3.up * turn.x, Space.World);
    }
}
