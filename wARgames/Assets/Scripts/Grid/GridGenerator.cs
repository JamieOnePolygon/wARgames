using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int GridSize = 10;
    public GameObject CellPrefab;
    public Transform Parent;

    public float HeightOffset = 5f;

	// Use this for initialization
	void Start ()
    {
        GenerateGrid();	
	}
    
    public void GenerateGrid()
    {
        GridCell[,] theGrid = new GridCell[GridSize, GridSize];

        // Generating the Grid
        for(int yIndex = 0; yIndex < GridSize; yIndex++)
        {
            for (int xIndex = 0; xIndex < GridSize; xIndex++)
            {
                // Spawning the GameObject
                GameObject go = Instantiate(CellPrefab);

                // Creating a position to place the Cell at.
                Vector3 position = new Vector3();
                position.x = xIndex - (GridSize / 2f);
                position.y = HeightOffset;
                position.z = (GridSize / 2f) - yIndex;

                //Setting the position of the cell and setting it's parent to
                // keep the heirarchy clean.
                go.transform.position = position;
                go.transform.parent = Parent;

                // Setting it's name for sanity's sake.
                GridCell cell = go.GetComponent<GridCell>();
                cell.Initialize(new Vector2Int(xIndex, yIndex), IntToString(xIndex) + ", " + yIndex);

                theGrid[xIndex, yIndex] = cell;
            }
        }

        GetComponent<GridManager>().InitializeGrid(theGrid);

        Destroy(this);
    }

    /// <summary>
    /// Converts an Interger into a Capitalised Char.
    /// </summary>
    /// <param name="index">Index of the Char.</param>
    /// <returns></returns>
    private string IntToString(int index)
    {
        char c = (char)(65 + index);
        return c.ToString();
    }

}
