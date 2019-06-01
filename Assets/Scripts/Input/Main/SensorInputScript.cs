using UnityEngine;

public class SensorInputScript : InputScript
{
    public Vector3 gyro;                            //Obtiene la informacion actual del giroscopio
    private Vector3 gyroLastPos;                    //Guarda la posicion actual y la usa como referencia del giroscopio
    private Vector3 inputGyro;                      //Calcula la inclinacion del celular tomando como referencia a gyroLastPos
    private Vector2 touchPos;                       //Usado para guardar la posicion del toque de pantalla
    private Vector2 inputTouch;                     //Usado para leer la posicion actual del toque

    private void Awake()
    {
        Input.gyro.enabled = true;
        gyroControl = true;
    }

    public override Vector3 GetInputs()
    {
        gyro = Input.gyro.gravity;
        gyro = gyro.z < 0 ? gyro : new Vector3(-gyro.x, gyro.y < 0 ? -1 - (1 + gyro.y) : 1 + (1 - gyro.y), gyro.z);

        if (GameManager.instance.sensorReader.proxValue != 5)
            gyroLastPos = gyro;                                         //Cuando se deja de tocar la pantalla se guarda la informacion del giroscopio

        if (Input.touches.Length > 0)                                   //Pregunta si se esta tocando la pantalla
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchPos = Input.touches[0].position;                   //Cuando se detecta el toque se guarda la posicion del dedo
                inputTouch = Input.touches[0].position - touchPos;      //A la posicion actual del dedo se le resta la posicion inicial guardada
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
                inputTouch = Input.touches[0].position - touchPos;      //A la posicion actual del dedo se le resta la posicion inicial guardada
            
            touching = true;                                            //Se toca la pantalla
            return inputTouch * sensibilidadTouch;
        }
        else
        {
            inputGyro = gyro - gyroLastPos;                             //A la informacion actual del giroscopio se le resta la posicion guardada
            touching = false;                                           //No se esta tocando la pantalla
            return inputGyro * sensibilidadGyro;
        }
    }
}
