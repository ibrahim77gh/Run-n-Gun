using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Normal_AI : MonoBehaviour
{
    public Transform target;
    public float speed = 5;
    private Vector3[] path;
    private int targetIndex;
    void Update()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            // Debug.Log(currentWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
