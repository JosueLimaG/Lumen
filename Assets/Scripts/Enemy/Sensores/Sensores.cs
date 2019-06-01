using UnityEngine;

public class Sensores : MonoBehaviour
{
    public float deteccion;   //Nivel de deteccion del sensor
    [HideInInspector] public IA01 ia;           //Acceso al script de la IA
    [HideInInspector] public Transform target;  //El objeto a detectar, generalmente el jugador
    public float importancia = 1;               //Nivel de importancia del sensor actual

    void Start()
    {
        ia = GetComponent<IA01>();              //Se obtiene la informacion del script de la IA
        ia.sensores.Add(this);                  //Se anade el sensor actual a la lista de sentidos de la IA
        target = ia.player;                     //Se obtiene el target de la IA
    }

    public virtual float GetValue()             //Metodo virtual sobreescribible usado para obtener la lectura del sensor actual
    {
        return 0;
    }

    public float CalcularAngulo(Vector3 objectA, Vector3 objectB)   //Metodo publico usado por los sensores para calcular el angulo entre dos objetos
    {
        float angle = Mathf.Atan((objectB.x - objectA.x) / (objectB.z - objectA.z)) * Mathf.Rad2Deg;

        if (objectA.z > objectB.z)
        {
            if (objectA.x < objectB.x)
                angle = ((-90 - angle) * -1) + 90;
            else if (objectA.x > objectB.x)
                angle = ((90 - angle) * -1) - 90;
            else if (objectA.x == objectB.x)
                angle = 180;
        }

        return angle;
    }

    public float Gamma(float a, float m, float x)                   //Metodo publico usado por los sensores para calcular el valor gamma de sus lecturas
    {
        if (x <= a)
            return 0;
        else if (x > a && x < m)
            return (x - a) / (m - a);
        else
            return 1;
    }
}
