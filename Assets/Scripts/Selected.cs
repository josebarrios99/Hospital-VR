using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{   
    LayerMask mask;
    public float distancia = 1.5f;

    public Texture2D puntero;
    public Texture2D mano;
    public GameObject TextDetect;
    
    GameObject ultimoReconocido = null;
    public GameObject jugador;

    public bool activarRaycast = true;


    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        TextDetect.SetActive(false);
    }

    
    void Update()
    {

           if (activarRaycast == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
            {
                Deselect();
                SelectedObject(hit.transform);
                
                if (hit.collider.tag == "Objeto interactivo")
                {
                    
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        hit.collider.transform.GetComponent<ObjetoInteractivo>().ActivarObjeto();
                        
                       activarRaycast=false;
                    }
                }
                if (hit.collider.tag == "Ventilaciones")
                {
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        hit.collider.transform.GetComponent<Iniciarcompresiones>().iniciarVentilaciones();
                       activarRaycast = false;
                    }
                }
                if (hit.collider.tag == "Ayudante")
                {

                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {

                        hit.collider.transform.GetComponent<ObjetoInteractivo>().ActivarObjeto();
                        activarRaycast = false;
                    }

                }
                if (hit.collider.tag == "Lista")
                {

                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        hit.collider.transform.GetComponent<ObjetoInteractivo>().MostrarLista();
                        activarRaycast = false;
                    }

                }
                if (hit.collider.tag == "Paciente")
                {
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        hit.collider.transform.GetComponent<ObjetoInteractivo>().ActivarObjeto();
                        activarRaycast = false;
                    }
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
            }
            else
            {
                Deselect();
                
            }
        }
            
        
            
        
        
    }

    void SelectedObject (Transform transform)
    {

      // transform.GetComponent<MeshRenderer>().material.color = Color.green;
        ultimoReconocido = transform.gameObject;
    }

    public void ActivarRaycast()
    {
        activarRaycast = true;  
    }

    void Deselect()
    {
        if(ultimoReconocido)
        {
            ultimoReconocido.GetComponent<Renderer>().material.color = Color.white;
            ultimoReconocido = null;
        }
    }

    void OnGUI()
    {
       /* if (activarRaycast)
        {
            Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
            Rect rect2 = new Rect(Screen.width / 2, Screen.height / 2, mano.width, mano.height);

            if (ultimoReconocido)
            {
                TextDetect.SetActive(true);
                GUI.DrawTexture(rect2, mano);
            }
            else
            {
                TextDetect.SetActive(false);
                GUI.DrawTexture(rect, puntero);
            }
        }
*/        
    }

}
