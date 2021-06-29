using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Stadisticas))]


public class Character : MonoBehaviour
{

    protected Stadisticas yo;
    protected float Retardo;
    protected AtaqueEspecial[] AtaquesEspeciales;
  

    // Start is called before the first frame update
    void Start()
    {
        yo = GetComponent<Stadisticas>();
        AtaquesEspeciales = GetComponents<AtaqueEspecial>();
    }
    public Stadisticas GetStats() {
        return yo;
    }
    public string GetNombre() {
        return yo.Nombre;
    }
 
    public virtual void Atacar(Character Enfocado)
    {
        
        if (Retardo <= 0)
        {
      
            yo.AnimacionAtaque();
            Enfocado.Recibir(yo.Ataque, this);
            Retardo = yo.Retardo_Ataque;// espera ataque normal
        }
    }
    public virtual void Ataque_Especial(Character Enfocado, bool Animacion) {
        int NumeroAtaques = AtaquesEspeciales.Length;
        int ataque = Random.Range(0,NumeroAtaques);
        if ( Retardo <= 0)
        {
            Retardo = AtaquesEspeciales[ataque].Tiempo_Recuperacion; //espera ataque especial
           if(Animacion) yo.AnimacionEspecial(AtaquesEspeciales[ataque].NombreDeAnimacion);
           // ejecuta animacion especial si deja, para clases heredadas
            AtaquesEspeciales[ataque].Ataque(Enfocado);// ejecuta codigo especial
            Enfocado.Recibir(AtaquesEspeciales[ataque].Daño, this); // dar danio con el ataque especial
        }
    }
    public virtual void Ataque_Especial(Character Enfocado)
    {
        int NumeroAtaques = AtaquesEspeciales.Length;
        int ataque = Random.Range(0, NumeroAtaques);
        if (Retardo <= 0)
        {
            Retardo = AtaquesEspeciales[ataque].Tiempo_Recuperacion; //espera ataque especial
            yo.AnimacionEspecial(AtaquesEspeciales[ataque].NombreDeAnimacion);// ejecuta animacion especial
            AtaquesEspeciales[ataque].Ataque(Enfocado);// ejecuta codigo especial
            Enfocado.Recibir(AtaquesEspeciales[ataque].Daño, this); // dar danio con el ataque especial
        }
    }

    protected virtual void KnockBack(Character Recibir) {
       GetComponent<Enemigos>().KnockBack(Recibir.transform.forward, Recibir.GetStats().KnockBack);
    }


    public virtual void Recibir(float ataque,Character Recibir)
    {
        KnockBack(Recibir);
        yo.Vida -= ataque;

        if (yo.Vida <= 0) { Morir(); }
    }
    public virtual void Recibir_ConKnockBackEspecial(float ataque,float knockBack,Vector3 DirKncockback)
    {
        GetComponent<Enemigos>().KnockBack(DirKncockback, knockBack);
        yo.Vida -= ataque;
        if (yo.Vida <= 0) { Morir(); }
    }
    protected virtual void Morir()
    {
        yo.AnimacionMorir();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Retardo -= Time.deltaTime;

       
    }      
}      

