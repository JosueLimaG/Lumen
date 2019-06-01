using UnityEngine;

public class TanqueGasolina : MonoBehaviour //Puzzle de gasolina usado en el primer nivel, este scipt lo usa el tanque del auto y el generador de gasolina
{
    public GameObject boton;                //El boton para acceder al UI del bidon
    public bool receptor;                   //Se marca si el objeto es el generador a llenar de gasolina
    public GameObject puerta;               //La puerta a abrir al llenarse el generador
    public int nivelDeGasolina;             //La gasolina disponible dentro del objeto
    public BidonScript bidon;               //Acceso al script del bidon

    private UIPlayerScript uiPlayer;        //Acceso al UI del jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")                                                  //El collider detecto al jugador?
        {
            InventarioScript inventario = other.GetComponent<InventarioScript>();   //Se accede al inventario del jugador
            uiPlayer = other.GetComponent<UIPlayerScript>();                        //Se obtiene el UI

            if (inventario.currentItem != null)                                     //El jugador tiene un objeto en su inventario?
            {
                if (inventario.currentItem.CompareTag("Bidon"))                     //Dicho objeto es el bidon?
                {
                    boton.SetActive(true);                                          //Se muestra el boton de interaccion
                    bidon = inventario.currentItem.GetComponent<BidonScript>();     //Se obtiene el script del bidon
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            boton.SetActive(false);             //Si el jugador se aleja del objeto, se oculta el boton
    }

    public void LlenarBidon()                   //Metodo publico usado para llenar el bidon de gasolina
    {
        if (nivelDeGasolina > 0)                //Se tiene aun gasolina en el objeto?
        {
            if (bidon.Llenar())                 //Se pudo llenar el bidon?
            {
                nivelDeGasolina--;              //Baja la cantidad de gasolina almacenada
                uiPlayer.CreateText(1, 5);      //Se crea un texto de interaccion
            }
            else
                uiPlayer.CreateText(2, 5);      //Se crea un texto de interaccion indicando que no se pudo llenar el bidon
        }
        else
            uiPlayer.CreateText(3, 5);          //Se crea un texto indicando que no queda mas gasolina
    }

    public void VaciarBidon()                   //Metodo public usado para vaciar la gasolina del bidon
    {
        if (bidon.Vaciar())                     //Se pudo vaciar la gasolina del bidon?
        {
            nivelDeGasolina++;                  //El nivel de gasolina del objeto aumenta

            if (nivelDeGasolina == 1)           //El nivel de gasolina del objeto es minima?
                uiPlayer.CreateText(4, 6);      //Se crea un texto indicando que se debe buscar mas gasolina
            else
                uiPlayer.CreateText(5, 5);      //Se crea un texto indicando que el tanque esta lleno

        }
        else                                    //No se pudo vacial el bidon
        {
            if (nivelDeGasolina == 2)           //El tanque esta lleno?
                uiPlayer.CreateText(6, 5);      //Se indica por medio de un texto que el objeto esta lleno
            else
                uiPlayer.CreateText(7, 6);      //Se indica por medio de un texto que el bidon esta vacio
        }

        if (receptor)                           //El objeto es el generador?
            Check();                            //Se revisa si se lleno el tanque
    }

    void Check()
    {
        if (nivelDeGasolina >= 2)                                   //El tanque esta lleno?
            puerta.GetComponent<PuertaScript>().SetAngulo(90);      //Se le indica a la puerta que gire 90 grados
    }
}
