using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Inicio(string Emergencias)
    {
        SceneManager.LoadScene(Emergencias);
    }
   public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliste");
    }
}
