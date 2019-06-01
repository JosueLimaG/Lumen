using UnityEngine;

public class Vista : Sensores
{
    private float distanciaDeVision = 3;    //Distancia a la que la IA ve al jugador

    public override float GetValue()        //Se sobreescribe el metodo de la clase heredada
    {
        //Se calcula la direccion en la que se encuentra el jugador
        Vector3 direction = (new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position); 
        Ray ray = new Ray(transform.position, direction);       //Se produce un raycast en la direccion calculada
        RaycastHit hit;                                         //Donde se almacenara la informacion del raycast

        if (Physics.Raycast(ray, out hit, distanciaDeVision))   //El raycast golpeo algo?
        {
            //Se comprueba que el target este en angulo correcto referente a la vista de la IA
            if (Mathf.Abs(CalcularAngulo(transform.position, target.position) - transform.eulerAngles.y) < 45 && hit.transform == target)
                deteccion += Time.deltaTime;                    //En dicho caso, el nivel de alerta aumenta
            else
                deteccion -= Time.deltaTime;                    //Caso contrario el nivel de alerta se reduce                                  
        }
        else
            deteccion--;                                        //Si no se detecta nada con el raycast, el nivel de alerta reduce

        deteccion = Mathf.Clamp(deteccion, 0, 5);               //Se limita el valor del nivel de alerta para evitar lecturas fuera del limite

        return Gamma(1, 5, deteccion);                          //Se devuelve el valor en una escala sobre 1, siendo un segundo tolerable, 5 no
    }
}
