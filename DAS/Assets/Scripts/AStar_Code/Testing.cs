using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;
    public LayerMask unWalkableMask;

    private PathfindingM pathfinding;

    PathRequestManager requestManager;
 
    private void Awake()
    {
        pathfinding = new PathfindingM(width, height, cellSize, transform.position, unWalkableMask);
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        pathfinding.GetGrid().GetXY(startPos, out int a, out int b);
        pathfinding.GetGrid().GetXY(targetPos, out int c, out int d);
        // Debug.Log(a.ToString() + ' ' + b.ToString() + ' ' + c.ToString() + ' ' + d.ToString());
        StartCoroutine(FindPathEnum(a, b, c, d));
    }

    IEnumerator FindPathEnum(int a, int b, int c, int d)
    {
        yield return null;
        Vector3[] path = pathfinding.FindPath(a, b, c, d);
        requestManager.FinishedProcessingPath(path, pathfinding.pathSuccess);
    }

}
