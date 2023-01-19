using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Run : MonoBehaviour
{
    public float speed = 5f;
    public ParticleSystem particle;
    private void Start()
    {
        particle.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            Player_Movement pm = collision.gameObject.GetComponent<Player_Movement>();
            pm.Nspeed = 5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            Player_Movement pm = collision.gameObject.GetComponent<Player_Movement>();
            pm.Nspeed = 15f;
        }
    }
}
