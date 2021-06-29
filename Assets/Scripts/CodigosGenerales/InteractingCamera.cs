using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class InteractingCamera : MonoBehaviour
{

    private Camera camara;
    private Interactuable ObjetoEnfocado;
    private CameraController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CameraController>();
        camara = Camera.main;
    }

   private void Interaccion(Ray Dedo,int Boton)
    {
        RaycastHit Trayecto;
        if (Physics.Raycast(Dedo, out Trayecto))
        {
            Interactuable objetoInteractuable = Trayecto.transform.GetComponent<Interactuable>();
            if (objetoInteractuable != null) {

                if (Trayecto.distance- controller.Distancia_Camara <= objetoInteractuable.Radio_Interaccion)
                {


                    if (Boton == 0)
                    {
                        ObjetoEnfocado = objetoInteractuable;
                        objetoInteractuable.EnfocarObjeto();
                        objetoInteractuable.InteractuarClickIzquierdo();
                    }
                    if (Boton == 1)
                    {
                        ObjetoEnfocado = objetoInteractuable;
                        objetoInteractuable.EnfocarObjeto();
                        objetoInteractuable.InteractuarClickDerecho();
                    }
                }
        }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray click = camara.ScreenPointToRay(Input.mousePosition);
            Interaccion(click,0);
        }
         if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray click = camara.ScreenPointToRay(Input.mousePosition);
            Interaccion(click, 1);
        }




    }
}
        
    

