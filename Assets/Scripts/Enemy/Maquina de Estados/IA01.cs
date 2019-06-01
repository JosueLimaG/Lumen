using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA01 : MonoBehaviour
{
    public Estado pacifico; //Comportamiento pacifico de la IA
    public Estado alerta;   //Comportamiento alerta de la IA
    public Estado agresivo; //Comportamiento agresivo de la IA

    public List<Sensores> sensores = new List<Sensores>();    //Lista de los sentidos con los que cuenta el enemigo
    [HideInInspector] public NavMeshAgent agent;                                //Componente de navegacion de la IA
    [HideInInspector] public Transform player;                                  //Datos de ubicacion del jugador
    public bool iluminado;                                    //El enemigo esta bajo la iluminacion del jugador?
    public float deteccion;                                   //Nivel de deteccion basado en los sentidos del enemigo
    [HideInInspector] public float multiplicador = 1;                           //Nivel del sensibilidad de los sentidos del enemigo

    private Estado estadoActual;                                                //Comportamiento asignado actualmente a la IA

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();                                   //Se obtiene el componente de navegacion de la IA
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();    //Se obtiene la informacion del jugador
        estadoActual = pacifico;                                                //El estado inicial es pacifico
        estadoActual.enabled = true;                                            //Se activa el estado inicial
    }

    private void Update()
    {
        deteccion = 0;                                              //Se resetea el nivel de deteccion en cada frame para evitar acumulaciones

        foreach(Sensores sen in sensores)                           //Se itera una funcion por cada sentido que tiene la IA
        {
            deteccion += sen.GetValue() * sen.importancia;          //Se suma el valor al nivel de deteccion basado en la importancia del sentido actual
        }

        deteccion *= multiplicador;                                 //Se multiplica el nivel de deteccion a modo de sensibilidad

        if (deteccion >= 0.75f && estadoActual == pacifico)         //Si el nivel de deteccion alcanzo cierto punto, la IA se pone en modo alerta
            CambiarEstado(alerta);

        if (deteccion >= 1.5f && estadoActual == alerta)            //Si el nivel de deteccion alcanzo un punto aun mayor, la IA se pone en modo agresivo
            CambiarEstado(agresivo);
    }

    public void CambiarEstado(Estado nuevoEstado)                   //Metodo usado para intercambiar los diferentes comportamientos de la IA
    {
        print("Estado cambiado");
        estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;
    }
}
