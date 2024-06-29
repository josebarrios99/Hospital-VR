using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carga : MonoBehaviour
{
    public Text texto;
    private int numero = 0;
    private bool sumar = false;
    private bool restar = false;
    private bool aumentando = false;
    private bool disminuyendo = false;

    private bool On;
    private ControladorRubrica _controladorRubrica;
    
    // Start is called before the first frame update
    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
        ActualizarTexto();
    }

    public void InteractWithObject()
    {
        if (On)
        {
            _controladorRubrica.ActualizarRubrica(8);
        }
        else
        {
            _controladorRubrica.ActualizarRubrica(8,false);
        }
    }
    
    private void OnDisable()
    {
        On = false;
    }

    public void TurnOn()
    {
        On = true;
    }
    void ActualizarTexto()
    {
        // Actualizar el texto con el n�mero actual
        texto.text = numero.ToString();
    }
    public void ValidarCarga()
    {
        Debug.Log("Estamos Validando Carga");
        if (numero == 200)
        {
            _controladorRubrica.ActualizarRubrica(9);
        }
    }
    public void ReiniciarCarga()
    {
        numero = 0;
    }
    public void ValidarFrecuencia()
    {
        if (numero >= 100 && numero <= 120) {
            _controladorRubrica.ActualizarRubrica(3);
        }
    }
    public void ValidarProfundidad()
    {
        if (numero >= 5 && numero <= 6)
        {
            _controladorRubrica.ActualizarRubrica(4);
        }
    }
    public void EmpezarAumentar()
    {
        if (!aumentando)
        {
            aumentando = true;
            StartCoroutine(AumentarNumero());
        }
    }

    public void DetenerAumentar()
    {
        aumentando = false;
    }

    IEnumerator AumentarNumero()
    {
        while (aumentando)
        {
            // Incrementar el n�mero y actualizar el texto
            if (numero < 360)
            {
                numero += 10;
                ActualizarTexto();
            }

            // Pausa para evitar un aumento muy r�pido
            yield return new WaitForSeconds(0.08f);
        }
    }

    public void EmpezarDisminuir()
    {
        if (!disminuyendo)
        {
            disminuyendo = true;
            StartCoroutine(RestarNumero());
        }
    }

    public void DetenerDisminuir()
    {
        disminuyendo = false;
    }

    IEnumerator RestarNumero()
    {
        while (disminuyendo)
        {
            // Incrementar el n�mero y actualizar el texto
            if (numero > 0)
            {
                numero -= 10;
                ActualizarTexto();
            }

            // Pausa para evitar un aumento muy r�pido
            yield return new WaitForSeconds(0.08f);
        }
    }

}
