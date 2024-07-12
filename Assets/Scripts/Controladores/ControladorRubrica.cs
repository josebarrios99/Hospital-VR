using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Medicamento
{
    Adrenalina,
    Atropina,
    Noradrenalina,
    Amiodarona,
    Lidocaina,
    SulfatoDeMagnesio,
}

public class ControladorRubrica : MonoBehaviour
{
    public static ControladorRubrica instance;

    [SerializeField] private Transform ContenedorRubrica;
    [SerializeField] private ControladorBarraPorcentaje _controladorBarraPorcentaje;
    [SerializeField] private CondicionUI PrefabCondicion;
    [SerializeField] private Controladoracciones controlador;

    [SerializeField] private ListaCondiciones RubricaSo;
    
    private int ?ultimaCondicion = null;
    public int? UltimaCondicion => ultimaCondicion;

    private Medicamento? primerMedicamento = null;
    private Medicamento UltimoMedicamentoSeleccionado;
    private List<Medicamento> ListaDeMedicamentosUtilizados;

    private List<CondicionUI> Condiciones = new List<CondicionUI>();
    private bool CompresionesIniciadas = false;
    private float TiempoCompresiones = 0.0f;

    private bool FirstTimeDesfi = true;
    private bool Descarga = false;
    private List<int> FirstTimeDesfiController;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void OnApplicationQuit()
    {
        RubricaSo.ResetConditions();
    }

    private void Start()
    {
        CrearRubrica();
    }
    private void FixedUpdate()
    {
        if (CompresionesIniciadas && !controlador.pulso)
        {
            TiempoCompresiones += Time.deltaTime;
            _controladorBarraPorcentaje.UpdateSliderValue(TiempoCompresiones);
            float TimeToUpdate = 360f;
            if (TiempoCompresiones >= TimeToUpdate)
            {
                ActualizarRubrica(5);
                controlador.nuevoCiclo("Compresiones Pesentes el 60% del tiempo");
            }
        }
    }

    public void SeIniciaronCompresiones()
    {
        CompresionesIniciadas = true;
        _controladorBarraPorcentaje.EnableDisableSlider(true);
    }

    public void SeDetienenCompresiones()
    {
        CompresionesIniciadas = false;
        _controladorBarraPorcentaje.EnableDisableSlider(false);
    }
    public int GetProgress()
    {
        Condicion[] Conditions = RubricaSo.GetConditions();
        int MaxConditions = Conditions.Length;

        int SuccessConditions = 0;
        foreach (var Condition in Conditions)
        {
            if (Condition.Success)
                SuccessConditions += 1;
        }

        float Procentage = SuccessConditions / MaxConditions;
        return (int)Procentage * 100;
    }
    public void ActualizarRubrica(int Index, bool Success = true)
    {
        if (Descarga && Index != 11)
        {
            Debug.Log($"Update Index : {Index}");
            RubricaSo.UpdateCondicion(11,false);
        }
        switch (Index)
        {
            case 0:
                if (ultimaCondicion != null)
                    RubricaSo.UpdateCondicion(Index, false);
                else
                    RubricaSo.UpdateCondicion(Index);
                break;
            case 1:
                if (ultimaCondicion != 0)
                    RubricaSo.UpdateCondicion(Index, false);
                else
                    RubricaSo.UpdateCondicion(Index);
                break;
            case 7:
            case 8:
            case 9:
                if (FirstTimeDesfi)
                {
                    if (!FirstTimeDesfiController.Contains(Index))
                        FirstTimeDesfiController.Add(Index);
                    if (FirstTimeDesfiController.Count >= 3)
                        FirstTimeDesfi = false;
                    RubricaSo.UpdateCondicion(Index, Success);
                }
                break;
            case 11:
                if (Descarga)
                    RubricaSo.UpdateCondicion(Index);
                break;
            default:
                RubricaSo.UpdateCondicion(Index, Success);
                break;
        }

        Descarga = false;
        ultimaCondicion = Index;
        UpdateRubricaView(Index);
    }

    public Condicion[] ObtenerCondiciones()
    {
        return RubricaSo.GetConditions();
    }

    public bool GetPulso()
    {
        return controlador.pulso;
    }
    public void CrearRubrica()
    {
        ListaDeMedicamentosUtilizados = new List<Medicamento>();
        FirstTimeDesfiController = new List<int>();
        Condicion[] Rubrica = ObtenerCondiciones();

        foreach (var condicion in Rubrica)
        {
            CondicionUI Condition = Instantiate(PrefabCondicion, ContenedorRubrica);
            Condition.SetCondition(condicion.Descripcion, condicion.Success);
            Condiciones.Add(Condition);
        }
    }

    public void UpdateRubricaView(int Index)
    {
        CondicionUI CondicionToUpdate = Condiciones[Index];
        Condicion condicion = RubricaSo.GetCondition(Index);

        CondicionToUpdate.UpdateState(condicion.Success);
    }
    public Medicamento? GetPrimerMedicamento()
    {
        return primerMedicamento;
    }
    public void OnMedicamentoSeleccionado(Medicamento _Medicamento = Medicamento.Adrenalina)
    {
        if (primerMedicamento == null)
            primerMedicamento = _Medicamento;
        UltimoMedicamentoSeleccionado = _Medicamento;
        UseMedicamento();
    }

    public Medicamento? GetUltimoMedicamentoSeleccionado()
    {
        return UltimoMedicamentoSeleccionado;
    }

    public bool MedicamentosUtilizadosCorrectos()
    {
        foreach (var medicamento in ListaDeMedicamentosUtilizados)
        {
            switch (medicamento)
            {
                case Medicamento.Noradrenalina:
                case Medicamento.SulfatoDeMagnesio:
                case Medicamento.Atropina:
                    return false;
            }
        }
        return true;
    }
    public void UseMedicamento()
    {
        ListaDeMedicamentosUtilizados.Add(UltimoMedicamentoSeleccionado);
    }
    
    public void OnDescarga()
    {
        Descarga = true;
    }
}
