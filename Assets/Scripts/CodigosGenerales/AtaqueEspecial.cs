using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtaqueEspecial : MonoBehaviour
{
    public float Daño;
    public string NombreDeAnimacion;
    public float Tiempo_Recuperacion;
    public abstract void Ataque(Character Enfocado);

}
