using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathfindingM
{
    private const int MOVE_STRAIGHT_COST = 10; // 1 * 10 (Scaled by 10 so we don`t deal with floats)
    private const int MOVE_DIAGONAL_COST = 14; // 1.4 * 10

    private Grid_M<PathNodeM> grid;
    private List<PathNodeM> openList; // Nodes queued up for searching
    private List<PathNodeM> closedList; // Nodes that have already been searched
    private List<Vector3> wayPoints;

    private LayerMask unwalkableMask;
    public bool pathSuccess = false;

    public PathfindingM(int width, int height, float cellSize, Vector3 originPos, LayerMask unwalkableMask)
    {
        grid = new Grid_M<PathNodeM>(width, height, cellSize, originPos, (Grid_M<PathNodeM> g, int x, int y) => new PathNodeM(g, x, y));
        this.unwalkableMask = unwalkableMask;
    }

    public Grid_M<PathNodeM> GetGrid()
    {
        return grid;
    }

    public Vector3[] FindPath(int startX, int startY, int endX, int endY)
    {
        PathNodeM startNode = grid.GetGridObject(startX, startY);
        PathNodeM endNode = grid.GetGridObject(endX, endY);
        //Debug.Log(startNode);
        //Debug.Log(endNode);

        openList = new List<PathNodeM> { startNode }; // initially openList will only contain start node
        closedList = new List<PathNodeM>();

        // initializing the grid
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNodeM pathNode = grid.GetGridObject(x, y); // each grid cell will have Node
                pathNode.gCost = int.MaxValue; // their gCost initially will be set to max
                pathNode.CalculateFCost(); // fCost = gCost + hCost so fCost will also be max
                pathNode.cameFromNode = null; // Initially no data from previous node
                pathNode.CheckWalkable(unwalkableMask);
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNodeM currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                // Reached final node
                pathSuccess = true;
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNodeM neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                // Check IsWalkable?
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    Debug.Log(currentNode);

                    if (!openList.Contains(neighbourNode)) { openList.Add(neighbourNode); }
                }
            }
        }

        // Out of nodes in the openList
        pathSuccess = false;
        Debug.Log(pathSuccess);
        return null; // We could not find a path
    }    

    private List<PathNodeM> GetNeighbourList(PathNodeM currentNode)
    {
        List<PathNodeM> neighbourList = new List<PathNodeM>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) { neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1)); }
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) { neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1)); }
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) { neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1)); }
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) { neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1)); }
        }
        // Down 
        if (currentNode.y - 1 >= 0) { neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1)); }
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) { neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1)); }

        return neighbourList;
    }

    public PathNodeM GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private Vector3[] CalculatePath(PathNodeM endNode)
    {
        List<PathNodeM> path = new List<PathNodeM>();
        path.Add(endNode);
        PathNodeM currentNode = endNode;
        while (currentNode.cameFromNode != null) // keep cycling from parent to parent untill we reach a node with no parent which is essentially the start node
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        Vector3[] wayPoints = SimplifyPath(path);
        // I want it to return location of the nodes rather than the actual nodes
        Array.Reverse(wayPoints);
        Debug.Log(wayPoints);
        return wayPoints;
    }

    Vector3[] SimplifyPath(List<PathNodeM> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].x - path[i].x, path[i - 1].y - path[i].y);
            if (directionNew != directionOld)
            {
                waypoints.Add(grid.GetWorldPosition(path[i].x, path[i].y));
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
}

    private int CalculateDistanceCost(PathNodeM a, PathNodeM b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.x - b.x);
        int remaining = Mathf.Abs(xDistance - yDistance); // This is the distance that cannot be moved diagonally because x dist and y dist don`t match
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    
    // Returns node with the lowest FCost 
    private PathNodeM GetLowestFCostNode(List<PathNodeM> pathNodeList)
    {
        PathNodeM lowestFCostNode = pathNodeList[0];

        // Cycle through all the nodes in the open list...
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
