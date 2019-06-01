using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject boton;
    public Vector3 posicionEnInventario;
    public Vector3 rotacionEnInventario;
    public int comentario;
    public float tiempoDeComentario = 5;
    private Transform player;

    private void Start()
    {
        if (boton != null)
            boton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MostrarBoton(true);
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            MostrarBoton(false);
    }

    public void SaveItem()
    {
        player.GetComponent<InventarioScript>().TomarItem(transform, posicionEnInventario, rotacionEnInventario);
        GetComponent<UIScript>().Window(false);
        player.GetComponent<UIPlayerScript>().CreateText(comentario, tiempoDeComentario);
    }

    public void MostrarBoton(bool x)
    {
        boton.SetActive(x);
    }

    public void MostrarComentario()
    {
        player.GetComponent<UIPlayerScript>().CreateText(comentario, tiempoDeComentario);
    }
}
