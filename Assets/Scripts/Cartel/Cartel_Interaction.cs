using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Cartel_Interaction : Interactuable
{
    public TextAsset AssetInformation;
    public  Canvas InformationInterface;
    public  GameObject AdvertanceInterface;
    public Scrollbar Vertical;
    public Sprite AssetImage;
    public TextMeshProUGUI Title;
    public Text Information;
    public Image Ilustration;

    public static bool OnCartelInteraction;




    // Start is called before the first frame update
    void Start()
    {
        
        InformationInterface.enabled = false;
        AdvertanceInterface.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") {
            OnCartelInteraction = true;
            if (InformationInterface.enabled == false)
            { 
                AdvertanceInterface.active = true;
                Debug.Log("Advertencia");
        }
    }
    }
     
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            OnCartelInteraction = false;
            AdvertanceInterface.SetActive( false);
            Debug.Log("cerrar advertencia");

        }
    }
   
    private void OnButtonRed() {
        Time.timeScale = 0;
        Vertical.value = 1;
        AdvertanceInterface.active = false;
        InformationInterface.enabled = true;
        Ilustration.sprite = AssetImage;
        Title.GetComponent<TextMeshProUGUI>().text = AssetInformation.name;
        Information.text = AssetInformation.text;
        Debug.Log("active information panel");
        General_Coding.MouseViewLock(false);



    }
    private void MouseCanvasLock() {
         if(!OnCartelInteraction || AdvertanceInterface.active) {
            General_Coding.MouseViewLock(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseCanvasLock();


    }

    public override void InteractuarClickIzquierdo()
    {
        OnButtonRed();
    }


    public override void InteractuarClickDerecho()
    {
        OnButtonRed();
    }
}
