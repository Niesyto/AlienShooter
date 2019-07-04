using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 4f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    EnemyHealth enemyHealth;                // Reference to zombie's health
    EnemyAttack enemyAttack;                // Reference to zombie's attack
    int scoreDivider;                       // Variable used for dividing score to scale enemies

    void Start ()
    {
        // Call the Spawn function
        StartCoroutine(Spawn(spawnTime) ) ;

        scoreDivider = 100;

        enemyHealth = enemy.GetComponent <EnemyHealth> ();
        enemyAttack = enemy.GetComponent <EnemyAttack> ();
    }

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

    public void ResetZombies()
    {
        enemyHealth.startingHealth=100;
        enemyAttack.attackDamage=10;
        enemyHealth.scoreValue=10;
    }
}