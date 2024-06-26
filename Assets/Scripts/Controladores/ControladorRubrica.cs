using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorRubrica : MonoBehaviour
{
    public static ControladorRubrica instance;
    
    [SerializeField] private Transform ContenedorRubrica;
    [SerializeField] private CondicionUI PrefabCondicion;
        
    [SerializeField] private ListaCondiciones RubricaSo;

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

    public void ActualizarRubrica(int Index, bool Success = true)
    {
        if(!Success)
            
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
        Debug.Log("Estamos Actualizando la Rubrica");
        CondicionUI CondicionToUpdate = Condiciones[Index];
        Condicion condicion = RubricaSo.GetCondition(Index);

        CondicionToUpdate.UpdateState(condicion.Success);
    }
}
