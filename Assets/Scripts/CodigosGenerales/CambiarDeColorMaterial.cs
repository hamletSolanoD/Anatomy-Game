using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarDeColorMaterial : MonoBehaviour
{

    public Vector3[] Colores;
    public float DiferenciaDeCambioSegundos = 0.2f;
    public float CantidadDeCambios = 40;
    public Material Material;


    // Start is called before the first frame update
    void Start()
    {
     
        StartCoroutine(CambioColoresCourrutine());
    }
    private Vector3 PasoDeLosColores(float Pasos, Color ColorActual, Vector3 ColorDeseado)
    {
        float RojoActual = ColorActual.r;
        float VerdeActual = ColorActual.g;
        float AzulActual = ColorActual.b;
        
        float Rojo = RojoActual - (ColorDeseado.x/100);
        float Verde = VerdeActual - (ColorDeseado.y/100);
        float Azul = AzulActual - (ColorDeseado.z/100);

        Rojo = -(Rojo / Pasos);
        Verde = -(Verde / Pasos);
        Azul = -(Azul / Pasos);
        return new Vector3(Rojo, Verde, Azul);

    }

 
    IEnumerator CambioColoresCourrutine()
    {
        while(true) {
            Debug.Log("Started Coroutine at timestamp : " + Time.time);
                for (int i = 0; i <= Colores.Length - 1; i++)
                {
                    for (int e = 0; e <= CantidadDeCambios - 1; e++)
                    {
                        Vector3 DiferencialDeColores = PasoDeLosColores(CantidadDeCambios, Material.GetColor("_Color"), Colores[i]);
                        float Rojo = Material.GetColor("_Color").r + DiferencialDeColores.x;
                        float Verde = Material.GetColor("_Color").g + DiferencialDeColores.y;
                        float Azul = Material.GetColor("_Color").b + DiferencialDeColores.z;
                         Material.shader = Shader.Find("Specular");
                    Material.SetColor("_Color", new Color(Rojo, Verde, Azul,1));
                 //  Debug.Log("Color: Rojo: " + Rojo + "; Verde: " + Verde + " Auzul: " + Azul);
                    yield return new WaitForSeconds(DiferenciaDeCambioSegundos);
                    if (Rojo >= Colores[i].x || Verde >= Colores[i].y || Azul >= Colores[i].z) {
//Debug.Log("ColorCompletado");
                        break;
                        

                    }

                }
              //  Debug.Log("Color: " + Colores[i]);

            }
            
        }
    }
}
