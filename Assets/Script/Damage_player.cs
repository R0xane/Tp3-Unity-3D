using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_player : MonoBehaviour
{
    public bool attack;
    private float inputTimeout = 2f; // Time in seconds before attack is set to false
    private float timeSinceLastInput;

    void Start()
    {
        attack = false;
        timeSinceLastInput = 0f;
    }

    void Update()
    {
        // Check if any movement key is pressed
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            attack = false;
            timeSinceLastInput = 0f; // Reset the timer
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            attack = true;
            timeSinceLastInput = 0f; // Reset the timer
        }
        else
        {
            timeSinceLastInput += Time.deltaTime;
            if (timeSinceLastInput >= inputTimeout)
            {
                attack = false;
            }
        }
    }
}
