using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCompresiones : MonoBehaviour
{
    public Text texto;
    public bool aumentaEn10;
    private int numero = 0;
    private bool sumar = false;
    private bool restar = false;
    private bool aumentando = false;
    private bool disminuyendo = false;
    
    [SerializeField] private ControladorRubrica _controladorRubrica;
    // Start is called before the first frame update
    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
        ActualizarTexto();
    }
    void Update()
    {

    }
    public void aumentarDosisEn10()
    {
        aumentaEn10 = true;
    }
    public void noAumentaDosisEn10()
    {
        aumentaEn10 = false;
    }
    void ActualizarTexto()
    {
        // Actualizar el texto con el n�mero actual
        texto.text = "" + numero.ToString();
    }
    
    public void Reiniciar()
    {
        numero = 0;
        ActualizarTexto();
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
                if (aumentaEn10==false)
                {
                    numero += 1;
                    ActualizarTexto();
                }
                else
                {
                    numero += 10;
                    ActualizarTexto();
                }
                
            }

            // Pausa para evitar un aumento muy r�pido
            yield return new WaitForSeconds(0.15f);
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
                if (aumentaEn10 == false)
                {
                    numero -= 1;
                    ActualizarTexto();
                }
                else { 
                    numero -= 10;
                    ActualizarTexto();
                }
                
            }


            // Pausa para evitar un aumento muy r�pido
            yield return new WaitForSeconds(0.15f);
        }
    }

}
