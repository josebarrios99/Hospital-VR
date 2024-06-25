using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controladoracciones : MonoBehaviour
{
    List<int> ordenes;
    List<int> ordenCorrecto;
    List<string> ordenesTexto;
    public GameObject video;
    public GameObject video2;
    public GameObject jugador;
    public GameObject pantallaFinal;

    [SerializeField] private ControladorRubrica _controladorRubrica; 
    public bool pulso;
    
    // Start is called before the first frame update
    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
        jugador.GetComponent<PlayerController>().CantMove = true;
        jugador.GetComponent<PlayerController>().ActivarCursor();
        video.SetActive(false);
        video2.SetActive(false);
        ordenes = new List<int>();
        ordenesTexto = new List<string>();
        ordenCorrecto = new List<int>()
        {
            1, 2
        };
        // 1, 2 , 3, 4, 5, 2, 3
    }
    private void Update()
    {
        Object cronometro = FindObjectOfType<Crono>();

        if ( cronometro != null  && !pulso) {

            if (FindObjectOfType<Crono>().getTiempoMinutos() >= 10)
            {
                verificarCiclos();
            }
            
        }
        if (cronometro != null && pulso == true)
        {
            if (FindObjectOfType<Crono>().getTiempoMinutos() >= 12)
            {
                jugador.GetComponent<PlayerController>().CantMove = true;
                pantallaFinal.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    public void verificarCiclos()
    {
       double calificacion = finalCiclos();
       Debug.Log(calificacion);
    }
    
    
    
    public double finalCiclos()
    {
        bool esCorrecto = true;
        double calificacion = 0.0;
        double correctos = 0.0;
        
       /* for (int i = 0; i < ordenes.Count; i ++)
        {
            
            if (ordenes[i] == ordenCorrecto[i])
            {
                correctos++;
                FindObjectOfType<Rubrica>().c1 = true;
                FindObjectOfType<Rubrica>().c2 = true;
                Debug.Log("adadad");
            }
        }*/
        
        calificacion = (correctos / ordenCorrecto.Count) * 100;

        pulso = true;

        FindObjectOfType<ObjetoInteractivo>().toraxParo = true;
        FindObjectOfType<ObjetoInteractivo>().presionParo = true;
        FindObjectOfType<ObjetoInteractivo>().saturacionParo = true;

        video.SetActive(true);
        video2.SetActive(true);

        ordenes.Clear();
        return calificacion;
    }
    public bool tienePulso()
    {
        return pulso;
    }
    public void nuevoCiclo(int ciclo, string texto)
    {
        ordenes.Add(ciclo);
        ordenesTexto.Add(texto + " " + FindObjectOfType<Crono>().obtenerTiempo());
    }
    public List<string> retornarLista(){

        return ordenesTexto;
    
    }
}
