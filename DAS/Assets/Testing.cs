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

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) {
    //        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
    //        pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
    //        List<PathNodeM> path = pathfinding.FindPath(0, 0, x, y);
    //        if (path != null)
    //        {
    //            for (int i = 0; i < path.Count - 1; i++){
    //                Vector3 initPos = pathfinding.GetGrid().GetWorldPosition(path[i].x, path[i].y);
    //                Vector3 finalPos = pathfinding.GetGrid().GetWorldPosition(path[i + 1].x, path[i + 1].y);
    //                Debug.DrawLine(new Vector3(initPos.x + cellSize/2, initPos.y + cellSize/2), new Vector3(finalPos.x + cellSize / 2, finalPos.y + cellSize / 2), Color.green, 1f);
    //            }
    //        }
    //    }
    //}
}
