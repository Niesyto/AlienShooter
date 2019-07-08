using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/** @brief Managing the player's score */
public class ScoreManager : MonoBehaviour
{
    /** The player's score. */
    public static int score;       
    /** Score required do level up */
    public int scoreRequired;      
    /** Reference to Leveling up canvas */
    GameObject levelUpCanvas;      
    /** Reference to HUD canvas */
    GameObject hUDCanvas;          
    /** Reference to the player */
    GameObject player;             
    /** Reference to the enemies container */
    GameObject enemies;            
    /** Reference to the Text component. */
    Text text;                     
    /** Array of enemies */
    GameObject[] enemiesArray;      
    /** Reference to the player's position. */
    Transform playerTransform;      
    /** Reference to the player's health. */
    PlayerHealth playerHealth;      
    /** Reference to the player's movement. */
    Movement playerMovement;       
    /** Reference to the player's shooting script. */
    PlayerShooting playerShooting;   

    /** @brief Set up the references and starting score */
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

    /** @brief Set the displayed text to the current score and run a CheckLevel() */
    void Update ()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = score.ToString();
      
        CheckLevel();
    }

    /** @brief Check if the player has leveled up */
    void CheckLevel()
    {
        if(score>=scoreRequired)
        {
            scoreRequired*=(int)2.5;
            ShowLevelUpCanvas();
        }
    }

    /** @brief Display level up canvas */
    void ShowLevelUpCanvas()
    {
        hUDCanvas.SetActive(false);
        levelUpCanvas.SetActive(true);   
    }

    /** @brief Hide level up canvas */
    void HideLevelUpCanvas()
    {
        hUDCanvas.SetActive(true);
        levelUpCanvas.SetActive(false);
    }

    /** @brief Increase player's maximum health*/
    public void IncreaseMaxHealth()
    {
        playerHealth.startingHealth=(int)(playerHealth.startingHealth*1.5);
        playerHealth.currentHealth=(int)(playerHealth.currentHealth*1.5);
        HideLevelUpCanvas();
    }

    /** @brief Increase player's fire rate */
    public void IncreaseFireRate()
    {
        playerShooting.timeBetweenBullets*=0.9f;
        HideLevelUpCanvas();
    }

    /** @brief Increase player's damage*/
    public void IncreaseDamage()
    {
        playerShooting.damagePerShot=(int)(playerShooting.damagePerShot*1.5f);
        HideLevelUpCanvas();
    }

    /** @brief Increase player's movement speed*/
    public void IncreaseMoveSpeed()
    {
        playerMovement.speed*=1.1f;
        HideLevelUpCanvas();
    }
}