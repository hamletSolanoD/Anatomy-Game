using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Stadisticas : MonoBehaviour
{
    public float Vida;
    public float Ataque;
    public float KnockBack = 1;
    public float Retardo_Ataque = 0.5f;
    public string Nombre;
   

    public string Nombre_Animacion_De_Ataque = "Atacar";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AnimacionAtaque() {
        animator.Play(Nombre_Animacion_De_Ataque);

    }
    public Animator GetAnimator() {
        return animator;
    }
    public void AnimacionEspecial(string Nombre)
    {
        animator.Play(Nombre);

    }
    public void AnimacionRecibirDanio()
    {
        animator.Play("Recibir");


    }
    public void AnimacionMorir() {
        animator.Play("Morir");

    }
}
