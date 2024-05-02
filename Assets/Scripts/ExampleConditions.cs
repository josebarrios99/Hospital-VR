using System;
using System.Collections.Generic;
using UnityEngine;

public enum CasosAValidar
{
    Llamar_al_paciente,
    Tomar_pulso_como_segunda_accion
}

public class ExampleConditions : MonoBehaviour
{
    private Dictionary<CasosAValidar, Condition> _rubricaValidada;

    private Condition llamarAlPaciente;
    /*private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;
    private Condition llamarAlPaciente;*/

    private void Awake()
    {
        _rubricaValidada = new Dictionary<CasosAValidar, Condition>();
    }

    private void Start()
    {
        llamarAlPaciente = new Condition(CasosAValidar.Llamar_al_paciente, () => _rubricaValidada.Count == 0);
    }

    public void ValidarLlamarPaciente()
    {
        if (_rubricaValidada.ContainsKey(llamarAlPaciente.validationName))
        {
            llamarAlPaciente.ValidarCondicion();
            _rubricaValidada.Add(llamarAlPaciente.validationName, llamarAlPaciente);
        }
    }

    public void ValidacionQueNoSeaIniciadaSiTienePulso()
    {
        _rubricaValidada.TryGetValue(CasosAValidar.Tomar_pulso_como_segunda_accion, out Condition condition);
        if (condition != null && condition.wasValidated)
        {
            llamarAlPaciente.ValidarCondicion();
            _rubricaValidada.Add(llamarAlPaciente.validationName, llamarAlPaciente);
        }
    }
}

public class Condition
{
    public readonly CasosAValidar validationName;
    private readonly Func<bool> _conditionApproval;
    public bool wasValidated;

    public Condition(CasosAValidar validationName, Func<bool> conditionApproval)
    {
        this.validationName = validationName;
        _conditionApproval = conditionApproval;
    }

    public void ValidarCondicion()
    {
        if (_conditionApproval != null)
            wasValidated = _conditionApproval();
        else
            wasValidated = false;
    }
}