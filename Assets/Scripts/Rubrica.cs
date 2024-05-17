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
    private List<bool> condiciones;
    private List<string> nombresCondiciones;


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
        "SeLlamoAlPacientePrimero",
        "SeTomoElPulsoDeSegundo",
        "CompresionesAntesDe30Segundos",
        "CompresionesConFrecuencia100_120cpm",
        "CompresionesConProfundidad5_6cm",
        "CompresionesPresentesEl60DelTiempo",
        "CompresionesNoFueronIniciadasSiElPacienteTienePulso",
        "ManipularDesfribiladorAntesDe30Segs",
        "SeEnciendeDesfribiladorComoPrimeraOpcionDespuesDeManipularlo",
        "SeElijeUnaCargaDe200JDespuesDeEncenderse",
        "SeAdministraDescargaSiElRitmoEsFV",
        "DespuesDeLaDescargaSeDejaDeManipularYSeInicianCompresiones",
        "NoSeAdministraDescargaSiElRitmoNoEsFV = true",
        "SiElRitmoNoEsFVLaPrimeraAccionEsDejarDeManipularYLuegoTomarPulso",
        "VentilacionesIniciadasAntesDe45Segs",
        "SeElijeUnaRelacion30_2",
        "SePusoUnAccesoVenosoAntesDe45Segs",
        "SeAdministraAdrenalinaComoPrimeraOpcion",
        "SiAdministraAdrenalinaLaDosisEs1mg",
        "SiAdministraAmiodaronaLaDosisEs300mg",
        "SiAdministraLidocainaLaDosisEs70_100mg",
        "LaViaElegidaSiempreEsIntraVenosa",
        "SiAdministraAmiodaronaNoPuedeSerAntesDe6Mins",
        "SiAdministraLidocainaNoPuedeSerAntesDe6Mins",
        "NoSeAdministraNingunOtroMedicamentoDiferenteSiElPacienteEstaEnParo",
        "SeTomaLaPresionArterialSoloSiHayPulso",
        "SePoneSaturadorSoloSiHayPulso",
        "SeTomaElectrocardiogramaSoloSiHayPulso"
        };
    }


    void Update()
    {
        ActualizarTextoCondiciones();
        
    }
    void ActualizarTextoCondiciones()
    {
        string texto = "Condiciones cumplidas:\n";

        if (SeLlamoAlPacientePrimero == true)
        {
            texto += "- Se Llamo Al Paciente Primero\n";
        }
        if (SeTomoElPulsoDeSegundo == true)
        {
            texto += "- Se Tomo El Pulso De Segundo\n";
        }
        if (CompresionesAntesDe30Segundos == true)
        {
            texto += "- Compresiones Antes De 30 Segundos\n";
        }
        if (CompresionesConFrecuencia100_120cpm == true)
        {
            texto += "- Compresiones Con Frecuencia entre 100 y 120cpm\n";
        }
        if (CompresionesConProfundidad5_6cm == true)
        {
            texto += "- Compresiones Con Profundidad entre 5 y 6cm\n";
        }
        if (CompresionesNoFueronIniciadasSiElPacienteTienePulso == true)
        {
            texto += "- Compresiones No Fueron Iniciadas Si El Paciente Tiene Pulso\n";
        }
        if (ManipularDesfribiladorAntesDe30Segs == true)
        {
            texto += "- Manipular Desfribilador Antes De 30Segs\n";
        }
        if (SeElijeUnaCargaDe200JDespuesDeEncenderse == true)
        {
            texto += "- Se Elije Una Carga De 200J Despues De Encenderse\n";
        }
        if (SeAdministraDescargaSiElRitmoEsFV == true)
        {
            texto += "- Se Administra Descarga Si El Ritmo Es FV\n";
        }
        if (NoSeAdministraDescargaSiElRitmoNoEsFV == true)
        {
            texto += "- No Se Administra Descarga Si El Ritmo No Es FV\n";
        }
        if (SiElRitmoNoEsFVLaPrimeraAccionEsDejarDeManipularYLuegoTomarPulso == true)
        {
            texto += "- Si El Ritmo No Es FV La Primera Accion Es Dejar De Manipular Y Luego Tomar Pulso\n";
        }
        if (VentilacionesIniciadasAntesDe45Segs == true)
        {
            texto += "- Ventilaciones Iniciadas Antes De 45 Segs\n";
        }
        if (SeElijeUnaRelacion30_2 == true)
        {
            texto += "- Se Elije Una Relacion 30/2\n";
        }
        if (SePusoUnAccesoVenosoAntesDe45Segs == true)
        {
            texto += "- Se Puso Un Acceso Venoso Antes De 45 Segs\n";
        }
        if (SeAdministraAdrenalinaComoPrimeraOpcion == true)
        {
            texto += "- Se Administra Adrenalina Como Primera Opcion\n";
        }
        if (SiAdministraAdrenalinaLaDosisEs1mg == true)
        {
            texto += "- Si Administra Adrenalina La Dosis Es 1mg\n";
        }
        if (SiAdministraAmiodaronaLaDosisEs300mg == true)
        {
            texto += "- Si Administra Amiodarona La Dosis Es 300mg\n";
        }
        if (SiAdministraLidocainaLaDosisEs70_100mg == true)
        {
            texto += "- Si Administra Lidocaina La Dosis Es entre 70 y 100mg\n";
        }
        if (LaViaElegidaSiempreEsIntraVenosa == true)
        {
            texto += "- La Via Elegida Siempre Es Intra Venosa\n";
        }
        if (SiAdministraAmiodaronaNoPuedeSerAntesDe6Mins == true)
        {
            texto += "- Si Administra Amiodarona No Puede Ser Antes De 6Mins\n";
        }
        if (SiAdministraLidocainaNoPuedeSerAntesDe6Mins == true)
        {
            texto += "- Si Administra Lidocaina No Puede Ser Antes De 6Mins\n";
        }
        if (NoSeAdministraNingunOtroMedicamentoDiferenteSiElPacienteEstaEnParo == true)
        {
            texto += "- No Se Administra Ningun Otro Medicamento Diferente Si El Paciente Esta En Paro\n";
        }
        if (SeTomaLaPresionArterialSoloSiHayPulso == true)
        {
            texto += "- Se Toma La Presion Arterial Solo Si Hay Pulso\n";
        }
        if (SePoneSaturadorSoloSiHayPulso == true)
        {
            texto += "- Se Pone Saturador Solo Si Hay Pulso\n";
        }
        if (SeTomaElectrocardiogramaSoloSiHayPulso == true)
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
                SiAdministraLidocainaLaDosisEs70_100mg = true;
                
            }
            else
            {
                SiAdministraLidocainaLaDosisEs70_100mg = false;
            }
        }
        if (inputDosis.text == "1" && adrenalina == true && confirmarDosis == true)
        {
            SiAdministraAdrenalinaLaDosisEs1mg = true;
            
        }
        else
        {
            SiAdministraAdrenalinaLaDosisEs1mg = false;
        }
        if (inputDosis.text == "300" && amiodarona == true && confirmarDosis == true)
        {
            SiAdministraAmiodaronaLaDosisEs300mg = true;
            
        }
        else { SiAdministraAmiodaronaLaDosisEs300mg = false; }
    }
    public void ValidarInputFrecuencia()
    {
       
        int numeroIngresado;

        if (int.TryParse(inputFrecuencia.text, out numeroIngresado))
        {
            if (numeroIngresado >= frecuenciaMin && numeroIngresado <= frecuenciaMax)
            {
                CompresionesConFrecuencia100_120cpm = true;
                
            }
            else
            {
                CompresionesConFrecuencia100_120cpm = false;
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
                CompresionesConProfundidad5_6cm = true;
                
            }
            else
            {
                CompresionesConProfundidad5_6cm = false;
            }
        }
    }
    public void SeleccionarAdrenalina()
    {
        if (adrenalina == false && amiodarona == false && lidocaina == false)
        {
            SeAdministraAdrenalinaComoPrimeraOpcion = true;
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
        SeElijeUnaRelacion30_2 = true;
    }
    void ValidarOrdenBoton(int indiceBoton)
    {
        if (indiceBoton == secuenciaPaciente[indiceSiguientePaciente])
        {
            indiceSiguientePaciente++;
            SeLlamoAlPacientePrimero = true;

            if (indiceSiguientePaciente >= secuenciaPaciente.Count)
            {
                
                SeTomoElPulsoDeSegundo = true;
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
                DespuesDeLaDescargaSeDejaDeManipularYSeInicianCompresiones = true;
            }
        }

        else
        {
            DespuesDeLaDescargaSeDejaDeManipularYSeInicianCompresiones = false;
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
                SiElRitmoNoEsFVLaPrimeraAccionEsDejarDeManipularYLuegoTomarPulso = true;
                // Todas las acciones se han realizado en el orden correcto
                Debug.Log("¡Todas las acciones realizadas en el orden correcto!");
                // Aquí puedes activar otro objeto o hacer cualquier otra acción
            }
        }

        else
        {
            SiElRitmoNoEsFVLaPrimeraAccionEsDejarDeManipularYLuegoTomarPulso = false;
            // La acción está fuera de orden
            Debug.Log("¡Error! Acción fuera de orden.");
            // Puedes reiniciar el contador aquí si quieres reiniciar el orden después de un error
            indiceSiguientePulso = 0;
        }
    }
}
