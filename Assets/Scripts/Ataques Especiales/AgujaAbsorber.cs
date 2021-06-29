using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerCharacter))]
public class AgujaAbsorber : AtaqueEspecial
{
    public int Racha = 7;
    public Text RachaText;
    public float AttackMultiplier = 3;
    private int CombosCount;
    private string Anterior;

    private PlayerCharacter character;

    public override void Ataque(Character Enfocados)
    {
        string actual = Enfocados.GetNombre();
        if (gameObject.tag == "Player")
        {
            Debug.Log("Usando Aguja Absorber");

            if (Anterior != actual && Anterior != null && Anterior != "NINGUNO")
            {
                CombosCount = 1;
                Anterior = "NINGUNO";
             
                RachaText.color = Color.black;
                character.SetRacha("NINGUNO", 1);// reset a la racha, multiplier 1 para no afectar los ataques 
            } //normales  
            else { CombosCount++; }

                if (CombosCount >= Racha)
                {
                    character.SetRacha(actual, AttackMultiplier);
                    RachaText.color = Color.red;
                }
                Anterior = actual;


        }
    }
    

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<PlayerCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            RachaText.text = Anterior + ": " + CombosCount;



        }
    
}
