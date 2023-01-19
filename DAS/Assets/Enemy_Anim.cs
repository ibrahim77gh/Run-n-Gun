using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Anim : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameObject player;
    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        EnemyRotation();
    }
    void EnemyRotation()
    {
        
        Vector3 worldDirectionToPointForward = rb.velocity.normalized;
        Vector3 localDirectionToPointForward = Vector3.right;

        Vector3 currentWorldForwardDirection = transform.TransformDirection(
                localDirectionToPointForward);
        float angleDiff = Vector3.SignedAngle(currentWorldForwardDirection,
                worldDirectionToPointForward, Vector3.forward);

        transform.Rotate(Vector3.forward, angleDiff, Space.World);
        
    }
    
}
