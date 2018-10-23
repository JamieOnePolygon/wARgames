using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int PieceLength = 2;

    public enum Orientation { North, East, South, West};
    public Orientation m_Orientation = Orientation.North;

    private Vector2Int Origin;
    private int remainingHitPoints;

    GridManager GridManager;

	// Use this for initialization
	void Start ()
    {
        GridManager = FindObjectOfType<GridManager>();

        remainingHitPoints = PieceLength;	
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GridCell gridCell = GridManager.GetCell(new Vector2Int(9, 9));

            TemporarilyPlacePiece(gridCell);
        }

        if (Origin != null && Input.GetKeyDown(KeyCode.R))
        {
            FindNextAvailableRotation();
            transform.rotation = Quaternion.Euler(0f, 90f * (int)m_Orientation, 0f);
        }

        if(Origin != null && Input.GetKeyDown(KeyCode.Return))
        {
            GridManager.PlacePiece(Origin, this);
        }
    }

    private void FindNextAvailableRotation()
    {
        bool canRotate = false;

        while (canRotate == false)
        {
            m_Orientation++;

            canRotate = GridManager.CanRotate(Origin, m_Orientation, PieceLength);

            if ((int)m_Orientation >= 3)
            {
                m_Orientation = 0;
            }
        }
    }

    public void TakeDamage()
    {
        remainingHitPoints--;

        if (remainingHitPoints <= 0)
        {
            // Die, and do things that will make the game piece no longer receive damage.
        }
    }

    public void TemporarilyPlacePiece(GridCell originCell)
    {
        transform.position = originCell.transform.position;
        Origin = originCell.GridPosition;

        FindNextAvailableRotation();
        transform.rotation = Quaternion.Euler(0f, 90f * (int)m_Orientation, 0f);
    }
}
