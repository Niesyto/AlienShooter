using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyMovement : MonoBehaviour
{

    public float speed = 0.5f;            // The speed that the enemy will move at.
    public float lenth = 20;
	public float xPar = 0.05f;
	public float zPar = 0.05f;
    float pathLenth;
    bool wayBool = true;
    Vector3 movement;                   // The vector to store the direction of the enemy's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the enemy's rigidbody.

    void Awake()
    {
        // Set up references.
        pathLenth = lenth;
        playerRigidbody = GetComponent<Rigidbody>();
 	    anim = GetComponent<Animator>();
	   
    }


    void FixedUpdate()
    {
        // Store the input axes.
       	float x = 0.0f;
    	float z = 0.0f;
        
        if (wayBool)
        {
            z += zPar;
		x += xPar;
        }
        else
        {
            z -= zPar;
		x -= xPar;
        }

        pathLenth -= 0.1f;

        if (pathLenth <= 0)
        {
            pathLenth = lenth;
            wayBool = !wayBool;
            Turning();
        }

        Move(x, z);

        
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);

        Animating(h,v);
    }

    void Turning()
    {
        playerRigidbody.rotation *= Quaternion.Euler(0, 180f, 0);
    }


    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("isWalking", walking);

    }

}
