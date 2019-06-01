using System.Collections.Generic;
using UnityEngine;

public class InventarioScript : MonoBehaviour
{
    public List<int> cartas = new List<int>();      //Donde se guardaran las cartas obtenidas por el jugador
    public Transform inventario;                    //Hueso donde se guardan los items recolectables
    public Transform currentItem;                   //El item almacenado actualmente

    public void Carta(int id)       //Metodo public usado al leer una carta y almacenarla
    {
        cartas.Add(id);             //Se anade el ID de la carta a una lista
    }

    public void TomarItem(Transform newObject, Vector3 position, Vector3 rotation)
    {
        SoltarItem();                                               //Al tomar un nuevo item se suelta primero el actual

        newObject.SetParent(inventario);                            //Se pone el nuevo item bajo la jerarquia del jugador
        newObject.localPosition = position;                         //Se ajusta la posicion del item a donde corresponda
        newObject.localEulerAngles = rotation;                      //Se ajusta la rotacion del item
        newObject.GetComponent<UIScript>().enabled = false;         //Se deshabilita el componente UI del item
        currentItem = newObject;                                    //Se lo marca como el item actual
    }

    public void SoltarItem()
    {
        if (currentItem != null)                                    //Se tiene un objeto en el inventario?
        { 
            currentItem.GetComponent<UIScript>().enabled = true;    //Se habilita el componente UI del item
            currentItem.SetParent(null);                            //El item no forma parte de la jerarquia del personaje
            currentItem = null;                                     //El slot del inventario se lo marca como vacio
        }
    }
}
