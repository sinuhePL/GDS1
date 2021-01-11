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
            myHole = Instantiate(holePrefab, transform.position + new Vector3(0.0f, -0.3f, 0.0f), holePrefab.transform.rotation);
            myHole.transform.SetParent(collision.gameObject.transform);
        }
        base.OnTriggerEnter2D(collision);
    }
}
