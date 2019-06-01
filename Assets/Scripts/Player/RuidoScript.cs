using UnityEngine;

public class RuidoScript : MonoBehaviour
{
    public float ruido;
    public AudioClip[] pasosClip;
    public Renderer ring;

    private AudioSource source;
    private float pasos;
    private float loudness;
    private Color aim = new Color (0.75f, 0, 0);
    private Color move = new Color (0.2f, 0.2f, 1);
    private Color adjust = new Color(0, 0.5f, 0);

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        loudness = GameManager.instance.mic.loudness;

        if (pasos > 5)
            pasos -= pasos / 15;
        else if (pasos > 0)
            pasos -= 0.15f;

        ruido = Mathf.Clamp(loudness + pasos, 0, 50);
        GameManager.instance.ruidoGenerado = ruido;
        ring.material.SetFloat("_Amplitude", Mathf.Clamp(ruido / 25, 0, 0.8f));

        if (GameManager.instance.input.touching)
            ring.material.SetColor("_Color", aim);
        else
            ring.material.SetColor("_Color", move);

        if (GameManager.instance.sensorReader.proxValue != 5)
            ring.material.SetColor("_Color", adjust);

        if (!GameManager.instance.input.playing)
            ring.material.SetColor("_Color", Color.black);
    }

    public void SonidoPaso()
    {
        pasos += Random.Range(3, 4);
        source.clip = pasosClip[Random.Range(0, pasosClip.Length)];
        source.Play();
    }
}