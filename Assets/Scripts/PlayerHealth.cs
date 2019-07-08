using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/** @brief Class responsible for handling player's health, damage taking and dying */
public class PlayerHealth : MonoBehaviour
{
    /** The amount of health the player starts the game with. */
    public int startingHealth = 100;                           
    /** The current health the player has. */
    public int currentHealth;                                  
    /** The audio clip to play when the player dies. */
    public AudioClip deathClip;                                 
    /** Reference to the Animator component. */
    Animator anim;                                             
    /** Reference to the AudioSource component. */
    AudioSource playerAudio;                                    
    /** Reference to the player's movement. */
    Movement playerMovement;                                  
    /** Reference to the shooting component */
    PlayerShooting playerShooting;                             
    /** Reference to the zombie spawn manager */
    public SpawnManager spawnManager;                                  
    /** True when the player is dead. */
    bool isDead;                                                
    /** True when the player gets damaged. */
    bool damaged;                                              


    /** @brief Setting up references */
    void Awake ()
    {
        // Setting up the references.
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <Movement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }

    /** @brief Reset the damage trigger */
    void Update ()
    {
       anim.ResetTrigger("Damage");
    }

    /** @brief Take some damage
    @param amount Amount of damage taken */
    public void TakeDamage (int amount)
    {

        // Tell the animator the player got damaged
        anim.SetTrigger ("Damage");

        // Reduce the current health by the damage amount.
        currentHealth -= amount;


        // Play the hurt sound effect.
        playerAudio.Play ();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }

    /** @brief Handling of player's death */
    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

       

        // Tell the animator that the player is dead.
        anim.SetTrigger ("Death");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play ();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerShooting.enabled = false;
        playerAudio.loop = false;

        spawnManager.ResetZombies();
    }        

    
}

