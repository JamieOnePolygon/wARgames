using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int PieceLength = 2;

    public enum Orientation { North, East, South, West};
    public Orientation m_Orientation = Orientation.North;

    public Vector2Int Origin { get; private set; }
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
    
    }

    public void RotatePiece()
    {
        FindNextAvailableRotation();

        transform.rotation = Quaternion.Euler(0f, 90f * (int)m_Orientation, 0f);
    }

    private bool FindNextAvailableRotation()
    {
        bool canPlace = false;
        int iteration = 0;

        while (canPlace == false || iteration < 4)
        {
            m_Orientation++;

            if ((int)m_Orientation > 3)
            {
                m_Orientation = Orientation.North;
            }

            Debug.LogFormat("Attempting Orientation: {0}", m_Orientation.ToString());

            canPlace = GridManager.ValidationLocationRotation(Origin, this);

            if (canPlace == true)
            {
                break;
            }

            iteration++;
        }

        return canPlace;
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
        Origin = originCell.GridPosition;
        m_Orientation = Orientation.West;


        if (FindNextAvailableRotation())
        {
            Debug.Log("Found Appropriate Position");

            transform.position = originCell.transform.position;

            transform.rotation = Quaternion.Euler(0f, 90f * (int)m_Orientation, 0f);
        }
        else
        {
            Debug.Log("No available room.");
        }
    }
}
