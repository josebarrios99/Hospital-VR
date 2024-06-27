using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class Iniciarcompresiones : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Controladoracciones controlador;

    [SerializeField] private ControladorRubrica _controladorRubrica;

    [SerializeField] private Crono ControladorTiempo;
    
    private bool CompresionesIniciadas = false;
    private float TiempoCompresiones = 0.0f;
    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
        ControladorTiempo = GameObject.FindGameObjectWithTag("RealCrono").GetComponent<Crono>();
        controlador = GameObject.Find("Controlador").GetComponent<Controladoracciones>();
        anim = gameObject.GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        if (CompresionesIniciadas && !controlador.pulso)
        {
            TiempoCompresiones += Time.deltaTime;
            float TimeToUpdate = 360f;
            if (TiempoCompresiones >= TimeToUpdate)
            {
                _controladorRubrica.ActualizarRubrica(5);
            }
        }
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
            _controladorRubrica.ActualizarRubrica(2);
        }
        if (controlador.pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(6, false);
        }
        if(_controladorRubrica.Descarga)
            _controladorRubrica.ActualizarRubrica(11);
        CompresionesIniciadas = true;
        anim.SetBool("Iniciar compresiones", true);
        Debug.Log("Se inician Compresiones");
        // controlador.nuevoCiclo(4, "Iniciar compresiones");
        
    }
    public void iniciarCompresiones2()
    {
        if (ControladorTiempo.getTiempoSec() <= 30f)
        {
            _controladorRubrica.ActualizarRubrica(2);
        }
        if (controlador.pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(6, false);
        }
        if(_controladorRubrica.Descarga)
            _controladorRubrica.ActualizarRubrica(11);
        anim.SetBool("Iniciar compresiones 2", true);
        Debug.Log("Se inician Compresiones");
        CompresionesIniciadas = true;
        // controlador.nuevoCiclo(4, "Iniciar compresiones");
    }
    public void detenerCompresiones()
    {
        CompresionesIniciadas = false;
        anim.SetBool("Iniciar compresiones", false);
    }
    public void detenerCompresiones2()
    {
        CompresionesIniciadas = false;
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
    public void ponerAccesoVenoso()
    {
        if (ControladorTiempo.getTiempoSec() <= 45f)
        {
            _controladorRubrica.ActualizarRubrica(16);
        }
    }
    public void ponerMedicamento()
    {
        Medicamento? MedicamentoSeleccionado = _controladorRubrica.GetUltimoMedicamentoSeleccionado();
        
        switch (MedicamentoSeleccionado)
        {
            case Medicamento.Adrenalina:
                var PrimerMedicamento = _controladorRubrica.GetPrimerMedicamento();
                if (PrimerMedicamento == Medicamento.Adrenalina)
                {
                    _controladorRubrica.ActualizarRubrica(17);
            
                }
                break;
            case Medicamento.Amiodarona:
                if (ControladorTiempo.getTiempoMinutos() <= 6)
                    _controladorRubrica.ActualizarRubrica(22);
                break;
            case Medicamento.Lidocaina:
                if (ControladorTiempo.getTiempoMinutos() <= 6)
                    _controladorRubrica.ActualizarRubrica(23);
                break;
            case Medicamento.Atropina:
            case Medicamento.Noradrenalina:
            case Medicamento.SulfatoDeMagnesio:
                if (!controlador.pulso)
                    _controladorRubrica.ActualizarRubrica(24,false);
                else
                    _controladorRubrica.ActualizarRubrica(24);
                break;
            default:
                return;
        }
        
        _controladorRubrica.ActualizarRubrica(21);
        anim.SetBool("Poner acceso venoso", true);
    }

    public void intramuscular()
    {
        var PrimerMedicamento = _controladorRubrica.GetPrimerMedicamento();
        if (PrimerMedicamento == Medicamento.Adrenalina)
        {
            _controladorRubrica.ActualizarRubrica(17);
        }
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
        if (controlador.pulso == true)
        {
            _controladorRubrica.ActualizarRubrica(12,false);
        }
        if (controlador.pulso == false)
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
        if (controlador.pulso)
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
        if (controlador.pulso)
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
        if (controlador.pulso == true)
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
