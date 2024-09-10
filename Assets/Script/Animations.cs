using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    // Start is called before the first frame update

    Animator Player_animator;

    public float rotationSpeed = 720f; // Vitesse de rotation du personnage

    void Awake()
    {
        Player_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Player_animator.SetFloat("Walk", 1);

        }
        if (Input.GetKey(KeyCode.Space))
        {
            Player_animator.SetBool("Jump",true);
        }
        else
        {
            Player_animator.SetFloat("Walk", 0);
            Player_animator.SetBool("Jump", false);
        }
    }
}
