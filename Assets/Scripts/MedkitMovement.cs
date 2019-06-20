using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitMovement : MonoBehaviour
{
    float timer;
    float amplitude = 0.2f;
    public float step;
    float stepCounter;
    public float rotationStep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (stepCounter >= amplitude || stepCounter <= -amplitude)
        {
            step *= (-1);
        }
        transform.position += new Vector3(0, step, 0);
        stepCounter += step;
        transform.Rotate(0, rotationStep, 0);
    }
}
