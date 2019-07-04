using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSlidingDoorScript : MonoBehaviour
{
    public GameObject trigger;
    public GameObject leftWing;
    public GameObject rightWing;

    Animator leftAnimator;
    Animator rightAnimator;
    // Start is called before the first frame update
    void Start()
    {
        leftAnimator = leftWing.GetComponent<Animator>();
        rightAnimator = rightWing.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
        {
            SlideDoors(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
        {
            SlideDoors(false);
        }

    }

    void SlideDoors(bool open)
    {
        leftAnimator.SetBool("slide", open);
        rightAnimator.SetBool("slide", open);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
