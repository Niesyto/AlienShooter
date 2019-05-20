using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyMovement : MonoBehaviour
{

    public float speed = 1f;            // The speed that the player will move at.
    public float lenth = 20;
	public float xPar = 0.05f;
	public float zPar = 0.05f;
    float pathLenth;
    bool wayBool = true;
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    void Awake()
    {
        // Set up references.
        pathLenth = lenth;
        playerRigidbody = GetComponent<Rigidbody>();
 	anim = GetComponent<Animator>();
	anim.SetInteger("State", 1);
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
    }

    void Turning()
    {
        playerRigidbody.rotation *= Quaternion.Euler(0, 180f, 0);
    }
}
