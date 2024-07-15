using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crono : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCrono;
    [SerializeField] private float tiempo;
    [SerializeField] private bool countDown;

    [SerializeField] private float Maxtime = 720f;
    private float animTimeControler = 0f;
    private int tiempoMinutos, tiempoSegundos, tiempoDecimas;
    private bool StartTime = false;
    public float GetMaxTime()
    {
        return Maxtime;
    }
    public int getTiempoMinutos()
    {
        return tiempoMinutos;
    }
    public int getTiempoSec()
    {
        return tiempoSegundos;
    }
    private void Start()
    {
        if(countDown)
            tiempo = Maxtime;
        textoCrono.color = new Color(255, 255, 255, 255);
    }

    public void OnStartTimer()
    {
        StartTime = true;
    }
    void Cronometro()
    {
        if (countDown)
        {
            tiempo -= Time.deltaTime;
            if (tiempo <= 120f)
                AnimColor();
        }
        else
            tiempo += Time.deltaTime;

        tiempoMinutos = Mathf.FloorToInt (tiempo / 60);
        tiempoSegundos = Mathf.FloorToInt (tiempo % 60);
        tiempoDecimas = Mathf.FloorToInt((tiempo % 1) * 100);

        textoCrono.text = string.Format("{0:00}:{1:00}:{2:00}", tiempoMinutos, tiempoSegundos, tiempoDecimas);
    }

    private void AnimColor()
    {
        animTimeControler += Time.deltaTime;
        if (animTimeControler <= 2f)
        {
            StartColorAnimation();
            
        }
        else
        {
            StartColorAnimation(false);
            if (animTimeControler >= 4f)
            {
                animTimeControler = 0f;
            }
        }
    }
    private void StartColorAnimation(bool AlertColor = true)
    {
        if (AlertColor)
            SwitchColor(Color.red);
        else
            SwitchColor(Color.white);
    }
    
    private void SwitchColor(Color newColor)
    {
        textoCrono.color = newColor;
    }
    // Update is called once per frame
    void Update()
    {
        if(StartTime)
            Cronometro();
        
        if (countDown)
        {
            if (tiempo <= 0)
            {
                textoCrono.text = "Fin de la simulación";
                textoCrono.color = new Color(255, 0, 0, 255);
                StartTime = false;
            }
        }
        else
        {
            if (tiempo >= Maxtime)
            {
                textoCrono.text = "Fin de la simulación";
                textoCrono.color = new Color(255, 0, 0, 255);
                StartTime = false;
            }
        }
    }
    public string obtenerTiempo()
    {
        return textoCrono.text;
    } 
}
