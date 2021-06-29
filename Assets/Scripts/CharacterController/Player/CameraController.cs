using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1,30)]
    public float Distancia_Camara = 2;
    public float Camara_Sensibilidad = 1;
    public float Smooth_Rotation = 0.2f;
    private Vector3 SmothAuxiliarSmothRotation;
    Vector3 rotation;
    public GameObject ObjSeguir;
    private float RotacionX;
    private float RotacionY;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(ObjSeguir.transform.forward * Distancia_Camara);
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotacionY -= Input.GetAxis("Mouse Y")* Camara_Sensibilidad;
        RotacionY = Mathf.Clamp(RotacionY, -75, 75);
        RotacionX += Input.GetAxis("Mouse X")* Camara_Sensibilidad;

        rotation = Vector3.SmoothDamp(rotation, new Vector3(RotacionY, RotacionX, 0), ref SmothAuxiliarSmothRotation, Smooth_Rotation);
        transform.eulerAngles = rotation;

        transform.position = ObjSeguir.transform.position - transform.forward * Distancia_Camara;


    }
}
