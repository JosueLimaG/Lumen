using UnityEngine;

public class _agresivo: Estado
{
    public float tiempoEnEspera = 10;   //Cuanto tiempo pasara para que, al perder contacto con el jugador, el enemigo vuelva a un estado alerta
    private float timer;                //El timepo transcurrido a comparar con el tiempoEnEspera
    private bool atacar;                //El enemigo esta realizando un ataque?
    private bool jugadorALaVista;       //El jugador sigue en la vista del enemigo?

	void OnEnable()
	{
        ia.multiplicador = 2;           //El enemigo es mas sensible a sus sensores
        ia.agent.speed = 2;             //La velocidad de movimiento en estado agresivo es mayor que en las demas
		alerta.enabled = false;         //Se apaga el comportamiendo alerta del enemigo
        pacifico.enabled = false;       //Se apaga el comportamiento pacifico del enemigo
	}

	void Update ()
	{
        /*Vector3 direction = (new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position); //Se calcula la direccion en la que se encuentra el jugador
        Ray ray = new Ray(transform.position, direction);                                                                                       //Se realiza un raycast en la direccion calculada
        RaycastHit hit;                                                                                                                         //Se almacena la informacion del raycast

        if (Physics.Raycast(ray, out hit))                                                                                                      //El raycast golpeo algo?
        {
            //El jugador esta a la vista del enemigo?
            jugadorALaVista = hit.transform == player && Mathf.Abs(CalcularAngulo(transform.position, player.position) - transform.eulerAngles.y) < 60 ? true : false;
            timer += jugadorALaVista ? -timer : Time.deltaTime;                     //Si el jugador no esta a la vista, se empieza a contar el tiempo transcurrido
            timer = Mathf.Clamp(timer, 0, tiempoEnEspera + 1);                      //Se limita el control del tiempo para evitar que se salga de los limites

            if (timer >= tiempoEnEspera)                                            //Paso el tiempo de espera sin detectar al jugador?
                ia.CambiarEstado(ia.alerta);                                        //Se cambia el comportamiento del enemigo al de alerta
        }*/

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia <= 1.5f)// && jugadorALaVista)   //El jugador esta cerca del enemigo y esta a la vista?
        {
            atacar = true;                  //Se indica al personaje que debe atacar
            transform.LookAt(player);       //El enemigo debe mirar al personaje mientras ataca
        }
        else if (distancia > 0.75f)   //El jugador esta a mas de 0.5 unidades de distancia?
        {
            ia.agent.SetDestination(player.position);                           //Se indica al enemigo que se acerque al jugadpr
            atacar = false;                                                     //No se esta a la distancia suficiente para atacar
        }

        if (distancia > 5)
        {
            ia.CambiarEstado(ia.alerta);
            atacar = false;
        }

        anim.SetBool("Walk", !atacar);      //Se le indica al animator si el enemigo esta caminando o no
        anim.SetBool("Attack", atacar);     //Se le indica al animator si el enemigo esta atacando o no
    }

    public void AttackAnim()                                //Metodo publico invocado al final de cada animacion de ataque
    {
        anim.SetInteger("AttackInt", Random.Range(0, 2));   //Devuelve una animacion aleatoria para el siguiente movimiento de ataque
    }
}
