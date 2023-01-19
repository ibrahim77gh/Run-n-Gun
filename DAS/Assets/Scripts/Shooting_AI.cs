using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shooting_AI : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform pTarget;
    private Rigidbody2D Erb;
    public Transform EfireP;
    public float bulletSpeed;
    public float timeStartShoot = 2f;
    public float timeBwShoot = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Erb = GetComponent<Rigidbody2D>();
        AIDestinationSetter targett = GetComponent<AIDestinationSetter>();
        targett.target = GameObject.FindGameObjectWithTag("Player").transform;
        pTarget = targett.target;
    }


    private void FixedUpdate()
    {
        if (pTarget != null)
        {
            Vector2 pDir = transform.position - pTarget.position;
            float angle = Mathf.Atan2(pDir.y, pDir.x) * Mathf.Rad2Deg + 90f;
            Erb.rotation = angle;
            if (timeStartShoot <= 0f)
            {
                Shoot();
                timeStartShoot = timeBwShoot;
            }
            else
            {
                timeStartShoot -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, EfireP.position, EfireP.rotation);
        Rigidbody2D rb_Ebullet = bullet.GetComponent<Rigidbody2D>();
        rb_Ebullet.AddForce(EfireP.up * bulletSpeed, ForceMode2D.Impulse);
    }

}
