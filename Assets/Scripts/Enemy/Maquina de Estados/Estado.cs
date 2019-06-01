using UnityEngine;

public class Estado : MonoBehaviour
{
    protected IA01 ia;                          //Script de la IA
    protected Animator anim;                    //El animator del enemigo
    [HideInInspector] public Transform player;  //El target de la IA
    [HideInInspector] public Estado pacifico;   //Comportamiento pacifico de la IA
    [HideInInspector] public Estado alerta;     //Comportamiento alerta de la IA
    [HideInInspector] public Estado agresivo;   //Comportamiento agresivo de la IA

    void Awake()
    {
        ia = GetComponent<IA01>();                              //Se obtiene el script de la IA
        anim = GetComponent<Animator>();                        //Se obtiene al animator del enemigo
        player = GameObject.FindWithTag("Player").transform;    //Se busca al personaje
        pacifico = ia.pacifico;                                 //Se obtiene el comportamiento pacifico asignado a la IA
        alerta = ia.alerta;                                     //Se obtiene el comportamiento alerta asignado a la IA
        agresivo = ia.agresivo;                                 //Se obtiene el comportamiento agresivo asignado a la IA
    }

    public float CalcularAngulo(Vector3 objectA, Vector3 objectB)   //Metodo publico usado por los distintos estados para calcular el angulo entre dos objetos
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
}