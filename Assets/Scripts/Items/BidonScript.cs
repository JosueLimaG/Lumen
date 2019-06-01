using UnityEngine;

public class BidonScript : MonoBehaviour
{
    private bool lleno = true;
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = new Color(1, 0.5f, 0, 1);
    }

    public bool Llenar()
    {
        if (lleno)
            return false;
        else
        {
            lleno = true;
            mat.color = new Color(1, 0.5f, 0, 1);
            return true;
        }
    }

    public bool Vaciar()
    {
        if (lleno)
        {
            lleno = false;
            mat.color = new Color(0.25f, 0.125f, 0, 1);
            return true;
        }
        else
            return false;
    }
}
