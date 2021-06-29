using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemigos : Interactuable
{
    
    private Character thisEnemigo;
    private PlayerCharacter Player;
    private NavMeshAgent Nav;

    [SerializeField]
    [Range(1, 100)]
    private float Radio_Ataque;
    [SerializeField]
    [Range(1, 100)]
    private float RangoFiebre = 2;

    private Slider BarraVida;
    private float SmoothKnockBack = 8;
    private Vector3 knockbackforce;
    private Vector3 knockbackforceFinal;
    private Vector3 knockbackforceHelper;
    // Start is called before the first frame update

    public Character GetCharacter() { return thisEnemigo; }
    void Start()
    {
        thisEnemigo = GetComponent<Character>();
        Nav = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        Canvas canvas = GetComponentInChildren<Canvas>();
        BarraVida = canvas.GetComponentInChildren<Slider>();
        BarraVida.maxValue = thisEnemigo.GetStats().Vida;

    }
    // Update is called once per frame
    void Update()
    {

        BarraVida.value = thisEnemigo.GetStats().Vida;

        //rango de ataque del enemigos
        if (Vector3.Distance(Player.transform.position, transform.position) <= Player.getRangoDeAtaque() && thisEnemigo.GetStats().Vida > 0) 
        {
            Player.AgregarEnemigoAlRango(thisEnemigo);
        }
        else {
            Player.QuitarEnemigoDelRango(thisEnemigo);
        }


        if (Vector3.Distance(transform.position, Player.transform.position) <= Radio_Ataque)
        {
            Nav.SetDestination(Player.transform.position);
        }
        if (Vector3.Distance(transform.position, Player.transform.position) <= Radio_Interaccion) {
            thisEnemigo.Atacar(Player);
            //EnfocarObjeto();
        }
        kncockBack();
    }
    private void kncockBack()
    {
        ///knock back
        knockbackforce = Vector3.SmoothDamp(knockbackforce, knockbackforceFinal, ref knockbackforceHelper, Time.deltaTime * SmoothKnockBack);
        if (knockbackforce == knockbackforceFinal)
        {
            knockbackforceFinal = Vector3.zero;
            knockbackforce = Vector3.zero;
        }
        else
        { transform.Translate(knockbackforce, Space.World); }
    }   
    public override void InteractuarClickIzquierdo() {
        Player.Atacar(thisEnemigo);
    }
    public override void InteractuarClickDerecho()
    {
        Player.Ataque_Especial(thisEnemigo);
    }
    public void KnockBack(Vector3 direccion, float knockbackMultiplier) {

        knockbackMultiplier = knockbackMultiplier/8;
        direccion = direccion.normalized;
        knockbackforceFinal = direccion * knockbackMultiplier;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangoFiebre);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radio_Interaccion);   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio_Ataque);

    }
}
