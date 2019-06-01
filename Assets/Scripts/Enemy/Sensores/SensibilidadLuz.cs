using UnityEngine;

public class SensibilidadLuz : Sensores
{
    public override float GetValue()                                            //Se sobreescribe el metodo de la clase heredada
    {
        float val = Gamma(1, 35, GameManager.instance.sensorReader.lightValue); //Se obtiene un valor gamma de la iluminacion que genera el jugador
        deteccion += ia.iluminado ? val : -Time.deltaTime;                      //Si la IA esta siendo iluminada se aumenta el nivel de deteccion
        deteccion = Mathf.Clamp(deteccion, 0, 5);                               //Se limita el nivel de deteccion para evitar lecturas fuera del limite
        return Gamma(1, 5, deteccion);                                          //Se devuelve el valor en una escala sobre 1, siendo un segundo tolerable, 5 no
    }
}