using UnityEngine;
using System.Collections;

public class MedKitSpawnManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject medKit;                // The enemy prefab to be spawned.
    public float spawnTime = 15f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

    void Start ()
    {
        // Call the Spawn function
        StartCoroutine(Spawn(spawnTime) ) ;
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
            Instantiate (medKit, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(spawnTime) ;
        }
    }
}