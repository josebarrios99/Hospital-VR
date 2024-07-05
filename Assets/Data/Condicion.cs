using System;
using UnityEngine;

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

    public void Reset(bool Success)
    {
        mistake = false;
        success = Success;
    }
}