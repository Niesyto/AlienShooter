using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSlidingDoorScript : MonoBehaviour
{
    public GameObject trigger;
    public GameObject leftWing;
    public GameObject rightWing;

    float timer;

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
            timer = 0f;
            SlideDoors(true);
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
         timer += Time.deltaTime;
         if(timer>=2f)
         {
            SlideDoors(false);
            timer = 0f;
         }
    }
}
