using UnityEngine;

public class Tacto : Sensores
{
    private bool col;                                           //Si el jugador esta en contacto con la IA

    public override float GetValue()                            //Se sobreescribe el metodo de la clase heredada
    {
        deteccion += col ? Time.deltaTime : -Time.deltaTime;  //Si se detecta colision, aumenta el valor de alerta
        deteccion = Mathf.Clamp(deteccion, 0, 5);          //Se limita el nivel de alerta para evitar lecturas fuera del limite
        return Gamma(1, 5, deteccion);                        //Se devuelve el valor en una escala sobre 1, siendo un segundo tolerable, 5 no
    }

    private void OnCollisionEnter(Collision collision)          //Se activa la collision al detectar contacto con el jugador
    {
        if (collision.transform == target)
            col = true;
    }

    private void OnCollisionExit(Collision collision)           //Se desactivala collision al detectar que jugador se alejo
    {
        if (collision.transform == target)
            col = false;
    }
}
