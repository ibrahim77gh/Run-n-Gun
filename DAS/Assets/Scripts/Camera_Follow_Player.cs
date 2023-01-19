using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player : MonoBehaviour
{
    public Camera cam;
    public float yOffset = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(0, transform.position.y + yOffset, -10); 
    }
}
