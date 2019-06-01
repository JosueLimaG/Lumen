using UnityEngine;

public class EmulatedInputScript : InputScript      //Modulo de lectura de entrada usado como variante de movimiento sin usar el giroscopio
{
    private Vector2 touchPos;                       //Usado para guardar la posicion del toque de pantalla
    private Vector2 inputTouch;                     //Usado para leer la posicion actual del toque

    public override Vector3 GetInputs()
    {
        if (Input.touches.Length > 0)                                   //Pregunta si se esta tocando la pantalla
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchPos = Input.touches[0].position;                   //Cuando se detecta el toque se guarda la posicion del dedo
                inputTouch = new Vector3();                             //Se genera un valor vacio

                if (Input.GetTouch(0).tapCount == 2)                    //Cuando se realizan dos toques consecutivos de pantalla se simula un toque normal con el modulo original
                    touching = !touching;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)        //El dedo cambio su posicion?
                inputTouch = Input.touches[0].position - touchPos;      //A la posicion actual del dedo se le resta la posicion inicial guardada
            else if (Input.touches[0].phase == TouchPhase.Ended)        //Se termino de tocar la pantalla?
            {
                inputTouch = new Vector3();                             //Se genera un valor vacio
                touching = false;                                       //Se indica que no se esta tocando la pantalla
            }

            return inputTouch * sensibilidadTouch;                      //Se devuelve el valor generado multiplicado por la sensibilidad
        }
        else
            return new Vector3();                                       //Si no se toca la pantalla se devuelve un valor vacio
    }
}
