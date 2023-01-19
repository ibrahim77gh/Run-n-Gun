using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float health = 100f;
    public float nbDamage = 20f;
    public GameObject psDestroyPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Normal Bullet")
        {
            health -= nbDamage;
            if (health == 0)
            {
                Instantiate(psDestroyPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        OnPlayUIManager.killCount += 1;
    }
}
