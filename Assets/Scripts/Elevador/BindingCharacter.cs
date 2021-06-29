using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingCharacter : MonoBehaviour
{
    public GameObject Parent;
    private GameObject character;
    private CharacterController controller;

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
      controller = character.GetComponent<CharacterController>();

    }

    private void OnTriggerStay(Collider other){
            if (other.transform.tag == "Player")
            {
            character.transform.SetParent(Parent.transform);
           // controller.Move(new Vector3(0,-10000*Time.deltaTime,0));
            Debug.Log("en la plataforma ");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player") {
            GameObject.FindGameObjectWithTag("Player").transform.SetParent(null);
            Debug.Log( "fuera de la plataforma ");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
