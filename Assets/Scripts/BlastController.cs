using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    private bool isPaused;
    private ParticleSystem[] myParticleSystems;
    private AudioSource myAudioSource;

    private void Pause()
    {
        if (isPaused)
        {
            foreach (ParticleSystem particleSystem in myParticleSystems)
                particleSystem.Play();
        }
        else
        {
            foreach (ParticleSystem particleSystem in myParticleSystems)
                particleSystem.Pause();
        }
        isPaused = !isPaused;
    }

    private IEnumerator RunParticleLifeCheck()
    {
        while (!CheckIfParticleSystemFinished())
            yield return new WaitForSeconds(2);

        StopAllCoroutines();
        Destroy(gameObject);
    }

    private bool CheckIfParticleSystemFinished()
    {
        foreach (ParticleSystem particleSystem in myParticleSystems)
            if (particleSystem != null && particleSystem.IsAlive()) return false;

        return true;
    }

    public void PlaySound(AudioClip myClip)
    {
        myAudioSource.PlayOneShot(myClip);
    }

    private void Awake()
    {
        myAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;        
        myParticleSystems = GetComponentsInChildren<ParticleSystem>();

        if (myParticleSystems.Length != 0)
            StartCoroutine("RunParticleLifeCheck");
        else
            Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
