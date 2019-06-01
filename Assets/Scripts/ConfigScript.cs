using UnityEngine;

public class ConfigScript : MonoBehaviour
{
    public float gyroSensibility;       //Sensibilidad del giroscopio
    public float touchSensibility;      //Sensibilidad del touch
    public int emulatedInput;           //Uso del control tactil o giroscopio
    public int textExtraDuration;     //Tiempo extra a la vida de los textos
    public string playerName;           //Nombre del jugador
    public int language;                //Lenguaje seleccionado
    public float lightSenibility;       //Sensibilidad del sensor de luz
    public float micSensibility;        //Sensibilidad del microfono
    public float resolution;            //Escalado de la resolucion de pantalla
    public int quality;                 //Nivel de calidad grafica

    private void Awake()
    {
        LoadPreferences();              //Al inicial se cargan los datos guardados
    }

    private void Start()
    {
        GameManager.instance.config = this; //Se acopla el modulo al GameManager para compartir los parametros
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.L))
            DeletePreferences();
    }

    public void DeletePreferences()     //Metodo publico usado para reiniciar la configuracion
    {
        print("borrando preferencias");
        PlayerPrefs.DeleteAll();        //Se borran todos los parametros guardados
        print("cargando valores por defecto");
        LoadPreferences();              //Se cargan los valores por defecto
        print("preferencias restauradas");
    }

    public void LoadPreferences()       //Se cargan los valores guardados, si no hay nada guardado se cargan valores por defecto
    {
        language = PlayerPrefs.GetInt("Lang", 0);
        gyroSensibility = PlayerPrefs.GetFloat("gyroSen", 20);
        touchSensibility = PlayerPrefs.GetFloat("touchSen", 0.02f);
        micSensibility = PlayerPrefs.GetFloat("micSen", 4);
        lightSenibility = PlayerPrefs.GetFloat("lightSen", 1);
        emulatedInput = PlayerPrefs.GetInt("emulatedInput", 0);
        resolution = PlayerPrefs.GetFloat("resolution", 9);
        quality = PlayerPrefs.GetInt("quality", 3);
        textExtraDuration = PlayerPrefs.GetInt("textExtraDuration", 0);
    }

    public void SavePreferences()       //Se guarda la informacion
    {
        PlayerPrefs.SetInt("Lang", language);
        PlayerPrefs.SetFloat("gyroSen", gyroSensibility);
        PlayerPrefs.SetFloat("touchSen", touchSensibility);
        PlayerPrefs.SetFloat("micSen", micSensibility);
        PlayerPrefs.SetFloat("lightSen", lightSenibility);
        PlayerPrefs.SetInt("emulatedInput", emulatedInput);
        PlayerPrefs.SetFloat("resolution", resolution);
        PlayerPrefs.SetInt("quality", quality);
        PlayerPrefs.SetInt("textExtraDuration", textExtraDuration);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SavePreferences();              //Cuando se sale de la aplicacion, se guardan los parametros actuales
    }
}
