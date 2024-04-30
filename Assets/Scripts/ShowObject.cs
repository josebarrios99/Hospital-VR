using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{
    public bool activar = false;
    public GameObject objetoParaActivar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activar == true)
        {
            objetoParaActivar.SetActive(true);
        }
        
    }
    void PonerJeringa()
    {
        activar = true;
    }
}
