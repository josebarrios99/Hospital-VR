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
    public Text inputDosis;
    public TMP_InputField inputFrecuencia;
    public TMP_InputField inputProfundidad;

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
        for (int i = 0; i < botonesDescarga.Length; i++)
        {
            int indiceBoton = i; // Captura el valor actual de 'i' para el delegado
            botonesDescarga[i].onClick.AddListener(delegate { ValidarOrdenBotonDescarga(indiceBoton); });
        }
        for (int i = 0; i < botonesPulso.Length; i++)
        {
            int indiceBoton = i; // Captura el valor actual de 'i' para el delegado
            botonesPulso[i].onClick.AddListener(delegate { ValidarOrdenBotonPulso(indiceBoton); });
        }
    }
    // public void MostrarResultados()
    // {
    //     int totalCondiciones = condiciones.Count;
    //     int condicionesCumplidas = 0;
    //
    //     string texto = "Condiciones cumplidas:\n";
    //
    //     for (int i = 0; i < totalCondiciones; i++)
    //     {
    //         if (condiciones[i])
    //         {
    //             texto += "- " + nombresCondiciones[i] + "\n";
    //             condicionesCumplidas++;
    //         }
    //     }
    //
    //     float porcentajeCumplidas = ((float)condicionesCumplidas / totalCondiciones) * 100f;
    //     texto += "Porcentaje de condiciones cumplidas: " + porcentajeCumplidas.ToString("F2") + "%";
    //
    //     // Mostrar el texto actualizado en el objeto de texto
    //     textoCondiciones.text = texto;
    // }
    public void ValidarInput()
    {
        DosisSeleccionada = inputDosis.text;
        int numeroIngresado;
        Medicamento? UltimoMedicamentoSeleccionado = _controladorRubrica.GetUltimoMedicamentoSeleccionado();
        Debug.Log($"Ultimo Medicamento Seleccionado: {UltimoMedicamentoSeleccionado}");
        Debug.Log($"Cantidad a Administrar: {DosisSeleccionada}");
        Debug.Log($"Dosis Confirmada?: {confirmarDosis}");
        switch (UltimoMedicamentoSeleccionado)
        {
            case Medicamento.Adrenalina:
                if (DosisSeleccionada == "1" && confirmarDosis == true)
                    _controladorRubrica.ActualizarRubrica(18);
                else
                    _controladorRubrica.ActualizarRubrica(18, false);
                break;
            case Medicamento.Amiodarona:
                if (DosisSeleccionada == "300" && confirmarDosis == true)
                    _controladorRubrica.ActualizarRubrica(19);
                else
                    _controladorRubrica.ActualizarRubrica(19, false);
                break;
            case Medicamento.Lidocaina:
                if (int.TryParse(DosisSeleccionada, out numeroIngresado) && confirmarDosis == true)
                {
                    if (numeroIngresado >= dosisMinima && numeroIngresado <= dosisMaxima)
                        _controladorRubrica.ActualizarRubrica(20);
                    else
                        _controladorRubrica.ActualizarRubrica(20, false);
                }
                break;
            case Medicamento.Atropina:
            case Medicamento.Noradrenalina:
            case Medicamento.SulfatoDeMagnesio:
            default:
                break;
        }
    }
    public void ValidarInputFrecuencia()
    {
        int numeroIngresado;

        if (int.TryParse(inputFrecuencia.text, out numeroIngresado))
        {
            if (numeroIngresado >= frecuenciaMin && numeroIngresado <= frecuenciaMax)
                _controladorRubrica.ActualizarRubrica(3);
            else
                _controladorRubrica.ActualizarRubrica(3,false);
        }
    }
    public void ValidarInputProfundidad()
    {
        int numeroIngresado;

        if (int.TryParse(inputProfundidad.text, out numeroIngresado))
        {
            if (numeroIngresado >= profundidadMin && numeroIngresado <= profundidadMax)
                _controladorRubrica.ActualizarRubrica(4);
            else
                _controladorRubrica.ActualizarRubrica(4,false);
        }
    }
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
        if (indiceBoton == secuenciaPaciente[indiceSiguientePaciente])
        {
            indiceSiguientePaciente++;
            _controladorRubrica.ActualizarRubrica(0);
            controlador.nuevoCiclo("Llamar paciente");

            if (indiceSiguientePaciente >= secuenciaPaciente.Count)
            {
                _controladorRubrica.ActualizarRubrica(1);
                controlador.nuevoCiclo("Se tom? el pulso");
            }
        }
        
    }
    void ValidarOrdenBotonDescarga(int indiceBoton)
    {
        Debug.Log("Descarga true");
        Debug.Log($"Indice Btn {indiceBoton}");
        Debug.Log($"Indice Sig Descarga {indiceSiguienteDescarga}");
        
        if (indiceBoton == secuenciaDescarga[indiceSiguienteDescarga])
        {
            indiceSiguienteDescarga++;
        
            if (indiceSiguienteDescarga >= secuenciaDescarga.Count)
                _controladorRubrica.ActualizarRubrica(11);
        }
        else
        {
            _controladorRubrica.ActualizarRubrica(11,false);
            indiceSiguienteDescarga = 0;
        }
    }
    void ValidarOrdenBotonPulso(int indiceBoton)
    {
        if (indiceBoton == secuenciaPulso[indiceSiguientePulso] && FindObjectOfType<Controladoracciones>().pulso == true)
        {
            // La acción es en el orden correcto
            Debug.Log("Acción " + indiceBoton + " realizada correctamente.");
            indiceSiguientePulso++;

            if (indiceSiguientePulso >= secuenciaPulso.Count)
            {
                _controladorRubrica.ActualizarRubrica(13);
                // Todas las acciones se han realizado en el orden correcto
                Debug.Log("¡Todas las acciones realizadas en el orden correcto!");
                // Aquí puedes activar otro objeto o hacer cualquier otra acción
            }
        }

        else
        {
            _controladorRubrica.ActualizarRubrica(13,false);
            // La acción está fuera de orden
            Debug.Log("¡Error! Acción fuera de orden.");
            // Puedes reiniciar el contador aquí si quieres reiniciar el orden después de un error
            indiceSiguientePulso = 0;
        }
    }
}
