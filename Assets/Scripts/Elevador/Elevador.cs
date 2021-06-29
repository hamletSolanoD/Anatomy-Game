using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad = 4.5f;
    public float desface = 0;

    private Hashtable estaciones = new Hashtable();
    private Vector3  destino;
    private int posicion = 0;
   
    void Start()
    {
        destino = transform.position;
    }

    public  void AgregarEstacion(int numeroDeEstacion, Vector3 CordenadasEstacion) {
        estaciones.Add(numeroDeEstacion, CordenadasEstacion);    }

    public void solicitarPlataforma(int estacion) {
        destino = (Vector3)estaciones[estacion];
        posicion = estacion;
    }
    public void PanelControl(Panel_Control.Direccion dir ) {
        if (dir == Panel_Control.Direccion.SUBIR) {
         posicion = posicion < estaciones.Count ? posicion+1 : posicion ;
        }
        else if (dir == Panel_Control.Direccion.BAJAR){
            posicion = posicion > 0 ? posicion-1 : posicion;
        }
        destino = (Vector3)estaciones[posicion];

    }
    public Vector3 GetDirection() {
        return destino;
    }
    public float getVelocidad() {
        return velocidad * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    { 

        transform.position = Vector3.MoveTowards(transform.position,destino, velocidad*Time.deltaTime);

    }
}
