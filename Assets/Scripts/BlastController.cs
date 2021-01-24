using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    private bool isPaused;
    private ParticleSystem[] myParticleSystems;

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

        Destroy(gameObject);
    }

    private bool CheckIfParticleSystemFinished()
    {
        foreach (ParticleSystem particleSystem in myParticleSystems)
            if (particleSystem.IsAlive()) return false;

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;        
        myParticleSystems = GetComponentsInChildren<ParticleSystem>();

        StartCoroutine("RunParticleLifeCheck");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
