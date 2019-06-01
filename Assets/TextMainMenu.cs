using UnityEngine;

public class TextMainMenu : MonoBehaviour
{
    public Material mat;
    public float value;

    void Update()
    {
        mat.SetFloat("_FaceDilate", value);
    }
}
