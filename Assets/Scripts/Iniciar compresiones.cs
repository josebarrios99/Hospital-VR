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
            FindObjectOfType<Rubrica>().condiciones[2] = true;
        }
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().condiciones[6] = false;
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
            FindObjectOfType<Rubrica>().condiciones[14] = true;
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
        FindObjectOfType<Rubrica>().condiciones[21] = true;
        anim.SetBool("Poner acceso venoso", true);
        

        if (FindObjectOfType<Rubrica>().adrenalina == true)
        {
            controlador.nuevoCiclo(5, "Se administró adrenalina");
        }
        if (FindObjectOfType<Rubrica>().atropina == true)
        {
            controlador.nuevoCiclo(5, "Se administró atropina");
        }
        if (FindObjectOfType<Rubrica>().noradrenalina == true)
        {
            controlador.nuevoCiclo(5, "Se administró noradrenalina");
        }
        if (FindObjectOfType<Rubrica>().amiodarona == true)
        {
            controlador.nuevoCiclo(5, "Se administró amiodarona");
        }
        if (FindObjectOfType<Rubrica>().lidocaina == true)
        {
            controlador.nuevoCiclo(5, "Se administró lidocaina");
        }
        if (FindObjectOfType<Rubrica>().sulfatoDeMagnesio == true)
        {
            controlador.nuevoCiclo(5, "Se administró sulfatoDeMagnesio");
        }

        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 2.5)
        {
            FindObjectOfType<Rubrica>().condiciones[16] = true;
        }
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 6)
        {
            if (FindObjectOfType<Rubrica>().amiodarona == true)
            {
                FindObjectOfType<Rubrica>().condiciones[22] = true;
            }
            if (FindObjectOfType<Rubrica>().lidocaina == true)
            {
                FindObjectOfType<Rubrica>().condiciones[23] = true;
            }
        }
        if (FindObjectOfType<Rubrica>().lidocaina == true || FindObjectOfType<Rubrica>().amiodarona == true || FindObjectOfType<Rubrica>().adrenalina == true)
        {
            FindObjectOfType<Rubrica>().condiciones[24] = true;
        } else { FindObjectOfType<Rubrica>().condiciones[24] = false; }
    }

    public void intramuscular()
    {
        FindObjectOfType<Rubrica>().condiciones[21] = false;
        controlador.nuevoCiclo(5, "Poner medicamento");
    }
    public void ActivarDesfribilador()
    {

    }
    public void desfibrilador()
    {
        if (FindObjectOfType<Crono>().getTiempoMinutos() <= 0.5)
        {
            FindObjectOfType<Rubrica>().condiciones[7] = true;
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
            FindObjectOfType<Rubrica>().condiciones[12] = false;
        }
        if (FindObjectOfType<Controladoracciones>().pulso == false)
        {
            FindObjectOfType<Rubrica>().condiciones[10] = true;
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
            FindObjectOfType<Rubrica>().condiciones[25] = true;
        } else
        {
            FindObjectOfType<Rubrica>().condiciones[25] = false;
        }
        anim.SetBool("Tomar presión", true);
    }
    public void ponerSaturador()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().condiciones[26] = true;
        }
        else
        {
            FindObjectOfType<Rubrica>().condiciones[26] = false;
        }
        anim.SetBool("Saturador", true);
    }
    public void auscultarTorax()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            FindObjectOfType<Rubrica>().condiciones[27] = true;
        }
        else
        {
            FindObjectOfType<Rubrica>().condiciones[27] = false;
        }
        anim.SetBool("Tórax", true);
    }
}
