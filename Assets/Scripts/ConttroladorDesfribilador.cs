using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConttroladorDesfribilador : MonoBehaviour
{
    public GameObject desfribilador;
    // Start is called before the first frame update
    void Start()
    {
        desfribilador.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarDesfribilador()
    {
        desfribilador.SetActive (true);
    }
}
