using UnityEngine;

public class _alerta : Estado
{
    public float tiempoAlerta = 5;         //Tiempo que durara el estado de alerta del enemigo
    private float timer;                    //El timepo transcurrido a comaprar con el timepoAlerta
    private bool deteccion;                 //Si el enemigo esta detectando algo o no
    private float tempDeteccion;            //Variable donde se almacena el ultimo valor de deteccion

    void OnEnable()
    {
        ia.agent.speed = 0.5f;              //La velocidad del movimiento en estado pacifico es baja
        ia.multiplicador = 1;               //El nivel de deteccion de la IA es mayor
        pacifico.enabled = false;             //El comportamiento alerta de la IA es apagado
        agresivo.enabled = false;           //El comportamiento agresivo de la IA es apagado
    }

    void Update()
    {
        deteccion = ia.deteccion > tempDeteccion ? true : false;    //Si la deteccion aumenta o no, se almacena en una variable
        timer += deteccion ? -timer : Time.deltaTime;               //Si el enemigo no detecta nada, se empieza a contar el tiempo transcurrido
        timer = Mathf.Clamp(timer, 0, tiempoAlerta);                //Se limita el control del tiempo para evitar que se salga de los limites
        tempDeteccion = ia.deteccion;                               //Se guarda el ultimo valor de la deteccion de la IA para su posterior comparacion

        if (timer >= tiempoAlerta)                                  //Paso el tiempo de espera sin detectar al jugador?
            ia.CambiarEstado(ia.pacifico);                          //Se cambia el comportamiento del enemigo al de pacifico

        if(Vector3.Distance(player.transform.position, transform.position) < 4)    //El enemigo se encuentra cerca del jugador?
            ia.agent.SetDestination(player.transform.position);                     //En ese caso la IA se acerca a donde esta el jugador para tener mejor deteccion
    }
}
