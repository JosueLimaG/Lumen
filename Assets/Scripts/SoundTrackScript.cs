using UnityEngine;

public class SoundTrackScript : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource source;
    private int clip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!source.isPlaying)
        {
            clip++;

            if (clip > tracks.Length)
                clip = 0;

            source.clip = tracks[clip];
            source.Play();
        }
    }
}
