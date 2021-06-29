using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class General_Coding : MonoBehaviour
{
    // Start is called before the first frame update

    public bool ManagerScene;
    
    public  string NameLevel;

    void Start()
    {
        MouseViewLock(true);
    }
    public void ReturnTime()
    {
        Time.timeScale = 1;
        MouseViewLock(true);
    }

    public static void MouseViewLock(bool CameraLock) {
       if (CameraLock){
          Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(Resources.Load("Sprites/CursorPrincipal") as Texture2D, Vector2.zero, CursorMode.Auto);


        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(Resources.Load("Sprites/CursorInteraccion") as Texture2D, Vector2.zero, CursorMode.Auto);
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (ManagerScene && other.tag == "Player") {
            Debug.Log("Cambiar escena");
            ChangeScene(NameLevel);
        }
    }


    public static void ChangeScene(string f) {
        SceneManager.LoadScene(f);
    }
  
    

    // Update is called once per frame
    void Update()
    {

        Cursor.visible = true;
    }
}
