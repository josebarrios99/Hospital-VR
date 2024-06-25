using System;
using UnityEngine;

[Serializable]
public struct Condicion
{
    [SerializeField] private int id;

    public int ID => id;
    public string Descripcion => descripcion;
    public bool Success => success;

    [SerializeField] private string descripcion;
    [SerializeField] private bool success;

    public void OnSuccess(bool ItsSuccess)
    {
        success = ItsSuccess;
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

    public Condicion[] GetConditions()
    {
        return Condiciones;
    }
}