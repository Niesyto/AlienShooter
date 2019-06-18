using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyMovement : MonoBehaviour
{

    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the enemy's rigidbody.
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.


    void Awake()
    {
        // Set up references.
        playerRigidbody = GetComponent<Rigidbody>();
 	    anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	   
    }

    void Update ()
    {
        // If the enemy and the player have health left...
        if(playerHealth.currentHealth > 0 && enemyHealth.currentHealth>=0)
        {
            anim.SetBool("isWalking", true);
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination (player.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    } 
  
}
