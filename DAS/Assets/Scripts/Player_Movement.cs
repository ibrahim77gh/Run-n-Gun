using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float Nspeed;
    private float speed;
    public float superSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 mousepos;

    public float superSpeedTime = 0.5f;
    private float superTime;

    public float waitTime = 2f;
    private float wTime = 0f;
    private int n = 0;


    // Update is called once per frame

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (wTime <= 0f)
        {
            if (Input.GetButtonDown("Super Speed"))
            {
                superTime = superSpeedTime;
                n += 1;
                if (n >= 2)
                {
                    wTime = waitTime;
                    n = 0;
                }
            }
        }
        else
        {
            wTime -= Time.deltaTime;
        }
   
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
    }
    void FixedUpdate()
    {
        if (superTime > 0f)
        {
            speed = superSpeed;
            PlayerMovement();
            superTime -= Time.fixedDeltaTime;
        }
        else
        {
            speed = Nspeed;
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        Vector2 mouse_dir = mousepos - rb.position;
        float angle = Mathf.Atan2(mouse_dir.y, mouse_dir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }


}
