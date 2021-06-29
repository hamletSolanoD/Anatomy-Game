using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRay : MonoBehaviour
{
    bool ground;
    // Start is called before the first frame update
    void Start()
    {
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") {

            ground = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            ground = false;
        }
    }

    public bool InGround() {
       
        return ground;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
