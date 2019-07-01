using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;        // The player's score.
    public int scoreRequired;       // Score required do level up
    GameObject levelUpCanvas;       // Reference to Leveling up canvas
    GameObject hUDCanvas;           // Reference to HUD canvas
    GameObject player;              // Reference to the player
    GameObject enemies;             // Reference to the enemies container
    Text text;                      // Reference to the Text component.
    GameObject[] enemiesArray;       // Array of enemies

    Transform playerTransform;      // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    Movement playerMovement;        // Reference to the player's movement.
    PlayerShooting playerShooting;   // Reference to the player's shooting script.

    void Awake ()
    {
        // Set up the reference.
        text = GetComponent <Text> ();
        levelUpCanvas=GameObject.FindGameObjectWithTag("LevelUpUI");
        hUDCanvas=GameObject.FindGameObjectWithTag("HUDUI");
        player=GameObject.FindGameObjectWithTag("Player");
        enemies=GameObject.FindGameObjectWithTag("Enemy");

        playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        playerMovement=player.GetComponent <Movement> ();
        playerShooting=player.GetComponentInChildren <PlayerShooting> ();

        levelUpCanvas.SetActive(false);
        // Reset the score.
        score = 0;
        scoreRequired=50;
    
    }


    void Update ()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = score.ToString();
      
        CheckLevel();
    }

    void CheckLevel()
    {
        if(score>=scoreRequired)
        {
            scoreRequired*=(int)2.5;
            PauseGame();
        }
    }


    void PauseGame()
    {
        /* 
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
 
        foreach(GameObject go in enemiesArray)
        {
            go.SetActive(false);
        }
        */


        hUDCanvas.SetActive(false);
        levelUpCanvas.SetActive(true);
        //player.SetActive(false);
    }

    void UnpauseGame()
    {
        
        /* 
        foreach(GameObject go in enemiesArray)
        {
            go.SetActive(true);
        }
        */
        hUDCanvas.SetActive(true);
        levelUpCanvas.SetActive(false);
        //player.SetActive(true);
    }

    public void IncreaseMaxHealth()
    {
        playerHealth.startingHealth=(int)(playerHealth.startingHealth*1.5);
        playerHealth.currentHealth=(int)(playerHealth.currentHealth*1.5);
        UnpauseGame();
    }

    public void IncreaseFireRate()
    {
        playerShooting.timeBetweenBullets*=0.9f;
        UnpauseGame();
    }

    public void IncreaseDamage()
    {
        playerShooting.damagePerShot=(int)(playerShooting.damagePerShot*1.5f);
        UnpauseGame();
    }

    public void IncreaseMoveSpeed()
    {
        playerMovement.speed*=1.1f;
        UnpauseGame();
    }
}