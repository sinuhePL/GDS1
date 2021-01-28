using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteSkinManager : MonoBehaviour
{
    private SpriteSkin[] spriteSkins;

    private void Awake()
    {
        spriteSkins = transform.parent.GetComponentsInChildren<SpriteSkin>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            foreach (SpriteSkin spriteSkin in spriteSkins)
                spriteSkin.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            foreach (SpriteSkin spriteSkin in spriteSkins)
                spriteSkin.enabled = false;
        }
    }
}
