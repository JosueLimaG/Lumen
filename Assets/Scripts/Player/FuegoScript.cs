using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class FuegoScript : MonoBehaviour
{
    public Material mat;
    public float emm = 50;
    public bool permanente;
    public AudioMixerSnapshot high;
    public AudioMixerSnapshot mid;
    public AudioMixerSnapshot low;
    public AudioMixerSnapshot none;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem.EmissionModule emission;
    private float gas = 1000;
    private bool loading = false;

    void Start()
    {
        emission = GetComponent<ParticleSystem>().emission;
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        if (gas > 0 && !loading)
        {
            if (GameManager.instance.input.touching && GameManager.instance.mic.loudness > 1)
            {
                emission.rateOverTime = GameManager.instance.mic.loudness * 2;
                gas -= GameManager.instance.mic.loudness / 5;

                if (GameManager.instance.mic.loudness > 20)
                    high.TransitionTo(1.0f);
                else if (GameManager.instance.mic.loudness > 10)
                    mid.TransitionTo(1.0f);
                else if (GameManager.instance.mic.loudness > 0)
                    low.TransitionTo(0.25f);
            }
            else
            {
                emission.rateOverTime = 0;
                gas += 5;
                none.TransitionTo(1.0f);
            }

            if (permanente)
            {
                emission.rateOverTime = emm;
                gas -= emm / 10;

                if (emm > 40 && !loading)
                    high.TransitionTo(0.1f);
                else if (emm > 20 && !loading)
                    mid.TransitionTo(0.1f);
                else if (emm > 0 && !loading)
                    low.TransitionTo(0.1f);
                else
                    none.TransitionTo(0.1f);
            }

            if (gas <= 0)
                loading = true;
        }
        else
        {
            emission.rateOverTime = 0;
            none.TransitionTo(1.0f);
            gas += 5;

            if (gas >= 1000)
                loading = false;
        }
        gas = Mathf.Clamp(gas, 0, 1001);

        if (loading)
        {
            mat.color = Color.Lerp(new Color(1, 0.5f, 0, 1), Color.red, Mathf.PingPong(Time.time, 2f));
        }
        else
            mat.color = Color.Lerp(Color.red, new Color(0.2f, 0.2f, 0.2f, 1), gas / 1000);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHPScript>().Damage(part.GetCollisionEvents(other, collisionEvents));
        }
    }
}
