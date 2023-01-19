using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

// We have made the grid to work with custom type objects i.e Generic Grid
public class Grid_M<TGridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float gridCellSize;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Vector3 originPosition;

    public Grid_M(int width, int height, float cellSize, Vector3 originPos, Func<Grid_M<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.gridCellSize = cellSize;
        this.originPosition = originPos;

        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);

            }
        }

        bool showDebug = true;

        if (showDebug == true)
        {
            // Making Grid by iterating through each grid cell
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    // We have to deal with null error because when we create a grid of our custom object, the default value for a reference type object is null! (pre-defined objects in unity are value types)
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize * 0.5f, cellSize * 0.5f), 20, Color.blue, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.blue, 200f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.blue, 200f);

                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.blue, 200f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.blue, 200f);

            // event fired if grid cell value changes
            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }

    }
    // Returns the world position of the Grid cell given it`s x and y value
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * gridCellSize + originPosition;
    }

    // Returns the integer position (x,y) of the Grid cell given it`s world position
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / gridCellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / gridCellSize);
    }

    // Set value based on integer
    public void SetGridObject(int x, int y, TGridObject value)
    {
        // Only set value if it is in the Grid otherwise ignore
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    // Get world position of enemy or Mouse, convert that into integer using GetXY and then use the SetValue defined above...
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}