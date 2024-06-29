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
    public GameObject textoPresionFinal;
    public GameObject textoSaturacion;
    public GameObject textoSaturacionFinal;
    public GameObject textoTorax;
    public GameObject textoToraxFinal;
    public GameObject jugador;
    private PlayerController ControladorJugador;
    public GameObject mostrarObjeto;
    public GameObject posicionCompresiones;
    public GameObject compresorInicial;

    public Iniciarcompresiones controladorCompresiones;
    public Controladoracciones controlador;

    public bool presionParo;
    public bool toraxParo;
    public bool saturacionParo;


    public int estado = 0;

    void Start()
    {
        ControladorJugador = jugador.GetComponent<PlayerController>();
        mostrarObjeto.SetActive(false);
    }
    private IEnumerator delayMenu() {

        yield return null;
        ControladorJugador.CantMove = true;
        ControladorJugador.DesactivarCursor();

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
        if (toraxParo == false)
        {
            textoTorax.SetActive(true);
            Invoke("DuracionTexto", 6);
        }
        if (toraxParo == true)
        {
            textoToraxFinal.SetActive(true); ;
            Invoke("DuracionTexto", 6);
        }
        
    }
    public void TextoSaturacion()
    {
        if (saturacionParo == false)
        {
            textoSaturacion.SetActive(true);
            Invoke("DuracionTexto", 4);
        }
        if (saturacionParo == true)
        {
            textoSaturacionFinal.SetActive(true);
            Invoke("DuracionTexto", 4);
        }
    }
    public void TextoPresion()
    {
        if (presionParo == false)
        {
            textoPresion.SetActive(true);
            Invoke("DuracionTexto", 4);
        }
        if(presionParo == true)
        {
            textoPresionFinal.SetActive(true);
            Invoke("DuracionTexto", 4);
        }
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
        ControladorJugador.CantMove = true;
        
        textoInicial.SetActive(true);
    }
    public void definirEstado(int estado)
    {
        
        this.estado = estado;
    }
    public void DuracionTexto()
    {
        textoPulso.SetActive(false);
        textoPulsoFinal.SetActive(false);
        textoRespuesta.SetActive(false);
        textoPresion.SetActive(false);
        textoTorax.SetActive(false);
        textoSaturacion.SetActive(false);
        textoPresionFinal.SetActive(false);
        textoToraxFinal.SetActive(false);
        textoSaturacionFinal.SetActive(false);
    }
   
}