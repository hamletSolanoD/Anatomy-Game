using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Control : Interactuable
{
    public enum Direccion { SUBIR, BAJAR};
    public Direccion direccion;
    public Elevador elevador;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Panel_Accionado() {
        elevador.PanelControl(direccion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void  InteractuarClickIzquierdo()
    {
        Panel_Accionado();
    }
    public override void InteractuarClickDerecho()
    {
        Panel_Accionado();
    }
}
