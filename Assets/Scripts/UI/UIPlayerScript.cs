using System.Collections;
using UnityEngine;

public class UIPlayerScript : MonoBehaviour
{
    public Canvas canvas;
    public GameObject TextPrefab;

    void Start()
    {
        canvas.worldCamera = Camera.main;
        Invoke("InitialText", 0.5f);
    }

    private void InitialText()
    {
        CreateText(0, 8, 3);
    }

    public void CreateText(int texto, float vida)
    {
        GameObject temp = Instantiate(TextPrefab, canvas.transform);
        temp.GetComponent<TextScript>().Set(texto);
        temp.GetComponent<FloatingButtonScript>().SetSettings(gameObject, canvas, vida);
    }

    IEnumerator Create(int texto, float vida, float retraso)
    {
        yield return new WaitForSeconds(retraso);
        CreateText(texto, vida);
    }

    public void CreateText(int texto, float vida, float retraso)
    {
        StartCoroutine(Create(texto, vida, retraso));
    }
}
