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
    // Start is called before the first frame update
    void Start()
    {
        ActualizarTexto();
    }
    void Update()
    {

    }
    void ActualizarTexto()
    {
        // Actualizar el texto con el número actual
        texto.text = "" + numero.ToString();
    }
    public void ValidarCarga()
    {
        if (numero == 200)
        {
            FindObjectOfType<Rubrica>().condiciones[9] = true;
            
        }
    }
    public void ReiniciarCarga()
    {
        numero = 0;
    }
    public void ValidarFrecuencia()
    {
        if (numero >= 100 && numero <= 120) {
            FindObjectOfType<Rubrica>().condiciones[3] = true;
        }
    }
    public void ValidarProfundidad()
    {
        if (numero >= 5 && numero <= 6)
        {
            FindObjectOfType<Rubrica>().condiciones[4] = true;
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
            // Incrementar el número y actualizar el texto
            if (numero < 360)
            {
                numero += 10;
                ActualizarTexto();
            }

            // Pausa para evitar un aumento muy rápido
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
            // Incrementar el número y actualizar el texto
            if (numero > 0)
            {
                numero -= 10;
                ActualizarTexto();
            }

            // Pausa para evitar un aumento muy rápido
            yield return new WaitForSeconds(0.08f);
        }
    }

}
