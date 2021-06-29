using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PlayerMovments))]
public class PlayerCharacter : Character
{
    [SerializeField]

    private Text Vida;
    private string RachaContra;
    private float Racha = 1; // RachaNormal por ataques consecutivos
    private float RachaPorTipo;// Racha por ataqueEspecial

    [SerializeField]
    private float EsperaPorEnfundar= 1.5f; //tiempo perdido por haber estado enfundado
    private bool Enfundado = true;
    [SerializeField]
    private float TiempoMinimoRacha = 5;
    [SerializeField]
    private float RachaMultiplier = 0.1f;

    private float ataquesTiempoRacha;// Controlador de tiempo por racha, si el ataque se hizo en menos de 
    //5 segundos la racha sigue
    private int AtaquesCombo;//controlador del tipo de animacion por combo
    private PlayerMovments Character;
    private ArrayList Enemigos = new ArrayList();

    [SerializeField]
    private float RangoDeAtaqueEnGrupo = 3f;// rango de ataque para ataques en grupo


    ////////////////////// FIEBRE /////////////////////////////////////
    private ArrayList EnemigosEnRangoFiebre = new ArrayList();






    public float getRangoDeAtaque() {
        return RangoDeAtaqueEnGrupo;
    }
    public void AgregarEnemigoAlRango(Character Enemigo) {
        if (!Enemigos.Contains(Enemigo))
        {
            Enemigos.Add(Enemigo);
        }
        }
    public void QuitarEnemigoDelRango(Character Enemigo) {
        if (Enemigos.Contains(Enemigo))
        {
            Enemigos.Remove(Enemigo);
        }

    }
    public void AgregarFiebre(Enemigos Enemigo) {
        EnemigosEnRangoFiebre.Add(Enemigo);
    }
    public void QuitarFiebre(Enemigos Enemigo) {
        EnemigosEnRangoFiebre.Remove(Enemigo);
    }
    public void SetFiebre() {
        if (EnemigosEnRangoFiebre.Count > 0) {


        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red ;
        Gizmos.DrawWireSphere(transform.position, RangoDeAtaqueEnGrupo);
    }
    private void establecerAnimacionDeAtaque() {
        //  4 ataques normales antes del ataque en rango   
        switch (AtaquesCombo) {
            case 1:
                yo.GetAnimator().SetFloat("TipoAtaque", 1); break;
            case 2:
                yo.GetAnimator().SetFloat("TipoAtaque", 2); break;
            case 3:
                yo.GetAnimator().SetFloat("TipoAtaque", 1); break;
            case 4:
                yo.GetAnimator().SetFloat("TipoAtaque", 2); break;

            case 5:
                yo.GetAnimator().SetFloat("TipoAtaque", 3); break;
            default:
                AtaquesCombo = 1;
                yo.GetAnimator().SetFloat("TipoAtaque", 1); break;

        }
        yo.AnimacionAtaque();
    }
    public override void Atacar (Character Enfocado)
    {
        if (Retardo <= 0)
        { 
            if (Enfundado)
            {
                AtaquesCombo = 1;// primerAtaque
                yo.AnimacionEspecial("Desenfundar");
                establecerAnimacionDeAtaque();
                Retardo += EsperaPorEnfundar;
                Enfundado = false;

            }
            else
            {
                AtaquesCombo++;
                establecerAnimacionDeAtaque();
            } 
            if (AtaquesCombo == 5) {// ataque en area por combo
                
                AtaqueDeGrupo(yo.Ataque,RangoDeAtaqueEnGrupo);
                Racha += RachaMultiplier;//Aumento De racha
                ataquesTiempoRacha = TiempoMinimoRacha;//racha 5 segundos desde el ultimo ataque
            }

            else { 
            if (RachaContra == Enfocado.GetNombre())
            {
                Enfocado.Recibir(yo.Ataque * RachaPorTipo, this);
                Racha += RachaMultiplier;//Aumento De racha
                ataquesTiempoRacha = TiempoMinimoRacha;//racha 5 segundos desde el ultimo ataque
            }
            else
            {
                Enfocado.Recibir(yo.Ataque * Racha, this);
                Racha += RachaMultiplier;//Aumento De racha
                ataquesTiempoRacha = TiempoMinimoRacha;//racha 5 segundos desde el ultimo ataque
               }
            }
            Retardo = yo.Retardo_Ataque;
        }
    }
    public void AtaqueDeGrupo(float Daño, float RangoKnockBack) {
        foreach (Character EnemigoEnArea in Enemigos)
        {
            if (EnemigoEnArea != null)
            {
                if (RachaContra == EnemigoEnArea.GetNombre())
                {

                    EnemigoEnArea.Recibir_ConKnockBackEspecial(Daño * RachaPorTipo, RangoKnockBack, EnemigoEnArea.transform.forward * -1);
                }
                else
                {
                    EnemigoEnArea.Recibir_ConKnockBackEspecial(Daño * Racha, RangoKnockBack, EnemigoEnArea.transform.forward * -1);
                }
            }
        }
    }
    public void RepelerEnemigos(float rango)
    {
        Debug.Log("PrintFloat is called with a value of " + rango);

        foreach (Character EnemigoEnArea in Enemigos)
        {
            if (EnemigoEnArea != null)
            {
                if (RachaContra == EnemigoEnArea.GetNombre())
                {

                    EnemigoEnArea.Recibir_ConKnockBackEspecial(0, rango, EnemigoEnArea.transform.forward * -1);
                }
                else
                {
                    EnemigoEnArea.Recibir_ConKnockBackEspecial(0, rango, EnemigoEnArea.transform.forward * -1);
                }
            }
        }
    }
        
    public override void Ataque_Especial(Character Enfocado)
    {
        AtaquesCombo = 1;//reset a los ataques
        Racha += RachaMultiplier;//Aumento De racha
        ataquesTiempoRacha = TiempoMinimoRacha;//racha 5 segundos desde el ultimo ataque
        yo.GetAnimator().SetFloat("TipoAtaque", 4);// ataque de  numero 4, ataque especial
        if (Retardo <= 0)
        {
            if (Enfundado)
            {
                Enfundado = false;
                yo.AnimacionEspecial("Desenfundar");
                base.Ataque_Especial(Enfocado, false);
                Retardo += EsperaPorEnfundar;
            }
            else {
                yo.AnimacionAtaque();
                base.Ataque_Especial(Enfocado,false); }
        }
    }

    public void SetRacha(string Tipo, float Multiplier) {
        RachaContra = Tipo;
        RachaPorTipo = Multiplier;
    }
    protected override void KnockBack(Character Recibir)
    {
        GetComponent<PlayerMovments>().Knock_Back(Recibir.transform.forward, Recibir.GetStats().KnockBack);


    }
    protected override void Morir()
    {
        yo.AnimacionMorir();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);



    }
    // Start is called before the first frame update
    public void Start()
    {
        yo = GetComponent<Stadisticas>();
        AtaquesEspeciales = GetComponents<AtaqueEspecial>();
        Character = GetComponent<PlayerMovments>();
    }
  
    // Update is called once per frame
    void Update()
    {

        ataquesTiempoRacha -= Time.deltaTime;
        if (ataquesTiempoRacha <= 0) { Racha = 1; }
        yo.GetAnimator().SetBool("Enfundar", Enfundado);
        Character.setEnfundado(Enfundado);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) Enfundado = true;
      
        Retardo -= Time.deltaTime;
     //   Debug.Log(yo.Vida);
     Vida.text = "Vida: "  + yo.Vida; 

    }
}
