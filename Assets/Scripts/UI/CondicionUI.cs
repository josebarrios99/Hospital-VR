using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CondicionUI : MonoBehaviour
{
    [SerializeField] private Text Descripcion;
    [SerializeField] private Toggle Check;

    public void SetCondition(string Description , bool Success)
    {
        Descripcion.text = Description;
        Check.isOn = Success;
    }

    public string GetDescription()
    {
        return Descripcion.text;
    }
    
    public void UpdateState(bool Success)
    {
        Debug.Log($"State its {Success}");
        Check.isOn = Success;
    }
}
