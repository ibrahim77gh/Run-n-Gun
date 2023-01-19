using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform fireP;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InvokeRepeating("Shoot", 0f, 0.2f);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke();
        }
    }
  
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, fireP.position, fireP.rotation);
        Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();
        rb_bullet.AddForce(fireP.up * bulletSpeed, ForceMode2D.Impulse);
    }

}
