using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCrono;
    [SerializeField] private float tiempo;

    private int tiempoMinutos, tiempoSegundos, tiempoDecimas;

    public int getTiempoMinutos()
    {
        return tiempoMinutos;
    }
    
    void Cronometro()
    {
        tiempo += Time.deltaTime;

        tiempoMinutos = Mathf.FloorToInt (tiempo / 60);
        tiempoSegundos = Mathf.FloorToInt (tiempo % 60);
        tiempoDecimas = Mathf.FloorToInt((tiempo % 1) * 100);

        textoCrono.text = string.Format("{0:00}:{1:00}:{2:00}", tiempoMinutos, tiempoSegundos, tiempoDecimas);
    }

    // Update is called once per frame
    void Update()
    {
        Cronometro();
    }
    public string obtenerTiempo()
    {
        return textoCrono.text;
    } 
}
