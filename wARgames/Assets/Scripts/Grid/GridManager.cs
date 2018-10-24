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

    public bool ValidationLocationRotation(Vector2Int origin, GamePiece piece)
    {
        bool isValid = true;

        // Looking over all of the possible Orientations and performing checks to see if they are valid.
        switch(piece.m_Orientation)
        {

            // Checking to see if the piece will hang over the bottom edge of the board, and checking to
            // see if it will overlap any other pieces.
            case GamePiece.Orientation.North:

                // Checking to see if a piece will over hang.
                if (origin.y + piece.PieceLength > GridCells.GetLength(0))
                {
                    Debug.Log("Piece too long to fit North");

                    return false;
                }

                // Checking to see if each cell under the piece has an already existing piece there.
                for(int index = 0; index < piece.PieceLength; index++)
                {
                    if (GridCells[origin.x, origin.y + index].ContainsGamePiece)
                    {
                        Debug.Log("Piece overlaps anorther");

                        return false;
                    }
                }

                return true;

            // Checking to see if the piece will hang over the left edge of the board, and checking to
            // see if it will overlap any other pieces.
            case GamePiece.Orientation.East:

                // Checking to see if a piece will over hang.
                if (origin.x - piece.PieceLength + 1 < 0)
                {
                    Debug.Log("Piece too long to fit East");


                    return false;
                }

                // Checking to see if each cell under the piece has an already existing piece there.
                for (int index = 0; index < piece.PieceLength; index++)
                {
                    if (GridCells[origin.x - index, origin.y].ContainsGamePiece)
                    {
                        Debug.Log("Piece overlaps anorther");

                        return false;
                    }
                }

                return true;

            // Checking to see if the piece will hang over the top edge of the board, and checking to
            // see if it will overlap any other pieces.
            case GamePiece.Orientation.South:

                // Checking to see if a piece will over hang.
                if (origin.y - piece.PieceLength + 1 < 0)
                {
                    Debug.Log("Piece too long to fit South");


                    return false;
                }

                // Checking to see if each cell under the piece has an already existing piece there.
                for (int index = 0; index < piece.PieceLength; index++)
                {
                    if (GridCells[origin.x, origin.y - index].ContainsGamePiece)
                    {
                        Debug.Log("Piece overlaps anorther");

                        return false;
                    }
                }

                return true;

            // Checking to see if the piece will hang over the left edge of the board, and checking to
            // see if it will overlap any other pieces.
            case GamePiece.Orientation.West:

                // Checking to see if a piece will over hang.
                if (origin.x + piece.PieceLength > GridCells.GetLength(1))
                {
                    Debug.Log("Piece too long to fit West");

                    return false;
                }

                // Checking to see if each cell under the piece has an already existing piece there.
                for (int index = 0; index < piece.PieceLength; index++)
                {
                    if (GridCells[origin.x + index, origin.y].ContainsGamePiece)
                    {
                        Debug.Log("Piece overlaps anorther");

                        return false;
                    }
                }

                return true;
        }

        return isValid;
    }

    public GridCell GetCell(Vector2Int position)
    {
        return GridCells[position.x, position.y];
    }
}
