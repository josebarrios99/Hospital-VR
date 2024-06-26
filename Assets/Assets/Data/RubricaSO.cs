using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public struct Condicion
{
    [SerializeField] private int id;
    [SerializeField] private string descripcion;
    [SerializeField] private bool success;
    [SerializeField] private bool mistake;

    public int ID => id;
    public string Descripcion => descripcion;
    public bool Success => success;

    public void OnSuccess(bool ItsSuccess = true)
    {
        if(!mistake)
            success = ItsSuccess;
    }

    public void OnMistake()
    {
        mistake = true;
        success = false;
    }
}

[CreateAssetMenu(fileName = "Condiciones", menuName = "ValleDelLili/Rubrica", order = 1)]
public class ListaCondiciones : ScriptableObject
{
    [SerializeField] private Condicion[] Condiciones;

    public Condicion GetCondition(int Index)
    {
        return Condiciones[Index];
    }
    public void UpdateCondicion(int Index, bool Success = true)
    {
        Condicion NewState = Condiciones[Index];
        if(!Success)
            NewState.OnMistake();
        NewState.OnSuccess(Success);
        Condiciones[Index] = NewState;
    }
    public Condicion[] GetConditions()
    {
        return Condiciones;
    }

    public void ResetConditions()
    {
        for (int i = 0; i < Condiciones.Length -1; i++)
        {
            if (i == 6 || i == 12)
                UpdateCondicion(i);
            else
                UpdateCondicion(i, false);
        }
    }
}