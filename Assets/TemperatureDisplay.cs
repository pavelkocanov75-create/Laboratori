using UnityEngine;
using TMPro;

public class TemperatureDisplay : MonoBehaviour
{
    public TMP_Text temperatureText;
    public Experiment experiment;
    
    void Update()
    {
        if (experiment != null && temperatureText != null)
        {
            temperatureText.text = "Температура воды: " + experiment.waterTemperature.ToString("F1") + "°C";
            
            if (experiment.waterTemperature > 50f)
                temperatureText.color = Color.red;
            else if (experiment.waterTemperature > 30f)
                temperatureText.color = Color.yellow;
            else
                temperatureText.color = Color.white;
        }
    }
}