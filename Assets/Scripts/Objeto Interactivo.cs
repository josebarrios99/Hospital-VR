using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjetoInteractivo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoLista;
    public GameObject textoInicial;
    public GameObject textoSegundo;
    public GameObject textoTercero;
    public GameObject textoPulso;
    public GameObject textoPulsoFinal;
    public GameObject textoRespuesta;
    public GameObject textoPresion;
    public GameObject textoSaturacion;
    public GameObject textoTorax;
    public GameObject jugador;
    public GameObject mostrarObjeto;
    public GameObject posicionCompresiones;
    public GameObject compresorInicial;

    public Iniciarcompresiones controladorCompresiones;
    public Controladoracciones controlador;




    public int estado = 0;

    void Start()
    {
        mostrarObjeto.SetActive(false);
    }
    private IEnumerator delayMenu() {

        yield return null;
        jugador.GetComponent<PlayerController>().CantMove = true;
        jugador.GetComponent<PlayerController>().DesactivarCursor();

        MostrarMenu();

    }
    public void ActivarObjeto()
    {
        StartCoroutine(delayMenu());
        
    }
    

    public void MostrarObjeto()
    {
        mostrarObjeto.SetActive(true);
    }
    public void OcultarObjeto()
    {
        mostrarObjeto.SetActive(false);
    }

    public void TextoPulso()
    {
        controlador.nuevoCiclo(2, "Se tomó el pulso");
        if (controladorCompresiones.tienePulso())
        {
            textoPulsoFinal.SetActive(true);
            Invoke("DuracionTexto", 2);
            Debug.Log("tiene pulso");

        }
        else
        {
            textoPulso.SetActive(true);
            Invoke("DuracionTexto", 2);
        }
        
    }
    public void TextoTorax()
    {
        textoTorax.SetActive(true);
        Invoke("DuracionTexto", 6);
    }
    public void TextoSaturacion()
    {
        textoSaturacion.SetActive(true);
        Invoke("DuracionTexto", 4);
    }
    public void TextoPresion()
    {
        textoPresion.SetActive(true);
        Invoke("DuracionTexto", 4);
    }
    public void TextoRespuesta()
    {
        textoRespuesta.SetActive(true);
        Invoke("DuracionTexto", 2);
    }
    public void MostrarMenu()
    {
        if (estado == 0)
        {
            textoInicial.SetActive(true);
            
        }
        else if (estado == 1)
        {
            textoSegundo.SetActive(true);
            
        }
        else
        {
            textoTercero.SetActive(true);
        }

    }
    public void mostrarCompresiones()
    {
        posicionCompresiones.SetActive(true);
    }

    public void ocultarCompresiones()
    {
        posicionCompresiones.SetActive(false);
    }
    public void mostrarCompresorInicial()
    {
        compresorInicial.SetActive(true);
    }
    public void ocultarCompresorInicial()
    {
        compresorInicial.SetActive(false);
    }
    public void MostrarLista()
    {
        jugador.GetComponent<PlayerController>().CantMove = true;

        string textoFinal = "";

        foreach (var texto in controlador.retornarLista()) textoFinal += texto.ToString() + "\n";
        textoLista.text =textoFinal;
        textoInicial.SetActive(true);
    }
    public void definirEstado(int estado)
    {
        
        this.estado = estado;
    }
    public void DuracionTexto()
    {
        textoPulso.SetActive(false);
        textoRespuesta.SetActive(false);
        textoPresion.SetActive(false);
        textoTorax.SetActive(false);
        textoSaturacion.SetActive(false);
    }
   
}