using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    public LayerMask layerMask; // Capa de objetos que el rayo puede detectar
    public LineRenderer lineRenderer; // Referencia al LineRenderer
    public Color defaultColor; // Color por defecto del rayo
    public Color objectColor; // Color del rayo al chocar con un objeto de la capa específica
    public string targetLayer = "Raycast Detect"; // Nombre de la capa del objeto que cambia el color del rayo
    public GameObject menu;

    void Start()
    {
        // Configura el LineRenderer si no está configurado
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("No se ha asignado el LineRenderer en el Inspector.");
                return;
            }
        }

        // Desactiva el LineRenderer al inicio
        lineRenderer.enabled = false;

        // Establece el color por defecto del LineRenderer
        lineRenderer.material.color = defaultColor;
    }

    void Update()
    {
         HandleTriggerPress();
    }

    void HandleTriggerPress()
    {
        // Activa el LineRenderer
        lineRenderer.enabled = true;

        // Actualiza el LineRenderer para que siga la dirección del rayo
        lineRenderer.SetPosition(0, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            // Se ha detectado un objeto
            lineRenderer.SetPosition(1, hit.point);
            GameObject objetoImpactado = hit.collider.gameObject;

            // Comprueba si el objeto impactado está en la capa específica
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(targetLayer))
            {
                // Cambia el color del LineRenderer al color del objeto
                lineRenderer.material.color = objectColor;
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    
                }
            }
            else
            {
                // Restaura el color por defecto del LineRenderer
                lineRenderer.material.color = defaultColor;
            }

            
        }
        else
        {
            // No se ha detectado ningún objeto, extiende el LineRenderer al infinito
            lineRenderer.SetPosition(1, transform.position + transform.forward * 100f);

            // Restaura el color por defecto del LineRenderer
            lineRenderer.material.color = defaultColor;
        }
    }
}

