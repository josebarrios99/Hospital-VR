using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum AccionesAValidar { 



}

public class Rubrica : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCondiciones;
    //acciones
    public Text porcentajeCondiciones;
    public  List<bool> condiciones;
    public List<string> nombresCondiciones;


    public bool SeLlamoAlPacientePrimero,
        SeTomoElPulsoDeSegundo,
        CompresionesAntesDe30Segundos,
        CompresionesConFrecuencia100_120cpm,
        CompresionesConProfundidad5_6cm,
        CompresionesPresentesEl60DelTiempo,
        CompresionesNoFueronIniciadasSiElPacienteTienePulso = true,
        ManipularDesfribiladorAntesDe30Segs,
        SeEnciendeDesfribiladorComoPrimeraOpcionDespuesDeManipularlo,
        SeElijeUnaCargaDe200JDespuesDeEncenderse,
        SeAdministraDescargaSiElRitmoEsFV,
        DespuesDeLaDescargaSeDejaDeManipularYSeInicianCompresiones,
        NoSeAdministraDescargaSiElRitmoNoEsFV = true,
        SiElRitmoNoEsFVLaPrimeraAccionEsDejarDeManipularYLuegoTomarPulso,
        VentilacionesIniciadasAntesDe45Segs,
        SeElijeUnaRelacion30_2,
        SePusoUnAccesoVenosoAntesDe45Segs,
        SeAdministraAdrenalinaComoPrimeraOpcion,
        SiAdministraAdrenalinaLaDosisEs1mg,
        SiAdministraAmiodaronaLaDosisEs300mg,
        SiAdministraLidocainaLaDosisEs70_100mg,
        LaViaElegidaSiempreEsIntraVenosa,
        SiAdministraAmiodaronaNoPuedeSerAntesDe6Mins,
        SiAdministraLidocainaNoPuedeSerAntesDe6Mins,
        NoSeAdministraNingunOtroMedicamentoDiferenteSiElPacienteEstaEnParo,
        SeTomaLaPresionArterialSoloSiHayPulso,
        SePoneSaturadorSoloSiHayPulso,
        SeTomaElectrocardiogramaSoloSiHayPulso;
    //medicamentos
    public TMP_InputField inputDosis;
    public TMP_InputField inputFrecuencia;
    public TMP_InputField inputProfundidad;
    public bool adrenalina = false;
    public bool atropina = false;
    public bool noradrenalina = false;
    public bool amiodarona = false;
    public bool lidocaina = false;
    public bool sulfatoDeMagnesio = false;

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
       // inputFrecuencia.onValueChanged.AddListener(delegate { ValidarInputFrecuencia(); });
       // inputProfundidad.onValueChanged.AddListener(delegate { ValidarInputProfundidad(); });
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
        condiciones = new List<bool>(new bool[28]);
        condiciones[6] = true; 
        condiciones[12] = true;

        nombresCondiciones = new List<string>
        {
        "Se llamo al paciente primero",
        "Se tomo el pulso de segundo",
        "Compresiones antes de 30 segundos",
        "Compresiones con frecuencia entre 100 y 120cpm",
        "Compresiones con profundidad entre 5 y 6cm",
        "Compresiones presentes el 60% del tiempo",
        "Compresiones no fueron iniciadas si el paciente tiene pulso",
        "Manipular desfribilador antes de 30 segundos",
        "Se enciende desfribilador como primera opcion despues de manipularlo",
        "Se elije una carga de 200J despues de encenderse",
        "Se administra descarga si el ritmo es FV",
        "Despues de la descarga se deja de manipular y se inician compresiones",
        "No se administra descarga si el ritmo no es FV",
        "Si el ritmo no es FV la primera accion es dejar de manipular y luego tomar pulso",
        "Ventilaciones iniciadas antes de 45 segundos",
        "Se elije una relacion de 30:2",
        "Se puso un acceso venoso antes de 45 segundos",
        "Se administra adrenalina como primera opcion",
        "Si administra adrenalina la dosis es 1mg",
        "Si administra amiodarona la dosis es 300mg",
        "Si administra lidocaina la dosis es entre 70 y 100mg",
        "La via elegida siempre es intra venosa",
        "Si administra amiodarona no puede ser antes de 6 minutos",
        "Si administra lidocaina no puede ser antes de 6 minutos",
        "No se administra ningun otro medicamento diferente si el paciente esta en paro",
        "Se toma la presion arterial solo si hay pulso",
        "Se pone saturador solo si hay pulso",
        "Se toma electrocardiograma solo si hay pulso"
        };
    }


    void Update()
    {
        //ActualizarTextoCondiciones();
        MostrarResultados();
    }
    public void EstablecerCondicionCumplida(int indice)
    {
        if (indice >= 0 && indice < condiciones.Count)
        {
            condiciones[indice] = true;
        }
    }
    public void MostrarResultados()
    {
        int totalCondiciones = condiciones.Count;
        int condicionesCumplidas = 0;

        string texto = "Condiciones cumplidas:\n";

        for (int i = 0; i < totalCondiciones; i++)
        {
            if (condiciones[i])
            {
                texto += "- " + nombresCondiciones[i] + "\n";
                condicionesCumplidas++;
            }
        }

        float porcentajeCumplidas = ((float)condicionesCumplidas / totalCondiciones) * 100f;
        texto += "Porcentaje de condiciones cumplidas: " + porcentajeCumplidas.ToString("F2") + "%";

        // Mostrar el texto actualizado en el objeto de texto
        textoCondiciones.text = texto;
    }
    void ActualizarTextoCondiciones()
    {
        string texto = "Condiciones cumplidas:\n";

        if (condiciones[0] == true)
        {
            texto += "- Se Llamo Al Paciente Primero\n";
        }
        if (condiciones[1] == true)
        {
            texto += "- Se Tomo El Pulso De Segundo\n";
        }
        if (condiciones[2] == true)
        {
            texto += "- Compresiones Antes De 30 Segundos\n";
        }
        if (condiciones[3] == true)
        {
            texto += "- Compresiones Con Frecuencia entre 100 y 120cpm\n";
        }
        if (condiciones[4] == true)
        {
            texto += "- Compresiones Con Profundidad entre 5 y 6cm\n";
        }
        if (condiciones[6] == true)
        {
            texto += "- Compresiones No Fueron Iniciadas Si El Paciente Tiene Pulso\n";
        }
        if (condiciones[7] == true)
        {
            texto += "- Manipular Desfribilador Antes De 30Segs\n";
        }
        if (condiciones[9] == true)
        {
            texto += "- Se Elije Una Carga De 200J Despues De Encenderse\n";
        }
        if (condiciones[10] == true)
        {
            texto += "- Se Administra Descarga Si El Ritmo Es FV\n";
        }
        if (condiciones[11] == true)
        {
            texto += "- Despues De La Descarga Se Deja De Manipular Y Se Inician Compresiones\n";
        }
        if (condiciones[12] == true)
        {
            texto += "- No Se Administra Descarga Si El Ritmo No Es FV\n";
        }
        if (condiciones[13] == true)
        {
            texto += "- Si El Ritmo No Es FV La Primera Accion Es Dejar De Manipular Y Luego Tomar Pulso\n";
        }
        if (condiciones[14] == true)
        {
            texto += "- Ventilaciones Iniciadas Antes De 45 Segs\n";
        }
        if (condiciones[15] == true)
        {
            texto += "- Se Elije Una Relacion 30/2\n";
        }
        if (condiciones[16] == true)
        {
            texto += "- Se Puso Un Acceso Venoso Antes De 45 Segs\n";
        }
        if (condiciones[17] == true)
        {
            texto += "- Se Administra Adrenalina Como Primera Opcion\n";
        }
        if (condiciones[18] == true)
        {
            texto += "- Si Administra Adrenalina La Dosis Es 1mg\n";
        }
        if (condiciones[19] == true)
        {
            texto += "- Si Administra Amiodarona La Dosis Es 300mg\n";
        }
        if (condiciones[20] == true)
        {
            texto += "- Si Administra Lidocaina La Dosis Es entre 70 y 100mg\n";
        }
        if (condiciones[21] == true)
        {
            texto += "- La Via Elegida Siempre Es Intra Venosa\n";
        }
        if (condiciones[22] == true)
        {
            texto += "- Si Administra Amiodarona No Puede Ser Antes De 6Mins\n";
        }
        if (condiciones[23] == true)
        {
            texto += "- Si Administra Lidocaina No Puede Ser Antes De 6Mins\n";
        }
        if (condiciones[24] == true)
        {
            texto += "- No Se Administra Ningun Otro Medicamento Diferente Si El Paciente Esta En Paro\n";
        }
        if (condiciones[25] == true)
        {
            texto += "- Se Toma La Presion Arterial Solo Si Hay Pulso\n";
        }
        if (condiciones[26] == true)
        {
            texto += "- Se Pone Saturador Solo Si Hay Pulso\n";
        }
        if (condiciones[27] == true)
        {
            texto += "- Se Toma Electrocardiograma Solo Si HayPulso\n";
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
                condiciones[20] = true;
                
            }
            else
            {
                condiciones[20] = false;
            }
        }
        if (inputDosis.text == "1" && adrenalina == true && confirmarDosis == true)
        {
            condiciones[18] = true;
            
        }
        else
        {
            condiciones[18] = false;
        }
        if (inputDosis.text == "300" && amiodarona == true && confirmarDosis == true)
        {
            condiciones[19] = true;
            
        }
        else { condiciones[19] = false; }
    }
    public void ValidarInputFrecuencia()
    {
       
        int numeroIngresado;

        if (int.TryParse(inputFrecuencia.text, out numeroIngresado))
        {
            if (numeroIngresado >= frecuenciaMin && numeroIngresado <= frecuenciaMax)
            {
                condiciones[3] = true;
                
            }
            else
            {
                condiciones[3] = false;
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
                condiciones[4] = true;
                
            }
            else
            {
                condiciones[4] = false;
            }
        }
    }
    public void SeleccionarAdrenalina()
    {
        if (adrenalina == false && amiodarona == false && lidocaina == false)
        {
            condiciones[17] = true;
        }
        adrenalina = true;
        
    }
    public void SeleccionarAmiodarona()
    {
        amiodarona = true;
        
    }
    public void SeleccionarAtropina()
    {
        atropina = true;
       
    }
    public void SeleccionarNoradrenalina()
    {
        noradrenalina = true;
    }
    public void SeleccionarLidocaina()
    {
        lidocaina = true;
        
    }
    public void SeleccionarSulfatoDeMagnesio()
    {
        sulfatoDeMagnesio = true;
        
    }
    public void ConfirmarDosis()
    {
        confirmarDosis = true;
        
        
        ValidarInput(); 
    }
    public void reiniciarMedicamentos()
    {
        adrenalina = false;
        atropina = false;
        noradrenalina = false;
        amiodarona = false;
        lidocaina = false;
        sulfatoDeMagnesio = false;
}
    public void validarVentilaciones()
    {
        condiciones[15] = true;
    }
    void ValidarOrdenBoton(int indiceBoton)
    {
        if (indiceBoton == secuenciaPaciente[indiceSiguientePaciente])
        {
            indiceSiguientePaciente++;
            condiciones[0] = true;

            if (indiceSiguientePaciente >= secuenciaPaciente.Count)
            {

                condiciones[1] = true;
            }
        }
        
    }
    void ValidarOrdenBotonDescarga(int indiceBoton)
    {
        if (indiceBoton == secuenciaDescarga[indiceSiguienteDescarga])
        {
            indiceSiguienteDescarga++;

            if (indiceSiguienteDescarga >= secuenciaDescarga.Count)
            {
                condiciones[11] = true;
            }
        }

        else
        {
            condiciones[11] = false;
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
                condiciones[13] = true;
                // Todas las acciones se han realizado en el orden correcto
                Debug.Log("¡Todas las acciones realizadas en el orden correcto!");
                // Aquí puedes activar otro objeto o hacer cualquier otra acción
            }
        }

        else
        {
            condiciones[13] = false;
            // La acción está fuera de orden
            Debug.Log("¡Error! Acción fuera de orden.");
            // Puedes reiniciar el contador aquí si quieres reiniciar el orden después de un error
            indiceSiguientePulso = 0;
        }
    }
}
