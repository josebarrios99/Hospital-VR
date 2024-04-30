using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Iniciarcompresiones : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Controladoracciones controlador;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        controlador = GameObject.Find("Controlador").GetComponent<Controladoracciones>();
    }
    

    // Update is called once per frame

    public bool tienePulso()
    {
        return controlador.tienePulso();
    }
    
    public void llamarPaciente()
    {
        controlador.nuevoCiclo(1, "Llamar paciente");
    }
    public void iniciarCompresiones()
    {
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 0.5)
        {
            FindObjectOfType<Rubrica>().c3 = true;
        }
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().c7 = false;
        }
        anim.SetBool("Iniciar compresiones", true);
        controlador.nuevoCiclo(4, "Iniciar compresiones");
        
    }
    public void iniciarCompresiones2()
    {
        
        anim.SetBool("Iniciar compresiones 2", true);
        controlador.nuevoCiclo(4, "Iniciar compresiones");
    }
    public void detenerCompresiones()
    {
        anim.SetBool("Iniciar compresiones", false);

    }
    public void detenerCompresiones2()
    {
        anim.SetBool("Iniciar compresiones 2", false);
    }
    public void aumentarVelocidad()
    {
        anim.speed++;
    }
    public void disminuirVelocidad()
    {
        anim.speed--;
    }

    public void iniciarVentilaciones()
    {
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 0.75)
        {
            FindObjectOfType<Rubrica>().c15 = true;
        }
        anim.SetBool("Poner mascarilla", true);
        controlador.nuevoCiclo(3, "Iniciar ventilaciones");
    }
    public void detenerVentilaciones()
    {
        anim.SetBool("Poner mascarilla", false);
    }

    public void ponerMedicamento()
    {
        FindObjectOfType<Rubrica>().c22 = true;
        anim.SetBool("Poner acceso venoso", true);
        controlador.nuevoCiclo(5, "Poner medicamento");
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 2.5)
        {
            FindObjectOfType<Rubrica>().c17 = true;
        }
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 6)
        {
            if (FindObjectOfType<Rubrica>().amiodarona == true)
            {
                FindObjectOfType<Rubrica>().c23 = true;
            }
            if (FindObjectOfType<Rubrica>().lidocaina == true)
            {
                FindObjectOfType<Rubrica>().c24 = true;
            }
        }
        if (FindObjectOfType<Rubrica>().lidocaina == true || FindObjectOfType<Rubrica>().amiodarona == true || FindObjectOfType<Rubrica>().adrenalina == true)
        {
            FindObjectOfType<Rubrica>().c25 = true;
        } else { FindObjectOfType<Rubrica>().c25 = false; }
    }

    public void intramuscular()
    {
        FindObjectOfType<Rubrica>().c22 = false;
        controlador.nuevoCiclo(5, "Poner medicamento");
    }

    public void desfibrilador()
    {
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 0.5)
        {
            FindObjectOfType<Rubrica>().c8 = true;
        }
        anim.SetBool("Desfibrilador", true);
    }
    public void desfibrilador2()
    {
        anim.SetBool("Desfibrilador 2", true);
    }
    public void descarga()
    {
        controlador.nuevoCiclo(4, "Se hizo una descarga");
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().c13 = false;
        }
        if (FindObjectOfType<Controladoracciones>().pulso == false)
        {
            FindObjectOfType<Rubrica>().c11 = true;
        }
        Debug.Log("se hizo una descarga");
    }
    public void detenerDesfibrilador()
    {
        anim.SetBool("Desfibrilador", false);
    }
    public void detenerDesfibrilador2()
    {
        anim.SetBool("Desfibrilador 2", false);
    }
    public void tomarPresion()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().c26 = true;
        } else
        {
            FindObjectOfType<Rubrica>().c26 = false;
        }
        anim.SetBool("Tomar presión", true);
    }
    public void ponerSaturador()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().c27 = true;
        }
        else
        {
            FindObjectOfType<Rubrica>().c27 = false;
        }
        anim.SetBool("Saturador", true);
    }
    public void auscultarTorax()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().c28 = true;
        }
        else
        {
            FindObjectOfType<Rubrica>().c28 = false;
        }
        anim.SetBool("Tórax", true);
    }
}
