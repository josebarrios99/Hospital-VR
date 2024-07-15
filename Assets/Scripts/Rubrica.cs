using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rubrica : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCondiciones;
    
    //medicamentos
    public TMP_Text inputDosis;
    // public TMP_InputField inputFrecuencia;
    // public TMP_InputField inputProfundidad;

    private int dosisMinima = 70;
    private int dosisMaxima = 100;
    //compresiones
    private int frecuenciaMin = 100;
    private int frecuenciaMax = 120;
    private int profundidadMin = 5;
    private int profundidadMax = 6;
    //secuencias
    public Button[] botonesPaciente;
    private List<int> secuenciaPaciente = new List<int>() { 0, 1 };
    private int indiceSiguientePaciente = 0;

    public Button[] botonesDescarga;
    private List<int> secuenciaDescarga = new List<int>() { 0, 2 };
    private int indiceSiguienteDescarga = 0;

    public Button[] botonesPulso;
    private List<int> secuenciaPulso = new List<int>() { 0, 1, 2 };
    private int indiceSiguientePulso = 0;

    public bool confirmarDosis = false;
    private string DosisSeleccionada;
    
    [SerializeField] private ControladorRubrica _controladorRubrica;
    [SerializeField] Controladoracciones controlador;
    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
        controlador = GameObject.Find("Controlador").GetComponent<Controladoracciones>();
        
        for (int i = 0; i < botonesPaciente.Length; i++)
        {
            int indiceBoton = i; // Captura el valor actual de 'i' para el delegado
            botonesPaciente[i].onClick.AddListener(() => ValidarOrdenBoton(indiceBoton));
        }
        // for (int i = 0; i < botonesPulso.Length; i++)
        // {
        //     int indiceBoton = i; // Captura el valor actual de 'i' para el delegado
        //     botonesPulso[i].onClick.AddListener(delegate { ValidarOrdenBotonPulso(indiceBoton); });
        // }
    }
    public void ValidarInput()
    {
        DosisSeleccionada = inputDosis.text;
        int numeroIngresado;
        Medicamento? UltimoMedicamentoSeleccionado = _controladorRubrica.GetUltimoMedicamentoSeleccionado();
        controlador.nuevoCiclo($"Se aplica {UltimoMedicamentoSeleccionado} - Dosis: {DosisSeleccionada}");
        switch (UltimoMedicamentoSeleccionado)
        {
            case Medicamento.Adrenalina:
                if (DosisSeleccionada == "1" && confirmarDosis == true)
                    _controladorRubrica.ActualizarRubrica(18);
                else
                    _controladorRubrica.ActualizarRubrica(18, false);
                break;
            case Medicamento.Amiodarona:
                // if (DosisSeleccionada == "300" && confirmarDosis == true)
                //     _controladorRubrica.ActualizarRubrica(19);
                // else
                //     _controladorRubrica.ActualizarRubrica(19, false);
                break;
            case Medicamento.Lidocaina:
                // if (int.TryParse(DosisSeleccionada, out numeroIngresado) && confirmarDosis == true)
                // {
                //     if (numeroIngresado >= dosisMinima && numeroIngresado <= dosisMaxima)
                //         _controladorRubrica.ActualizarRubrica(20);
                //     else
                //         _controladorRubrica.ActualizarRubrica(20, false);
                // }
                break;
        }
    }
    // public void ValidarInputFrecuencia()
    // {
    //     int numeroIngresado;
    //
    //     if (int.TryParse(inputFrecuencia.text, out numeroIngresado))
    //     {
    //         if (numeroIngresado >= frecuenciaMin && numeroIngresado <= frecuenciaMax)
    //             _controladorRubrica.ActualizarRubrica(3);
    //         else
    //             _controladorRubrica.ActualizarRubrica(3,false);
    //     }
    // }
    // public void ValidarInputProfundidad()
    // {
    //     int numeroIngresado;
    //
    //     if (int.TryParse(inputProfundidad.text, out numeroIngresado))
    //     {
    //         if (numeroIngresado >= profundidadMin && numeroIngresado <= profundidadMax)
    //             _controladorRubrica.ActualizarRubrica(4);
    //         else
    //             _controladorRubrica.ActualizarRubrica(4,false);
    //     }
    // }
    public void SeleccionarAdrenalina()
    {
        _controladorRubrica.OnMedicamentoSeleccionado();
    }
    
    public void SeleccionarAmiodarona()
    {
        _controladorRubrica.OnMedicamentoSeleccionado(Medicamento.Amiodarona);
    }
    public void SeleccionarAtropina()
    {
        _controladorRubrica.OnMedicamentoSeleccionado(Medicamento.Atropina);
    }
    public void SeleccionarNoradrenalina()
    {
        _controladorRubrica.OnMedicamentoSeleccionado(Medicamento.Noradrenalina);
    }
    public void SeleccionarLidocaina()
    {
        _controladorRubrica.OnMedicamentoSeleccionado(Medicamento.Lidocaina);
    }
    public void SeleccionarSulfatoDeMagnesio()
    {
        _controladorRubrica.OnMedicamentoSeleccionado(Medicamento.SulfatoDeMagnesio);
    }
    public void ConfirmarDosis()
    {
        confirmarDosis = true;
        ValidarInput(); 
    }
    public void validarVentilaciones()
    {
        _controladorRubrica.ActualizarRubrica(15);
    }
    void ValidarOrdenBoton(int indiceBoton)
    {
        if (!controlador.tienePulso())
        {
            if (indiceBoton == 0)
            {
                indiceSiguientePaciente++;
                _controladorRubrica.ActualizarRubrica(0);
                controlador.nuevoCiclo("Llamar paciente");
            }
            else
            {
                _controladorRubrica.ActualizarRubrica(1);
                controlador.nuevoCiclo("Se tom? el pulso");
            }
        }
        else
        {
            if (indiceBoton != 0)
                if(_controladorRubrica.GetDejarDeManipular())
                    _controladorRubrica.ActualizarRubrica(13);
        }
    }
}
