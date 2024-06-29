using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controladoracciones : MonoBehaviour
{
    [SerializeField] private ControladorRubrica _controladorRubrica;
    [SerializeField] private TMP_Text ListGO;
    [SerializeField] private Transform ListTransform;
    [SerializeField] private Crono TimeController;
    
    public GameObject video;
    public GameObject video2;
    public GameObject jugador;
    private PlayerController ControladorJugador;
    public GameObject pantallaFinal;

    public bool pulso;
    
    // Start is called before the first frame update
    void Start()
    {
        _controladorRubrica = ControladorRubrica.instance;
        TimeController = GameObject.FindGameObjectWithTag("RealCrono").GetComponent<Crono>();
        
        ControladorJugador = jugador.GetComponent<PlayerController>();
        ControladorJugador.CantMove = true;
        ControladorJugador.ActivarCursor();
        
        video.SetActive(false);
        video2.SetActive(false);
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
        if (cronometro != null && pulso)
        {
            if (FindObjectOfType<Crono>().getTiempoMinutos() >= 12)
            {
                ControladorJugador.CantMove = true;
                pantallaFinal.SetActive(true);
            }
        }
    }
    
    public void verificarCiclos()
    {
       double calificacion = finalCiclos();
    }
    
    public double finalCiclos()
    {
        double calificacion = 0.0;

        calificacion = _controladorRubrica.GetProgress();
    
        pulso = true;
    
        FindObjectOfType<ObjetoInteractivo>().toraxParo = true;
        FindObjectOfType<ObjetoInteractivo>().presionParo = true;
        FindObjectOfType<ObjetoInteractivo>().saturacionParo = true;
    
        video.SetActive(true);
        video2.SetActive(true);
        return calificacion;
    }
    public bool tienePulso()
    {
        return pulso;
    }
    public void nuevoCiclo(string texto)
    {
        TMP_Text Text = Instantiate(ListGO, ListTransform);
        Text.text = TimeController.obtenerTiempo() + ": " + texto;
    }
}
