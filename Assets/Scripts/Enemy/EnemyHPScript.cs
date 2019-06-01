using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    private int hp = 150;   //La vida maxima del enemigo actual
    private Material mat;   //El material del enemigo actual

    private void Start()
    {
        mat = Instantiate(transform.GetComponentInChildren<Renderer>().material);   //Se instancia el material del objeto actual
        GetComponentInChildren<Renderer>().material = mat;                          //Se asigna al objeto el material instanciado
    }

    public void Damage(int dam)                                                     //Metodo publico usado al recibir dano
    {
        hp -= dam;                                                                  //Se resta a la vida actual el valor recibido
        mat.SetFloat("_SpecularIntensity", 1 - (hp / 300));                         //El color del material refleja la vida del enemigo

        if (hp <= 0)                                                                //La vida actual es igual o menor que 0?
            Destroy(this.gameObject);                                               //En ese caso el objeto es destruido
    }
}
