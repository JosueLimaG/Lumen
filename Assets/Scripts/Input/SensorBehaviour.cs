using UnityEngine;
using UnityAndroidSensors.Scripts.Utils.SmartVars;

public class SensorBehaviour : MonoBehaviour
{
    public FloatVar lightInfo;  //SmartVar donde se almacena las lecturas del sensor de luz
    public FloatVar proxInfo;   //SmartVar donde se almacena las lecturas del sensor de proximidad
    public float lightValue;    //El valor final de a luz registrada
    public float sensibilidad;  //La sensibilidad del sensor
    public float proxValue;     //El valor final de la proximidad. 5 = no hay nada proximo al sensor, 0 = se esta tapando

    private void Start()
    {
        GameManager.instance.sensorReader = this;                   //Se acopla el script al GameManager
        sensibilidad = GameManager.instance.config.lightSenibility; //Se obtiene el valor de la sensibilidad
    }

    private void Update()
    {
        lightValue = lightInfo.value * sensibilidad;                //Se lee el sensor de luz y se multiplica por la sensibilidad
        proxValue = Mathf.RoundToInt(proxInfo.value);               //Se redondea el valor registrado por el sensor de proximidad
    }
}