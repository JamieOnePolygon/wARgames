using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GridCell[,] GridCells;
    
    public void InitializeGrid(GridCell[,] cells)
    {
        GridCells = cells;
    }

    public bool QueryCell(Vector2Int pos)
    {
        GridCell theCell = GridCells[pos.x, pos.y];

        if (theCell.Targeted == true) return false;
        else return true;
    }

    public void PlacePiece(Vector2Int position, GamePiece gamePiece)
    {
        GridCell originCell = GridCells[position.x, position.y];

        gamePiece.gameObject.transform.position = originCell.transform.position;

        switch (gamePiece.m_Orientation)
        {
            case GamePiece.Orientation.North:

                gamePiece.gameObject.transform.position = originCell.transform.position;

                PlaceNorth(position, gamePiece.PieceLength);
                break;

            case GamePiece.Orientation.East:

                gamePiece.gameObject.transform.position = originCell.transform.position;

                PlaceEast(position, gamePiece.PieceLength);
                break;

            case GamePiece.Orientation.South:

                gamePiece.gameObject.transform.position = originCell.transform.position;

                PlaceSouth(position, gamePiece.PieceLength);
                break;

            case GamePiece.Orientation.West:

                gamePiece.gameObject.transform.position = originCell.transform.position;

                PlaceWest(position, gamePiece.PieceLength);
                break;
        }
    }

    #region
    private void PlaceNorth(Vector2Int origin, int length)
    { 
        for(int index = 0; index < length; index++)
        {
            GridCell cell = GridCells[origin.x, origin.y + index];

            cell.GamePiecePlaced();
        }
    }

    private void PlaceSouth(Vector2Int origin, int length)
    {
        for (int index = 0; index < length; index++)
        {
            GridCell cell = GridCells[origin.x, origin.y - index];

            cell.GamePiecePlaced();
        }
    }

    private void PlaceEast(Vector2Int origin, int length)
    {
        for (int index = 0; index < length; index++)
        {
            GridCell cell = GridCells[origin.x - index, origin.y];

            cell.GamePiecePlaced();
        }
    }

    private void PlaceWest(Vector2Int origin, int length)
    {
        for (int index = 0; index < length; index++)
        {
            GridCell cell = GridCells[origin.x + index, origin.y];

            cell.GamePiecePlaced();
        }
    }
    #endregion

    /// <summary>
    /// Checks to see if a GamePiece rotated to a new orientation will fit on the game grid.
    /// </summary>
    /// <param name="origin"> Origin of the GamePiece located on the Grid. </param>
    /// <param name="newOrientation"> Orientation which we want to test. </param>
    /// <param name="size"> Length of the GamePiece trying to fit on the Grid. </param>
    /// <returns> Returns a boolean on whether the GamePiece in the new orientation will fit on the board. </returns>
    public bool CanRotate(Vector2Int origin, GamePiece.Orientation newOrientation, int size)
    {
        bool canRotate = true;

        switch(newOrientation)
        {
            case GamePiece.Orientation.North:

                if (origin.y + size >= GridCells.GetLength(1) - 1)
                {
                    Debug.Log("North NOT Valid");
                    canRotate = false;
                }
                else
                {
                    Debug.Log("North IS valid");
                    canRotate = true;
                }

                break;

            case GamePiece.Orientation.East:

                if(origin.x - size < 0)
                {
                    canRotate = false;
                }
                else
                {
                    canRotate = true;
                }

                break;

            case GamePiece.Orientation.South:

                if (origin.y - size < 0)
                {
                    canRotate = false;
                }
                else
                {
                    canRotate = true;
                }

                break;

            case GamePiece.Orientation.West:

                if (origin.x + size >= GridCells.GetLength(0) - 1)
                {
                    canRotate = false;
                }
                else
                {
                    canRotate = true;
                }

                break;
        }

        return canRotate;
    }

    public GridCell GetCell(Vector2Int position)
    {
        return GridCells[position.x, position.y];
    }
}
