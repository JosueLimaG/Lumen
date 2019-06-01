using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScript : MonoBehaviour
{
    Material mat;
    public string image;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnBecameVisible()
    {
        mat.mainTexture = GameManager.instance.langReader.GetImage(image);
    }
}
