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
            texto += "- Condición 1\n";
        }
        if (SeTomoElPulsoDeSegundo == true)
        {
            texto += "- Condición 2\n";
        }
        if (CompresionesAntesDe30Segundos == true)
        {
            texto += "- Condición 3\n";
        }
        if (CompresionesConFrecuencia100_120cpm == true)
        {
            texto += "- Condición 4\n";
        }
        if (CompresionesConProfundidad5_6cm == true)
        {
            texto += "- Condición 5\n";
        }
        if (CompresionesNoFueronIniciadasSiElPacienteTienePulso == true)
        {
            texto += "- Condición 7\n";
        }
        if (ManipularDesfribiladorAntesDe30Segs == true)
        {
            texto += "- Condición 8\n";
        }
        if (SeElijeUnaCargaDe200JDespuesDeEncenderse == true)
        {
            texto += "- Condición 10\n";
        }
        if (SeAdministraDescargaSiElRitmoEsFV == true)
        {
            texto += "- Condición 11\n";
        }
        if (NoSeAdministraDescargaSiElRitmoNoEsFV == true)
        {
            texto += "- Condición 13\n";
        }
        if (SiElRitmoNoEsFVLaPrimeraAccionEsDejarDeManipularYLuegoTomarPulso == true)
        {
            texto += "- Condición 14\n";
        }
        if (VentilacionesIniciadasAntesDe45Segs == true)
        {
            texto += "- Condición 15\n";
        }
        if (SeElijeUnaRelacion30_2 == true)
        {
            texto += "- Condición 16\n";
        }
        if (SePusoUnAccesoVenosoAntesDe45Segs == true)
        {
            texto += "- Condición 17\n";
        }
        if (SeAdministraAdrenalinaComoPrimeraOpcion == true)
        {
            texto += "- Condición 18\n";
        }
        if (SiAdministraAdrenalinaLaDosisEs1mg == true)
        {
            texto += "- Condición 19\n";
        }
        if (SiAdministraAmiodaronaLaDosisEs300mg == true)
        {
            texto += "- Condición 20\n";
        }
        if (SiAdministraLidocainaLaDosisEs70_100mg == true)
        {
            texto += "- Condición 21\n";
        }
        if (LaViaElegidaSiempreEsIntraVenosa == true)
        {
            texto += "- Condición 22\n";
        }
        if (SiAdministraAmiodaronaNoPuedeSerAntesDe6Mins == true)
        {
            texto += "- Condición 23\n";
        }
        if (SiAdministraLidocainaNoPuedeSerAntesDe6Mins == true)
        {
            texto += "- Condición 24\n";
        }
        if (NoSeAdministraNingunOtroMedicamentoDiferenteSiElPacienteEstaEnParo == true)
        {
            texto += "- Condición 25\n";
        }
        if (SeTomaLaPresionArterialSoloSiHayPulso == true)
        {
            texto += "- Condición 26\n";
        }
        if (SePoneSaturadorSoloSiHayPulso == true)
        {
            texto += "- Condición 27\n";
        }
        if (SeTomaElectrocardiogramaSoloSiHayPulso == true)
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
        Debug.Log("conejo");
        int numeroIngresado;

        if (int.TryParse(inputFrecuencia.text, out numeroIngresado))
        {
            if (numeroIngresado >= frecuenciaMin && numeroIngresado <= frecuenciaMax)
            {
                CompresionesConFrecuencia100_120cpm = true;
                Debug.Log("conejo");
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
