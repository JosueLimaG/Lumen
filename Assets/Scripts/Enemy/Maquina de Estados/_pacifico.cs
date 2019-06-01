using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class _pacifico : Estado
{
    public Transform[] puntosDePatrulla;    //Puntos de patrulla de la IA
    public bool patrulla = true;            //La IA esta patrullando?

    private NavMeshPath path;               //El camino al siguiente punto de patrullla
    private int idleAnims = 2;              //Numero de animaciones Idle
    private int destinoActual;              //Indicador actual de punto de patrulla
    private float tiempoDeEspera = 5;       //Tiempo que tomara en desanso antes de dirigirse al siguiente punto de patrulla

    void OnEnable()
	{
        path = new NavMeshPath();           //Se crea una variable donde se pueda guardar la informacion de la ruta de la IA
        ia.agent.speed = 1;                 //La velocidad del movimiento en estado pacifico es baja
        ia.multiplicador = 1;               //El nivel de deteccion de la IA es normal
        alerta.enabled = false;             //El comportamiento alerta de la IA es apagado
        agresivo.enabled = false;           //El comportamiento agresivo de la IA es apagado

        if (patrulla)                       //La IA debe patrullar?
            StartCoroutine(Destino(puntosDePatrulla[destinoActual].position, 1)); //Se le asigna un objetivo de ruta a la IA pasado 1 segundo
    }

    void Update()
    {
        if (patrulla)                       //La IA debe patrullar?
        {
            if (Vector3.Distance(puntosDePatrulla[destinoActual].position, transform.position) < 0.2f)  //La IA llego a su destino?
            {
                destinoActual++;                                                                        //Se obtiene el siguiente punto de patrulla
                destinoActual = destinoActual >= puntosDePatrulla.Length ? 0 : destinoActual;           //Si no existe un siguiente punto de patrulla se le asigna el inicial
                anim.SetBool("Walk", false);                                                            //Se le dice al Animator que ya no se esta caminando
                StartCoroutine(Destino(puntosDePatrulla[destinoActual].position, tiempoDeEspera));      //Se le asigna a la IA el siguiente punto de patrulla pasado el timepo de espera
            }

            if (ia.agent.speed == 0)                                                                                            //La velocidad es 0? Este tiempo se usa para girar al enemigo al angulo deseado
            {
                NavMesh.CalculatePath(transform.position, puntosDePatrulla[destinoActual].position, NavMesh.AllAreas, path);    //Se guarda en el "path" la ruta al punto actual
                float angle = CalcularAngulo(transform.position, path.corners[1]);                                              //Se calcula el angulo entre la IA y la siguiente curva en su camino
                Quaternion targetAngle = Quaternion.Euler(0, angle, 0);                                                         //Con el angulo obtenido se obtiene la rotacion objetivo de la IA
                transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, Time.deltaTime * 0.5f);                   //Se gira la IA hasta llegar a la rotacion objetivo
            }
        }
    }

    IEnumerator Destino(Vector3 destino, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);        //Se espera por un tiempo determinado
        ia.agent.SetDestination(destino);               //Se le indica a la IA el siguiente destino
        ia.agent.speed = 0f;                            //La velocidad es 0, para acomodar a la IA en la rotacion correcta antes de empezar a moverse
        StartCoroutine(Mover());                        //Se le indica que se mueva
    }

    IEnumerator Mover()
    {
        yield return new WaitForSeconds(5);             //Se espera por un tiempo determinado
        ia.agent.speed = 1f;                            //La velocidad de la IA se setea a 1
        anim.SetBool("Walk", true);                     //Se le indica al Animator que la IA esta caminando
    }

    public void IdleAnim()                                                  //Metodo usado para aplicar animaciones Idle aleatorias
    {                                                                       //Se le llama al final de cada animacion
        anim.SetInteger("Idle", Random.Range(-idleAnims, idleAnims));       //Se le indica al animator un numero de animacion
    }
}