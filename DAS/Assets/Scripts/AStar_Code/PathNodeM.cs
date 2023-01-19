using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeM
{
    private Grid_M<PathNodeM> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    private RaycastHit2D[] result;
    public PathNodeM cameFromNode;
    public PathNodeM(Grid_M<PathNodeM> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }


    public void CheckWalkable(LayerMask unWalkableMask)
    {
        Vector3 nodePosition = grid.GetWorldPosition(x, y);
        isWalkable = !Physics2D.CircleCast(new Vector2(nodePosition.x + 1.5f, nodePosition.y + 1.5f), 1.5f, Vector2.one, unWalkableMask);

    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
