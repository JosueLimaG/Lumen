using UnityEngine;
using UnityEngine.Android;

public class MicScript : MonoBehaviour
{
    public float noiseDataUpdateIn = 0.1f;
    public float sensibilidad;
    public int currentMic = 0;
    private AudioSource audioSRC;

    private int samples = 512;
    private float senstivity = 0.3f;
    private float initialLoudness;
    private float[] y;

    public float loudness;
    int tick;
    int Tick()
    {
        tick++;
        return tick;
    }
    void Start()
    {
        GameManager.instance.mic = this;
        sensibilidad = GameManager.instance.config.micSensibility;
        Initialize(currentMic);
    }

    void Awake()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);

        audioSRC = GetComponent<AudioSource>();
    }

    public void Initialize(int mic)
    {
        currentMic = mic;
#if !UNITY_EDITOR
        audioSRC.clip = Microphone.Start(Microphone.devices[currentMic], true, 1, AudioSettings.outputSampleRate);
#else
        audioSRC.clip = AudioClip.Create("ClipName", 128, 1, 44100 * 2, false);
        audioSRC.loop = true;
#endif
        audioSRC.Play();
    }

    void Update()
    {
        Listen();
    }

    void Listen()
    {
        y = new float[samples];

        if (audioSRC.isPlaying)
        { }

        else if (audioSRC.clip.loadState == AudioDataLoadState.Loaded)
        {
            audioSRC.loop = true;

            while (!(Microphone.GetPosition(null) > 0))
            { }

            if (audioSRC.clip.loadState == AudioDataLoadState.Loaded)
                audioSRC.Play();
        }
        else
        {
            audioSRC.clip = Microphone.Start(Microphone.devices[currentMic], true, 1, AudioSettings.outputSampleRate);

            audioSRC.loop = true;

            while (!(Microphone.GetPosition(null) > 0))
            { }

            if (audioSRC.clip.loadState == AudioDataLoadState.Loaded)
                audioSRC.Play();
        }

        if ((Time.time) < noiseDataUpdateIn)
        {
            initialLoudness = 0.0f;
            return;
        }

        audioSRC.GetSpectrumData(y, 1, FFTWindow.BlackmanHarris);

        for (int i = 0; i < y.Length; i++)
        {
            initialLoudness += Mathf.Abs(y[i]);
        }

        initialLoudness /= (samples / 2.0f);
        initialLoudness *= senstivity;
        loudness = Mathf.Clamp(initialLoudness * 10000 * sensibilidad, 0, 50);
    }
}