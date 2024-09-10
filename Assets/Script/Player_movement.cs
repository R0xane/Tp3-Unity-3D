using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame updat

    public  bool isGrounded;


    public GameObject player;

    private Rigidbody rb;

    private bool isWalking;

    private bool isIdle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        movement.Normalize();


        transform.Translate(movement * Time.deltaTime * 5, Space.World);

        if(movement != Vector3.zero)
        {
            player.GetComponent<Animator>().SetBool("Walk", true);
            //player.GetComponent<Animator>().SetBool("Idle", false);
        
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation,720f* Time.deltaTime);
        }

        else
        {
            player.GetComponent<Animator>().SetBool("Walk", false);
            //player.GetComponent<Animator>().SetBool("Idle", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
            player.GetComponent<Animator>().SetBool("Jump", true);
            rb.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
            }

        }
        else
        {
            player.GetComponent<Animator>().SetBool("Jump", false);
            // player.GetComponent<Animator>().SetBool("Idle", true);
        }

        

        /*if (Input.GetKey(KeyCode.W))
        {
            rb.transform.position += new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.transform.position += new Vector3(0, 0, -0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }*/


        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }


}
