using UnityEngine;
using System.Collections;

public class Grid_A : MonoBehaviour
{

    public LayerMask unwalkableMask; 
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;   // 2D array of nodes

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        // How many cells(nodes) should we have inside the grid? we want this to be int because doesnt make sense to have half a node
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        // Creating all nodes
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                // Collision check for each node, is it walkable or not?
                bool walkable = !Physics2D.CircleCast(worldPoint, nodeRadius, Vector2.one, unwalkableMask);
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y - transform.position.y + gridWorldSize.y / 2) / gridWorldSize.y;

        int x = Mathf.FloorToInt(Mathf.Clamp((gridSizeX) * percentX, 0, gridSizeX - 1));
        int y = Mathf.RoundToInt((gridSizeY) * percentY) + 1;

        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1)); 

        // Creating cells (after grid has been created and filled with nodes)
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                if (n.walkable) { Gizmos.color = Color.white; }
                else { Gizmos.color = Color.red; }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}

