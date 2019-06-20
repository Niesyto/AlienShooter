using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    public GameObject trigger;
    public GameObject lights;

    float timer;
    float switchDelay = 0.5f;
    bool switchOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && timer >= switchDelay) 
        {
            timer = 0f;
            switchOn = !switchOn;
            for (int i = 0; i < lights.transform.childCount; i++)
            {
                lights.transform.GetChild(i).gameObject.GetComponent<Light>().intensity = switchOn ? 1 : 0;
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
