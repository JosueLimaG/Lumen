using System.Collections;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    public Light spotLight;                 //La luz de la linterna
    public Light pointLight;                //La luz al rededor del personaje

    private bool flick;                     //La linterna debe parpadear?
    private bool flicked;                   //Al parpadera, cambio el valor de la luz?
    private float intensity;                //Intensidad de la luz
    private float mult = 0.05f;             //Multiplicador de la intensidad
    private float frequency;                //La frecuencia a la que debe parpadear la luz
    private float randomFreq;               //Valor menor a la frecuencia para ajustar un valor aleatorio

    void Update()
    {
        float lightVal = GameManager.instance.sensorReader.proxValue != 0 ? GameManager.instance.sensorReader.lightValue : 0;   //Si el sensor de proximidad esta tapado la luz tiene un valor minimo, caso contrario se lee el sensor de luz
        var intensidadSpot = Mathf.Clamp(Ajustar(spotLight.intensity, lightVal * mult, Time.deltaTime * 10), 1f, 35);           //Se limita y se asigna el valor leido por el sensor a la luz de la linterna
        var intensidadPoint = Mathf.Clamp(Ajustar(pointLight.intensity, lightVal * mult, Time.deltaTime * 10), 5f, 35);         //Se limita y se asigna el valor leido por el sensor a la luz del personaje

        spotLight.intensity = intensidadSpot;                       //Se asigna el nuevo valor a la linterna
        pointLight.intensity = intensidadPoint;

        if (flick && !flicked)                                      //La linterna debe parpadear? Estas lineas se ejecutan solo una vez por cada cambio de intensidad
        {
            intensidadSpot += Random.Range(-intensity, intensity);  //Se genera un valor aleatorio de intensidad
            flicked = true;                                         //Se indica al script que el valor cambio
            StartCoroutine(Wait());                                 //Se empieza la corrutina para cambiar la intensidad
        }
    }

     float Ajustar(float input, float target, float velocity)
     {
         return input += input > target ? -velocity : velocity;      //El valor input se ajusta hasta ser igual que el valor target
     }
    /*float Ajustar(float input, float target, float velocity)
    {
        float offset = 0.1f;
        float output = input;

        if (output > target + offset)
            output -= velocity;
        else if (output < target - offset)
            output += velocity;

        return output;
    }*/
       
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(frequency + Random.Range(-randomFreq, randomFreq)); //Se espera por un tiempo variable
        flicked = false;                                                                    //Tras ese lapso se indica que se puede cambiar nuevamente el valor de la intensidad
    }

    public void Flick(bool flick, float intensity, float frequency, float randomFreq)       //Metodo publico para ajustar los parametros del parpadeo de la luz
    {
        this.flick = flick;                 //Indica si debe o no parpadear
        this.intensity = intensity;         //La intensidad del parpadeo
        this.frequency = frequency;         //La frequencia del cambio de iluminacion
        this.randomFreq = randomFreq;       //Valor de aleatoriedad en la frecuencia
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<IA01>().iluminado = true;        //Cuando el collider detecte a un enemigo, se le indica a la IA que puede ser iluminado
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IA01>().iluminado = false;       //Cuando un enemigo abandone un collider, se le indica a la IA que no puede ser iluminado
        }
    }
}
