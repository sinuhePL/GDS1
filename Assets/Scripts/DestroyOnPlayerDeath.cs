using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerDeath : MonoBehaviour
{
    protected void DestroyMe()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.OnVehicleDestroyed += DestroyMe;
    }

    private void OnDestroy()
    {
        EventsManager.instance.OnVehicleDestroyed -= DestroyMe;
    }

}
