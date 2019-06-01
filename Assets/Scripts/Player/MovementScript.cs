using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Vector3 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        input = GameManager.instance.input.inputMovement;       //Se usa como input la informacion proporcionada por el MovementScript
        rb.velocity = new Vector3(0, 0, 0);                     //Se le indica al personaje que la velocidad es 0
        animator.SetBool("Walking", false);                     //Se le indica al animator que no se esta caminando
        animator.speed = 1;                                     //La velocidad de la animacion es normal

        if (!GameManager.instance.input.touching)               //Cuando no se presiona la pantalla el personaje debe moverse
        {
            input.x = Mathf.Clamp(input.x, -5, 5);              //Se limita el input en sus 3 ejes
            input.y = Mathf.Clamp(input.y, -5, 5);
            input.z = Mathf.Clamp(input.z, -5, 5);

            float xTemp = input.x;                              //Se copia una variable temporal con los valores x y del input
            float yTemp = input.y;

            if (Mathf.Abs(input.x) * 100 > 0 || Mathf.Abs(input.y) * 100 > 0)                                           //Se realizan acciones solo si el input 
            {                                                                                                           //  registra movimiento
                transform.LookAt(new Vector3(input.x * 100, transform.position.y, input.y * 100) + transform.position); //El personaje debe mirar hacia la direccion
                                                                                                                        //  a la que se esta moviendo
                if (Mathf.Abs(Mathf.RoundToInt(input.x)) > 1 || Mathf.Abs(Mathf.RoundToInt(input.y)) > 1)               //Si el input recide una entrada con un
                {                                                                                                       //  valor relevante se ejecuta el movimiento
                    rb.velocity = new Vector3(xTemp / 2, 0, yTemp / 2);                                                 //Se aplica el calculo final como velocidad al personaje
                    animator.SetBool("Walking", true);                                                                  //Se le indica al animator que se esta caminando
                    float speed = (Mathf.Abs(Mathf.RoundToInt(input.x)) + Mathf.Abs(Mathf.RoundToInt(input.y))) / 2;    //Se calcula la velocidad promedio en ambos ejes
                    if (speed > 1)                                                                                      //El calculo es mayor a 1?
                        animator.speed = speed;                                                                         //La velocidad de la animacion se acelera
                }
            }
        }
        else if (input.x != 0 || input.y != 0)                                                                          //Se registro un valor en el input?
            transform.LookAt(new Vector3(input.x, transform.position.y, input.y) + transform.position);                 //El personaje mira en esa direccion
    }
}
