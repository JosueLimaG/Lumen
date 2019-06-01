using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float bateria;               //Seria interesante aplicar una mecanica basada en el nivel de bateria del movil
    public static GameManager instance; //Referencia global del script
    private string[] names;             //Se obtiene una lista de los niveles graficos disponibles

    public Text text;

    //Modulos de funcionamiento del juego, cada uno se acopla al GameManager al iniciar, este ultimo es usado como puente de comunicacion
    [HideInInspector] public InputScript input;                 //Modulo de reconocimiento del gyroscopio y pantalla
    [HideInInspector] public MicScript mic;                     //Modulo de reconocimiento del microfono
    [HideInInspector] public SensorBehaviour sensorReader;      //Modulo de reconocimiento de los demas sensores
    [HideInInspector] public ConfigScript config;               //Modulo que guarda la informacion persistente
    [HideInInspector] public LanguageScript langReader;         //Modulo que lee la informacion que varia al cambiar el idioma del juego
    [HideInInspector] public AudioScript audioS;                //Modulo que controla la configuracion de audio

    [HideInInspector] public float ruidoGenerado;               //El ruido generado por el personaje y el microfono

    private void Awake()                        //En el Awake se seleccciona el singleton del GameManager
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //El objet no debe ser destruido al cambiar de nivel
        }
        else
            Destroy(this.gameObject);           //Si ya existe un GameManager funcionando, se destruye el objeto actual
    }

    void Start()
    {
        Application.targetFrameRate = 30;                       //Los FPS ideales son los 30
        Screen.sleepTimeout = SleepTimeout.NeverSleep;          //La pantalla no debe apagarse ni atenuarse
        bateria = SystemInfo.batteryLevel;   
        Handheld.Vibrate();
        names = QualitySettings.names;                          //Se obtiene una lista con las calidades graficas disponibles

        EmulatedInput(config.emulatedInput == 1 ? true : false);//Se crea el control seleccionado
        CambioDeCalidad(config.quality);
    }

    private void Update()
    {
        text.text = QualitySettings.resolutionScalingFixedDPIFactor.ToString();
    }

    public void Pausa(bool pausa)
    {
        input.playing = pausa ? false : true;       //Si el juego esta en pausa se permite comunicarlo a los demas modulos
        Time.timeScale = pausa ? 0 : 1;             //Al estar en pausa se detiene el timepo del juego.
    }

    public void LoadLevel(int level)
    {
        level = level > SceneManager.sceneCount ? 0 : level;           //Si el nivel a cargar recibido no existe, se carga la primera escena del build
        SceneManager.LoadScene(level);                                  //Se carga el nivel seleccionado
    }

    public void LoadLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;               //Se obtiene el index de la escena actual
        scene += (scene + 1) > SceneManager.sceneCount ? -scene : 1;        //Si la escena actual es la ultima del build, se vuelve a la escena 0
        SceneManager.LoadScene(scene);
    }

    public void CambioDeCalidad(int calidad)
    {
        config.quality = calidad;                                           //Se guarda la calidad actual como dato persistente
        QualitySettings.SetQualityLevel(calidad, true);                     //Se carga la calidad seleccionada en tiempo real
    }

    public void DPI(float scale)                                            //Metodo usado para cambiar el escalado de pantalla
    {
        int qLevels = QualitySettings.names.Length;                         //
        int qCurrent = QualitySettings.GetQualityLevel();

        for(int i = 0; i <= qLevels; i++)
        {
            QualitySettings.SetQualityLevel(i, false);
            QualitySettings.resolutionScalingFixedDPIFactor = scale;
        }

        QualitySettings.SetQualityLevel(qCurrent, true);
    }

    public void EmulatedInput(bool value)                       //Metodo usado para cargar el modulo del input simulado o por giroscopio
    {
        if (value) gameObject.AddComponent<EmulatedInputScript>();
        else gameObject.AddComponent<SensorInputScript>();
    }

    public void ChangeMic(int newMic)   //Metodo utilizado para cambiar el microfono actual
    {
#if !UNITY_EDITOR                       //No debe correr en el editor o crea un bucle infinito
        //mic.Initialize(newMic);
#endif
    }

    private void OnLevelWasLoaded(int level)    //Metodo usado para despausar el juego luego de cargar un nivel
    {
        try {
            Pausa(false);                       //Se quita la pausa, devolviendo la velocidad normal al juego
            langReader.LoadLang();              //Se carga la informacion del idioma
        }               
        catch { print("Nivel cargado?"); }      //Al cargar el nivel se produce por un frame un error de lectura de los modulos, este catch evita mayores errores
    }
}