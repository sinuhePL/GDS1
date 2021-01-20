using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{

    private bool isPaused;
    private ParticleSystem myParticleSystem;

    private void Pause()
    {
        if (isPaused)
        {
            myParticleSystem.Play();
        }
        else
        {
            myParticleSystem.Pause();
        }
        isPaused = !isPaused;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
