﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private ParticleSystem destructionBlastPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if (destructionBlastPrefab != null)
            {
                ParticleSystem myParticleSystem;
                myParticleSystem = Instantiate(destructionBlastPrefab, transform.position, destructionBlastPrefab.transform.rotation);
                Destroy(myParticleSystem.gameObject, myParticleSystem.main.duration);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
