using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Bull_Col : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    public GameObject hitEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
        else
        {
            Destroy(gameObject, 0.05f);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

    }
}
