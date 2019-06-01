using UnityEngine;

public class Oido : Sensores
{
    private float rangoEscucha = 10f;

    public override float GetValue()                                            //Se sobreescribe el metodo de la clase heredada
    {
        float ruido = Gamma(3, 40, GameManager.instance.ruidoGenerado);
        float distancia = Vector3.Distance(transform.position, target.position);
        float escuchado = 1 - (ruido * Gamma(0, rangoEscucha, distancia));
        return ruido * escuchado;
    }
}