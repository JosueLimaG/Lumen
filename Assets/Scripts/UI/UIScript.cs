using UnityEngine;

public class UIScript : MonoBehaviour
{
    public bool ajustarCanvas = true;       //Si el canvas no tiene una camara asignada se debe marcar esta casilla
    public Canvas canvas;                   //Referencia al canvas actual
    public bool ocultarVentana = false;     //Si la ventana a abrirse se sobrepone a una ventana anterior, se marca esto para esconder la anterior
    public bool itemGuardable = false;      //Si el item puede ser almacenado en el inventario del jugador, se debe marcar esto
    public GameObject ventana;              //La ventana a esconderse, en caso de que ocultarVentana sea true
    public GameObject window;               //La ventana a abrirse

    void Start()
    {
        window.SetActive(false);            //En caso de estar visible, esconde la ventana al iniciar al juego

        if (ajustarCanvas)                  //Se debe ajustar el canvas?
            canvas.worldCamera = Camera.main;   //Se ajusta el canvas a la camara de la escena
    }

    public void Window(bool open)           //Metodo llamado desde el boton de un item para interactuar con el
    {
        window.SetActive(open);             //Se abre o se esconde la ventana del item actual
        GameManager.instance.Pausa(open);   //Se pausa o se reanuda el juego

        if (ocultarVentana)                 //Se debe ocultar la ventana anterior?
            ventana.SetActive(!open);       //Se oculta o se muestra la ventana anterior

        if(!open && !itemGuardable)         //???
        {
            try { GetComponent<ItemScript>().MostrarComentario(); }
            catch { }
        }
    }
    private void OnLevelWasLoaded(int level)//Metodo que se ejecuta al cargar un nivel para evitar errores
    {
        window.SetActive(false);            //Se oculta la ventana actual
        GameManager.instance.Pausa(false);  //Se despausa el juego

        if (ocultarVentana)
            ventana.SetActive(true);
    }
}
