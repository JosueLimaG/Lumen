using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    private LevelAudioSettings lvl;
    public AudioMixerSnapshot _outside;
    public AudioMixerSnapshot _inside;
    public AudioMixerSnapshot _deepInside;

    void Start()
    {
        if (GameManager.instance.audioS != null)
            if (GameManager.instance.audioS != this)
                Destroy(GameManager.instance.audioS);

        GameManager.instance.audioS = this;
        Init();
    }

    void Init()
    {
        if (GameObject.FindWithTag("Nivel") == null)
            print("No se encontro una configuracion de sonido para el nivel actual");
        else
        {
            lvl = GameObject.FindWithTag("Nivel").GetComponent<LevelAudioSettings>();

            switch (lvl.ambient.ToString())
            {
                case "outside":
                    _outside.TransitionTo(0.1f);
                    break;
                case "inside":
                    _inside.TransitionTo(0.1f);
                    break;
                case "deepInside":
                    _deepInside.TransitionTo(0.1f);
                    break;
            }
        }
    }

    public void Transition(string sound)
    {
        switch (sound)
        {
            case "outside":
                _outside.TransitionTo(0.1f);
                break;
            case "inside":
                _inside.TransitionTo(0.1f);
                break;
            case "deepInside":
                _deepInside.TransitionTo(0.1f);
                break;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        Init();
    }
}
