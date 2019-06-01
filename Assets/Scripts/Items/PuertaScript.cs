using UnityEngine;

public class PuertaScript : MonoBehaviour
{
    float grados;

    void Start()
    {
        grados = transform.localEulerAngles.y;
    }

    void Update()
    {
        if(transform.localEulerAngles.y < grados - 1)
            transform.localEulerAngles += new Vector3(0, 1, 0);
        else if (transform.localEulerAngles.y > grados + 1)
            transform.localEulerAngles -= new Vector3(0, 1, 0);
    }

    public void SetAngulo(int angulo)
    {
        grados = angulo;
    }
}
