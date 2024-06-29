using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Unity.VisualScripting;
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
    [SerializeField] private CondicionUI PrefabCondicion;

    [SerializeField] private ListaCondiciones RubricaSo;
    
    private int ?ultimaCondicion = null;
    public int? UltimaCondicion => ultimaCondicion;

    private Medicamento? primerMedicamento = null;
    private Medicamento? UltimoMedicamentoSeleccionado = null;

    private List<CondicionUI> Condiciones = new List<CondicionUI>();
    
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
        if (Index == 0)
        {
            if (ultimaCondicion != null)
            {
                RubricaSo.UpdateCondicion(Index, false);
            }
        }

        if (Index == 1)
        {
            if (ultimaCondicion != 0)
            {
                RubricaSo.UpdateCondicion(Index, false);
            }
        }
        ultimaCondicion = Index;
        RubricaSo.UpdateCondicion(Index, Success);
        UpdateRubrica(Index);
    }

    public Condicion[] ObtenerCondiciones()
    {
        return RubricaSo.GetConditions();
    }

    public void CrearRubrica()
    {
        Condicion[] Rubrica = ObtenerCondiciones();

        foreach (var condicion in Rubrica)
        {
            CondicionUI Condition = Instantiate(PrefabCondicion, ContenedorRubrica);
            Condition.SetCondition(condicion.Descripcion, condicion.Success);
            Condiciones.Add(Condition);
        }
    }

    public void UpdateRubrica(int Index)
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
    }

    public Medicamento? GetUltimoMedicamentoSeleccionado()
    {
        return UltimoMedicamentoSeleccionado;
    }
}
