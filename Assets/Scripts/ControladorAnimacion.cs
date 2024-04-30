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
            // La animación está en curso
            if (!animacionEjecutandose)
            {
                // La animación acaba de comenzar
                animacionEjecutandose = true;
            }
        }
        else
        {
            // La animación no se está reproduciendo
            if (animacionEjecutandose)
            {
                // La animación acaba de detenerse
                animacionEjecutandose = false;
                tiempoTranscurrido += animador.GetCurrentAnimatorStateInfo(0).length;
            }
        }

        if (tiempoTranscurrido >= tiempoTotal * porcentajeObjetivo)
        {
            Debug.Log("El 60% de la animación se ha reproducido durante " + (tiempoTotal / 60) + " minutos.");
            // Aquí puedes realizar alguna acción cuando se alcanza el 60% de la animación.
        }
    }

    public void ReproducirAnimacion(string nombreAnimacion)
    {
        // Iniciar la reproducción de la animación por su nombre
        animador.SetTrigger(nombreAnimacion);
    }
}
