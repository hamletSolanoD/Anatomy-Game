using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estacion : Interactuable
{
    public int Numero_Estacion;
    public Elevador Elevador;
    public GameObject Cordenadas_Estacion;

    // Start is called before the first frame update
    void Start()
    {
        Elevador.AgregarEstacion(Numero_Estacion, Cordenadas_Estacion.transform.position);
    }
    private void SolicitarEstacion() {
        Elevador.solicitarPlataforma(Numero_Estacion);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InteractuarClickDerecho()
    {
        SolicitarEstacion();
    }
    public override void InteractuarClickIzquierdo()
    {
        SolicitarEstacion();
    }
}

