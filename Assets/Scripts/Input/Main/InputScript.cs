using UnityEngine;

public class InputScript : MonoBehaviour
{
    public float sensibilidadTouch;                     //Sensibilidad de la lectura del toque de pantalla
    public float sensibilidadGyro;                      //Sensibilidad de la lectura del giroscopio
    [HideInInspector] public bool gyroControl = false;  //El scipt es por giroscopio?
    [HideInInspector] public Vector3 inputMovement;     //Variable donde se almacenara el valor de entrada
    [HideInInspector] public bool playing = true;       //Si el juego esta en pausa, o no debe registrarse movimiento esto va en false
    [HideInInspector] public bool touching;             //Se esta tocando la pantalla?

    private void Start()
    {
        if (GameManager.instance.input != this)
        {
            Destroy(GameManager.instance.input);
            GameManager.instance.input = this;
        }

        sensibilidadTouch = GameManager.instance.config.touchSensibility;   //Se obtiene la sensibilidad de el touch
        sensibilidadGyro = GameManager.instance.config.gyroSensibility;     //Se obtiene la sensibilidad del giroscopio
    }

    void Update()
    {
        inputMovement = GetInputs();                    //Se lee el input cada frame
    }

    public virtual Vector3 GetInputs()                  //Metodo sobreescribible usado como lectura de entrada
    {
        return new Vector3();                           //Aunque esto nunca se ejecute, si no le pongo nada da error :v
    }
}
