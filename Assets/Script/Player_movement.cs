using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool isGrounded;
    public GameObject player;
    private Rigidbody rb;

    private floating_health healthBar;
    public  float maxHealth = 10, health;

    private EnemyMovement enemyScript;

    public int dead;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        enemyScript = GetComponent<EnemyMovement>();
        healthBar = GetComponentInChildren<floating_health>();  
        health = maxHealth;
        dead = 0;

    }


    void Update()
    {
        if (dead==0){// Mouvement horizontal et vertical
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontal, 0, vertical);
            movement.Normalize();

            // Vitesse par défaut pour marcher
            float speed = 5f;

            // Animation de marche
            if (movement != Vector3.zero)
            {
                player.GetComponent<Animator>().SetBool("Walk", true);
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, 720f * Time.deltaTime);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("Walk", false);
            }

            // Animation de course
            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.GetComponent<Animator>().SetBool("Run", true);
                speed = 10f; // Augmenter la vitesse quand on court
                player.GetComponent<Animator>().SetBool("Walk", false);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("Run", false);
                
            }

            // Appliquer le mouvement au personnage
            transform.Translate(movement * Time.deltaTime * speed, Space.World);

            // Animation de saut
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                player.GetComponent<Animator>().SetBool("Jump", true);  // Utiliser SetTrigger pour éviter les conflits
                rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
                isGrounded = false;
            }
            else {
                player.GetComponent<Animator>().SetBool("Jump", false);
            }

            // Animation d'attaque
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.GetComponent<Animator>().SetBool("Attacking", true);  // Utiliser SetTrigger pour l'attaque
            }
            else{
                player.GetComponent<Animator>().SetBool("Attacking", false);
            }
        }

        else
        {
            player.GetComponent<Animator>().SetBool("Death", true);

            player.GetComponent<Animator>().SetBool("Attacking", false);
            player.GetComponent<Animator>().SetBool("Run", false);
            player.GetComponent<Animator>().SetBool("Walk", false);
            player.GetComponent<Animator>().SetBool("Jump", false);
        }
    }
    

    // Détection de la collision avec le sol pour réactiver le saut
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Ennemy")
        {
            health -= 1;
            healthBar.UpdateHealth(health, maxHealth);
            if (health <= 0)
            {
                dead = 1;
            }

            
        }
    }
}
