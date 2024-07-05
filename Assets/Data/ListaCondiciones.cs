using UnityEngine;

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

    public void ResetCondicion(int Index, bool Success = true)
    {
        Condicion NewState = Condiciones[Index];
        NewState.Reset(Success);
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
                ResetCondicion(i);
            else
                ResetCondicion(i, false);
        }
    }
}
