using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    // Using a Vector2 to store the position so that we can breakdown
    // the axes easily.
    public Vector2Int GridPosition { get; private set; }
    
    // Stores whether the player has placed a piece within this grid cell.
    public bool ContainsGamePiece { get; private set; }

    public bool Targeted { get; private set; }

    /// <summary>
    /// Called from GridGenerator.cs. This is used to populate the grid cell with information.
    /// </summary>
    /// <param name="gridPosition">Position in the grid. This is not the world location of the gameobject.</param>
    /// <param name="name">The name is constructed from the Row and Column given to this cell. For example "C, 1".</param>
    public void Initialize(Vector2Int gridPosition, string name = "A, 0")
    {
        GridPosition = gridPosition;
        gameObject.name = name;

        ContainsGamePiece = false;
        Targeted = false;
    }

    public void GamePiecePlaced()
    {
        ContainsGamePiece = true;
    }

    public void OnUserTapped()
    {
        
    }
}
