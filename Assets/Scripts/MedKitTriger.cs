using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitTriger : MonoBehaviour
{
    Transform player;               
    PlayerHealth playerHealth;  // Reference to the player's heath.

    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectWithTag ("Player").transform;
         playerHealth = player.GetComponent <PlayerHealth> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            double healValue=playerHealth.startingHealth *0.2;
            playerHealth.currentHealth +=(int)healValue;
            if(playerHealth.currentHealth>playerHealth.startingHealth)
                playerHealth.currentHealth=playerHealth.startingHealth;
            Destroy(transform.parent.gameObject);
        }
    }
}
