using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public float TiempoRepeticiones = 2f;
    public int Numero_Maximo_Enemigos;
    public GameObject Enemigo_Spawear;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InvocarEnemigos", 0.1f, TiempoRepeticiones);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InvocarEnemigos() {
        if (transform.childCount <= Numero_Maximo_Enemigos) {
            GameObject instancia = Instantiate(Enemigo_Spawear, transform.position,Quaternion.identity,transform);
            Vector3 escala = new Vector3(instancia.transform.localScale.x / transform.localScale.x,
                instancia.transform.localScale.y / transform.localScale.y, 
                instancia.transform.localScale.z / transform.localScale.z);
            instancia.transform.localScale = escala;


        }
        
    }
}
