using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBombController : BombController
{
    [Header("Technical:")]
    [SerializeField]
    private GameObject holePrefab;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject myHole;

        if (collision.gameObject.tag == "Ground")
        {
            myHole = Instantiate(holePrefab, transform.position + new Vector3(0.0f, 0.15f, 0.0f), holePrefab.transform.rotation);
            myHole.transform.SetParent(collision.gameObject.transform);
        }
        base.OnTriggerEnter2D(collision);
    }

    public override void Start()
    {
        Vector3 maxX;
        isPaused = false;
        EventsManager.instance.OnPausePressed += Pause;
        maxX = new Vector3(-100.0f, 0.0f, 0.0f);
        for(int i=0; i< VehicleOptionsController.instance.dropZones.Length;i++)
        {
            if (VehicleOptionsController.instance.dropZones[i].transform.position.x > maxX.x) maxX = VehicleOptionsController.instance.dropZones[i].transform.position;
        }
        destination = new Vector3(maxX.x + 2.0f, maxX.y, maxX.z);
    }
}
