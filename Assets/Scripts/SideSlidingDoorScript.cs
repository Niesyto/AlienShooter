using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Handle door opening and closing*/
public class SideSlidingDoorScript : MonoBehaviour
{
    /** Reference to the door trigger */
    public GameObject trigger;
    /** Reference to the left wing*/
    public GameObject leftWing;
    /** Reference to the right wing*/
    public GameObject rightWing;
    /** Time counter */
    float timer;
    /** Reference to the left wing animator*/
    Animator leftAnimator;
    /** Reference to the right wing animator*/
    Animator rightAnimator;
    
    /** @brief Set up references*/
    void Start()
    {
        leftAnimator = leftWing.GetComponent<Animator>();
        rightAnimator = rightWing.GetComponent<Animator>();
    }

    /** @brief Open the door if the player or enemy enters the trigger
    @param collider Collider of the object entering the trigger*/
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
        {
            timer = 0f;
            SlideDoors(true);
        }
    }

   
    /** @brief Change the door position
    @param open Variable deciding whether the door is open*/
    void SlideDoors(bool open)
    {  
        leftAnimator.SetBool("slide", open);
        rightAnimator.SetBool("slide", open);
    }

    /** @brief Update the timer*/
    void Update()
    {
         timer += Time.deltaTime;
         if(timer>=2f)
         {
            SlideDoors(false);
            timer = 0f;
         }
    }
}
