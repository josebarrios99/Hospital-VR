using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorBarraPorcentaje : MonoBehaviour
{
    [SerializeField] private GameObject SliderView;
    [SerializeField] private Slider SliderElement;

    public void EnableDisableSlider(bool value)
    {
        Debug.Log($"Se activa el view del slider: {value}");
        SliderView.SetActive(value);
    }
    public void UpdateSliderValue(float value)
    {
        float percentaje = value / 360f;

        SliderElement.value = percentaje;
    }
}
