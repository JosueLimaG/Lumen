using UnityEngine;
using UnityEngine.UI;

public enum types { gyro, touch, light, mic, res }

public class SliderConfigScript : MonoBehaviour
{
    public types tipo;
    private Slider slider;
    private ConfigScript cfg;
    private bool changeRes = false;

    void Start()
    {
        cfg = GameManager.instance.config;
        slider = GetComponent<Slider>();

        slider.minValue = 0;

        switch (tipo)
        {
            case types.gyro:
                slider.maxValue = 60;
                slider.value = cfg.gyroSensibility;
                break;
            case types.light:
                slider.maxValue = 3;
                slider.value = cfg.lightSenibility;
                break;
            case types.mic:
                slider.maxValue = 10;
                slider.value = cfg.micSensibility;
                break;
            case types.touch:
                slider.maxValue = 0.1f;
                slider.value = cfg.touchSensibility;
                break;
            case types.res:
                slider.minValue = 4;
                slider.maxValue = 9;
                slider.value = cfg.resolution;
                break;
        }
    }

    public void OnValueChanged()
    {
        switch (tipo)
        {
            case types.gyro:
                cfg.gyroSensibility = slider.value;
                GameManager.instance.input.sensibilidadGyro = slider.value;
                break;
            case types.light:
                cfg.lightSenibility = slider.value;
                GameManager.instance.sensorReader.sensibilidad = slider.value;
                break;
            case types.mic:
                cfg.micSensibility = slider.value;
                GameManager.instance.mic.sensibilidad = slider.value;
                break;
            case types.touch:
                cfg.touchSensibility = slider.value;
                GameManager.instance.input.sensibilidadTouch = slider.value;
                break;
            case types.res:
                changeRes = true;
                break;
        }
    }

    private void Update()
    {
        if(Input.touchCount == 0 && changeRes)
        {
            cfg.resolution = slider.value / 9;
            GameManager.instance.DPI(slider.value / 9);
            changeRes = false;
        }
    }
}
