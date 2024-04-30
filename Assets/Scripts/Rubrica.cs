using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rubrica : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCondiciones;
    //acciones
    public bool c1, c2, c3, c4, c5, c6, c7 = true, c8, c9, c10, c11, c12, c13 = true, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28;
    //medicamentos
    public TMP_InputField inputDosis;
    public TMP_InputField inputFrecuencia;
    public TMP_InputField inputProfundidad;
    public bool adrenalina = false;
    public bool amiodarona = false;
    public bool lidocaina = false;
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
    private List<int> secuenciaDescarga = new List<int>() { 0, 1, 2 };
    private int indiceSiguienteDescarga = 0;

    public Button[] botonesPulso;
    private List<int> secuenciaPulso = new List<int>() { 0, 1, 2 };
    private int indiceSiguientePulso = 0;

    public bool confirmarDosis = false;
    void Start()
    {
        inputDosis.onValueChanged.AddListener(delegate { ValidarInput(); });
        inputFrecuencia.onValueChanged.AddListener(delegate { ValidarInputFrecuencia(); });
        inputProfundidad.onValueChanged.AddListener(delegate { ValidarInputProfundidad(); });
        for (int i = 0; i < botonesPaciente.Length; i++)
        {
            int indiceBoton = i; // Captura el valor actual de 'i' para el delegado
            botonesPaciente[i].onClick.AddListener(delegate { ValidarOrdenBoton(indiceBoton); });
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

    
    void Update()
    {
        ActualizarTextoCondiciones();
        
    }
    void ActualizarTextoCondiciones()
    {
        string texto = "Condiciones cumplidas:\n";

        if (c1 == true)
        {
            texto += "- Condición 1\n";
        }
        if (c2 == true)
        {
            texto += "- Condición 2\n";
        }
        if (c3 == true)
        {
            texto += "- Condición 3\n";
        }
        if (c4 == true)
        {
            texto += "- Condición 4\n";
        }
        if (c5 == true)
        {
            texto += "- Condición 5\n";
        }
        if (c7 == true)
        {
            texto += "- Condición 7\n";
        }
        if (c8 == true)
        {
            texto += "- Condición 8\n";
        }
        if (c10 == true)
        {
            texto += "- Condición 10\n";
        }
        if (c11 == true)
        {
            texto += "- Condición 11\n";
        }
        if (c13 == true)
        {
            texto += "- Condición 13\n";
        }
        if (c14 == true)
        {
            texto += "- Condición 14\n";
        }
        if (c15 == true)
        {
            texto += "- Condición 15\n";
        }
        if (c16 == true)
        {
            texto += "- Condición 16\n";
        }
        if (c17 == true)
        {
            texto += "- Condición 17\n";
        }
        if (c18 == true)
        {
            texto += "- Condición 18\n";
        }
        if (c19 == true)
        {
            texto += "- Condición 19\n";
        }
        if (c20 == true)
        {
            texto += "- Condición 20\n";
        }
        if (c21 == true)
        {
            texto += "- Condición 21\n";
        }
        if (c22 == true)
        {
            texto += "- Condición 22\n";
        }
        if (c23 == true)
        {
            texto += "- Condición 23\n";
        }
        if (c24 == true)
        {
            texto += "- Condición 24\n";
        }
        if (c25 == true)
        {
            texto += "- Condición 25\n";
        }
        if (c26 == true)
        {
            texto += "- Condición 26\n";
        }
        if (c27 == true)
        {
            texto += "- Condición 27\n";
        }
        if (c28 == true)
        {
            texto += "- Condición 28\n";
        }

        // Mostrar el texto actualizado en el objeto de texto
        textoCondiciones.text = texto;
    }
    public void ValidarInput()
    {
        int numeroIngresado;
        

        
        if (int.TryParse(inputDosis.text, out numeroIngresado) && lidocaina == true && confirmarDosis == true)
        {
            if (numeroIngresado >= dosisMinima && numeroIngresado <= dosisMaxima)
            {
                c21 = true;
                
            }
            else
            {
                c21 = false;
            }
        }
        if (inputDosis.text == "1" && adrenalina == true && confirmarDosis == true)
        {
            c19 = true;
            
        }
        else
        {
            c19 = false;
        }
        if (inputDosis.text == "300" && amiodarona == true && confirmarDosis == true)
        {
            c20 = true;
            
        }
        else { c20 = false; }
    }
    public void ValidarInputFrecuencia()
    {
        Debug.Log("conejo");
        int numeroIngresado;

        if (int.TryParse(inputFrecuencia.text, out numeroIngresado))
        {
            if (numeroIngresado >= frecuenciaMin && numeroIngresado <= frecuenciaMax)
            {
                c4 = true;
                Debug.Log("conejo");
            }
            else
            {
                c4 = false;
            }
        }
    }
    public void ValidarInputProfundidad()
    {
        int numeroIngresado;

        if (int.TryParse(inputProfundidad.text, out numeroIngresado))
        {
            if (numeroIngresado >= profundidadMin && numeroIngresado <= profundidadMax)
            {
                c5 = true;
                
            }
            else
            {
                c5 = false;
            }
        }
    }
    public void SeleccionarAdrenalina()
    {
        if (adrenalina == false && amiodarona == false && lidocaina == false)
        {
            c18 = true;
        }
        adrenalina = true;
        Debug.Log("adrenalina");
    }
    public void SeleccionarAmiodarona()
    {
        amiodarona = true;
        Debug.Log("adrenalina");
    }
    public void SeleccionarLidocaina()
    {
        lidocaina = true;
        Debug.Log("adrenalina");
    }
    public void ConfirmarDosis()
    {
        confirmarDosis = true;
        Debug.Log("conhfirmo dosis");
        
        ValidarInput(); 
    }
    public void validarVentilaciones()
    {
        c16 = true;
    }
    void ValidarOrdenBoton(int indiceBoton)
    {
        if (indiceBoton == secuenciaPaciente[indiceSiguientePaciente])
        {
            indiceSiguientePaciente++;

            if (indiceSiguientePaciente >= secuenciaPaciente.Count)
            {
                c1 = true;
                c2 = true;
            }
        }
        else
        {
            c1 = false;
            c2 = false;
            indiceSiguientePaciente = 0;
        }
    }
    void ValidarOrdenBotonDescarga(int indiceBoton)
    {
        if (indiceBoton == secuenciaDescarga[indiceSiguienteDescarga])
        {
            indiceSiguienteDescarga++;

            if (indiceSiguienteDescarga >= secuenciaDescarga.Count)
            {
                c12 = true;
            }
        }

        else
        {
            c12 = false;
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
                c14 = true;
                // Todas las acciones se han realizado en el orden correcto
                Debug.Log("¡Todas las acciones realizadas en el orden correcto!");
                // Aquí puedes activar otro objeto o hacer cualquier otra acción
            }
        }

        else
        {
            c14 = false;
            // La acción está fuera de orden
            Debug.Log("¡Error! Acción fuera de orden.");
            // Puedes reiniciar el contador aquí si quieres reiniciar el orden después de un error
            indiceSiguientePulso = 0;
        }
    }
}
