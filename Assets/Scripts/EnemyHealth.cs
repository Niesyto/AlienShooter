using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip zombieDeath;

    Animator anim;                              // Reference to the animator.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    EmemyMovement enemyMovement;                // Reference to enemy movement module
    ParticleSystem bloodParticles;              // Reference to the particle renderer
    AudioSource zombieAudio;                    // Reference to the audio source.
    UnityEngine.AI.NavMeshAgent nav;   

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

    void Update ()
    {
      
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if(isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;
        //bloodParticles.Shape.rotation = hitPoint;
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