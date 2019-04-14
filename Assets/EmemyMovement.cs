using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyMovement : MonoBehaviour
{

    public float speed = 1f;            // The speed that the player will move at.
    public float lenth = 20;
    float pathLenth;
    bool wayBool = true;
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animation anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    void Awake()
    {
        // Set up references.
        pathLenth = lenth;
        anim = GetComponent<Animation>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        if (wayBool)
        {
            v += 0.1f;
        }
        else
        {
            v -= 0.1f;
        }

        pathLenth -= 0.1f;

        if (pathLenth <= 0)
        {
            pathLenth = lenth;
            wayBool = !wayBool;
            Turning();
        }

        Move(h, v);

        //// Turn the player to face the mouse cursor.
        //Turning();

        // Animate the player.
        Animating(h, v);

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

    void Animating(float h, float v)
    {
        anim.Play("Walk");
    }
}
