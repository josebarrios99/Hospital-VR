using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Iniciarcompresiones : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Controladoracciones controlador;

    [SerializeField] private ControladorRubrica _controladorRubrica;

    [SerializeField] private Crono ControladorTiempo;
    // Start is called before the first frame update
    private void Awake()
    {
        ControladorTiempo = FindObjectOfType<Crono>();
    }

    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
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
        // _controladorRubrica.ActualizarRubrica(0);
        // controlador.nuevoCiclo(1, "Llamar paciente");
    }
    public void iniciarCompresiones()
    {
        if (ControladorTiempo.getTiempoSec() <= 30f)
        {
            Debug.Log($"Compresiones  Correctas, Current Time: {ControladorTiempo.getTiempoMinutos()}");
            _controladorRubrica.ActualizarRubrica(2);
        }
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(6, false);
        }
        anim.SetBool("Iniciar compresiones", true);
        // controlador.nuevoCiclo(4, "Iniciar compresiones");
        
    }
    public void iniciarCompresiones2()
    {
        if (ControladorTiempo.getTiempoSec() <= 30f)
        {
            Debug.Log($"Compresiones  Correctas, Current Time: {ControladorTiempo.getTiempoMinutos()}");
            _controladorRubrica.ActualizarRubrica(2);
        }
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(6, false);
        }
        anim.SetBool("Iniciar compresiones 2", true);
        // controlador.nuevoCiclo(4, "Iniciar compresiones");
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
        if (ControladorTiempo.getTiempoMinutos() <= 0.75)
        {
            _controladorRubrica.ActualizarRubrica(14);
        }
        anim.SetBool("Poner mascarilla", true);
        // controlador.nuevoCiclo(3, "Iniciar ventilaciones");
    }
    public void detenerVentilaciones()
    {
        anim.SetBool("Poner mascarilla", false);
    }

    public void ponerMedicamento()
    {
        _controladorRubrica.ActualizarRubrica(21);
        anim.SetBool("Poner acceso venoso", true);
        

        // if (FindObjectOfType<Rubrica>().adrenalina == true)
        // {
        //     controlador.nuevoCiclo(5, "Se administr� adrenalina");
        // }
        // if (FindObjectOfType<Rubrica>().atropina == true)
        // {
        //     controlador.nuevoCiclo(5, "Se administr� atropina");
        // }
        // if (FindObjectOfType<Rubrica>().noradrenalina == true)
        // {
        //     controlador.nuevoCiclo(5, "Se administr� noradrenalina");
        // }
        // if (FindObjectOfType<Rubrica>().amiodarona == true)
        // {
        //     controlador.nuevoCiclo(5, "Se administr� amiodarona");
        // }
        // if (FindObjectOfType<Rubrica>().lidocaina == true)
        // {
        //     controlador.nuevoCiclo(5, "Se administr� lidocaina");
        // }
        // if (FindObjectOfType<Rubrica>().sulfatoDeMagnesio == true)
        // {
        //     controlador.nuevoCiclo(5, "Se administr� sulfatoDeMagnesio");
        // }

        if (ControladorTiempo.getTiempoMinutos() <= 2.5)
        {
            _controladorRubrica.ActualizarRubrica(16);
        }
        if (ControladorTiempo.getTiempoMinutos() <= 6)
        {
            if (FindObjectOfType<Rubrica>().amiodarona == true)
            {
                _controladorRubrica.ActualizarRubrica(22);
            }
            if (FindObjectOfType<Rubrica>().lidocaina == true)
            {
                _controladorRubrica.ActualizarRubrica(23);
            }
        }
        if (FindObjectOfType<Rubrica>().lidocaina == true || FindObjectOfType<Rubrica>().amiodarona == true || FindObjectOfType<Rubrica>().adrenalina == true)
            _controladorRubrica.ActualizarRubrica(24);
        else 
            _controladorRubrica.ActualizarRubrica(24,false);
    }

    public void intramuscular()
    {
        _controladorRubrica.ActualizarRubrica(21,false);
        // controlador.nuevoCiclo(5, "Poner medicamento");
    }
    public void ActivarDesfribilador()
    {

    }
    public void desfibrilador()
    {
        if (ControladorTiempo.getTiempoSec() <= 30f)
        {
            _controladorRubrica.ActualizarRubrica(7);
        }
        anim.SetBool("Desfibrilador", true);
    }
    public void desfibrilador2()
    {
        if (ControladorTiempo.getTiempoSec() <= 30f)
        {
            _controladorRubrica.ActualizarRubrica(7);
        }
        anim.SetBool("Desfibrilador 2", true);
    }
    public void descarga()
    {
        // controlador.nuevoCiclo(4, "Se hizo una descarga");
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(12,false);
        }
        if (FindObjectOfType<Controladoracciones>().pulso == false)
        {
            _controladorRubrica.ActualizarRubrica(10);
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
            _controladorRubrica.ActualizarRubrica(25);
        } else
        {
            _controladorRubrica.ActualizarRubrica(25,false);
        }
        anim.SetBool("Tomar presi�n", true);
    }
    public void ponerSaturador()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(26);
        }
        else
        {
            _controladorRubrica.ActualizarRubrica(26,false);
        }
        anim.SetBool("Saturador", true);
    }
    public void auscultarTorax()
    {
        if (FindObjectOfType<Controladoracciones>().pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(27);
        }
        else
        {
            _controladorRubrica.ActualizarRubrica(27,false);
        }
        anim.SetBool("T�rax", true);
    }
}
