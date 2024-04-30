using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionObjeto : MonoBehaviour
{
    [SerializeField] private Transform objeto;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("space"))
            {
                other.gameObject.GetComponent<PlayerController>().CantMove = true;
                Transform nuevaPosicion = other.gameObject.GetComponent<PlayerController>().PosicionObjeto;
                objeto.position = nuevaPosicion.position;
            }
            
        }
        
    }

}
