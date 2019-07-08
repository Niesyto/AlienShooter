using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class responsible for handling enemy's health, damage taking and dying  */
public class EnemyHealth : MonoBehaviour
{
    /** The amount of health the enemy starts the game with. */
    public int startingHealth = 100;            
    /** The current health the enemy has. */
    public int currentHealth;                  
    /** The amount added to the player's score when the enemy dies. */
    public int scoreValue = 10;                
    /** Reference to zombie death audio sound */
    public AudioClip zombieDeath;
    /** Reference to the animator. */
    Animator anim;                            
    /** Reference to the capsule collider. */
    CapsuleCollider capsuleCollider;           
    /** Whether the enemy is dead. */
    bool isDead;
    /** Reference to enemy movement module */                               
    EmemyMovement enemyMovement;
    /** Reference to the particle renderer */                
    ParticleSystem bloodParticles;  
    /** Reference to the audio source. */            
    AudioSource zombieAudio;      
    /** Reference to navigation mesh agent */           
    UnityEngine.AI.NavMeshAgent nav;   

     /** @brief Setting up the references. */
    void Awake ()
    {
        // Setting up the references.
        anim = GetComponent <Animator> ();
       
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();


        enemyMovement= GetComponent <EmemyMovement> ();

        bloodParticles = GetComponent<ParticleSystem> ();

        zombieAudio = GetComponent<AudioSource>();

        capsuleCollider = GetComponent <CapsuleCollider> ();


        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

     /** @brief Take damage 
     @param amount Amount of damage taken
     @param hitPoint Direction from which the attack came
     */
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if(isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;
        
        var particleShape= bloodParticles.shape;                 // Reference to the shape of particle system


        particleShape.rotation=gameObject.transform.eulerAngles+hitPoint;
        bloodParticles.Play();    

        
        // If the current health is less than or equal to zero...
        if(currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death ();
            
        }
        zombieAudio.Play();
        

    }

    /** @brief Play death animation, and disable everything
     */
    void Death ()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        anim.SetTrigger ("Dead");

        enemyMovement.enabled=false;
        nav.enabled = false;
        zombieAudio.clip = zombieDeath;
        
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }

}