using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAnimacion : MonoBehaviour
{
    public Animator animador;
    private bool animacionEjecutandose = false;
    private float tiempoTotal = 10 * 60; // 10 minutos en segundos
    private float porcentajeObjetivo = 0.6f; // 60%
    private float tiempoTranscurrido = 0f;

    void Start()
    {
        // Obtener la referencia al Animator
        animador = GetComponent<Animator>();
    }
    void Update()
    {
        if (animador.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && animador.GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
        {
            // La animaci�n est� en curso
            if (!animacionEjecutandose)
            {
                // La animaci�n acaba de comenzar
                animacionEjecutandose = true;
            }
        }
        else
        {
            // La animaci�n no se est� reproduciendo
            if (animacionEjecutandose)
            {
                // La animaci�n acaba de detenerse
                animacionEjecutandose = false;
                tiempoTranscurrido += animador.GetCurrentAnimatorStateInfo(0).length;
            }
        }

        if (tiempoTranscurrido >= tiempoTotal * porcentajeObjetivo)
        {
            Debug.Log("El 60% de la animaci�n se ha reproducido durante " + (tiempoTotal / 60) + " minutos.");
            // Aqu� puedes realizar alguna acci�n cuando se alcanza el 60% de la animaci�n.
        }
    }

    public void ReproducirAnimacion(string nombreAnimacion)
    {
        // Iniciar la reproducci�n de la animaci�n por su nombre
        animador.SetTrigger(nombreAnimacion);
    }
}
