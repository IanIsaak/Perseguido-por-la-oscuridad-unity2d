using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    private Slider slider;
    private float currentStamina;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxStamina(float maxStamina)
    {
        slider.maxValue = maxStamina;
        currentStamina = maxStamina;
        slider.value = currentStamina;
    }

    public void ChangeStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, slider.maxValue);
        slider.value = currentStamina;
    }

    public void InitializeStamina(float stamina)
    {
        slider.maxValue = stamina;
        currentStamina = stamina;
        slider.value = stamina;
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    public float GetMaxStamina()
    {
        return slider.maxValue;
    }
}
