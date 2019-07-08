using UnityEngine;
using System.Collections;

/** @brief Manager of enemy spawners */
public class SpawnManager : MonoBehaviour
{
    /** Reference to the player's heatlh. */
    public PlayerHealth playerHealth;       
    /** The enemy prefab to be spawned. */
    public GameObject enemy;               
    /** How long between each spawn. */
    public float spawnTime = 4f;          
    /** An array of the spawn points this enemy can spawn from. */
    public Transform[] spawnPoints;  
    /** Reference to zombie's health */       
    EnemyHealth enemyHealth;                
    /** Reference to zombie's attack */
    EnemyAttack enemyAttack;               
    /** Variable used for dividing score to scale enemies */
    int scoreDivider;                      

     /** @brief Call the Spawn function on start of the game and set up references */
    void Start ()
    {
        // Call the Spawn function
        StartCoroutine(Spawn(spawnTime) ) ;

        scoreDivider = 100;

        enemyHealth = enemy.GetComponent <EnemyHealth> ();
        enemyAttack = enemy.GetComponent <EnemyAttack> ();
    }

    /** @brief Modify the spawn rate and enemy prefab according to player's score */
    void Update ()
    {
        // Modify spawn rate according to player's score
        spawnTime=4f - (ScoreManager.score/scoreDivider);
        if(spawnTime<=0f)
            {
            // Upgrade enemies after reaching certain score
            spawnTime=4f;
            scoreDivider=scoreDivider*2;
            enemyHealth.scoreValue=enemyHealth.scoreValue*2;

            int randomEnemyUpgrade = Random.Range (0, 2);

            if(randomEnemyUpgrade==0)
                enemyHealth.startingHealth=(int)(enemyHealth.startingHealth*1.5);
            else
                enemyAttack.attackDamage=(int)(enemyAttack.attackDamage*1.5);
            }
    }

    /** @brief Spawn an enemy
    @param time Time to wait between spawning a next enemy */
    IEnumerator Spawn( float time )
    {
        yield return new WaitForSeconds(time) ;
        while( true )
        {
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                break;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(spawnTime) ;
        }
    }

    /** @brief Reset the enemy preset */
    public void ResetZombies()
    {
        enemyHealth.startingHealth=100;
        enemyAttack.attackDamage=10;
        enemyHealth.scoreValue=10;
    }
}