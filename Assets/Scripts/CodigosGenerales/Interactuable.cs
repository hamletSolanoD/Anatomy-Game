using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactuable : MonoBehaviour
{
    [Range(1,100)]
    public float Radio_Interaccion = 10;
    
    private void Start()
    {
    
    }
    public abstract void InteractuarClickDerecho();
    public abstract void InteractuarClickIzquierdo();

    public void EnfocarObjeto()
    {
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direccion = (transform.position - Player.transform.position).normalized;
        direccion.y = 0;
        Quaternion destino = Quaternion.LookRotation(direccion);
        Player.transform.rotation = destino;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radio_Interaccion);
        
    }




}