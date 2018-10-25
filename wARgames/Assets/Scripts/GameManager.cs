using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GridManager m_GridManager;

    // Handling the Players CurrentState.
    public enum PlayerState { Setup, Targetting, Waiting };
    public PlayerState CurrentPlayerState = PlayerState.Setup;

    // Check to see if the player has any unplaced pieces, if they do we will enter targeting state.
    public int StartingPieces = 5;
    private int unplacedPieces;

    public LayerMask SetupMask;
    public LayerMask TargetingMask;

    // Keeping a Reference of the GamePiece that we have selected in the Setup phase.
    public GamePiece SelectedPiece;

	// Use this for initialization
	void Start ()
    {
        if (Instance == null) Instance = this;

        m_GridManager = FindObjectOfType<GridManager>();
        unplacedPieces = StartingPieces;
	}

    public void Update()
    {
        HandleUserInput();
    }

    private void HandleUserInput()
    {
        // Checking to see if we have clicked the mouse.
        if (Input.GetMouseButtonDown(0))
        {
            // Turning the Camera Direction and Mouse Position into a Ray to be used in a Raycast.
            Ray screnRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;           // Creating a RaycastHit to store the data of any hit object.

            // Here we should probably do a check to see which PlayerState we are in to determine if we should look for game pieces as
            // well as grid cells.
            LayerMask mask = (CurrentPlayerState == PlayerState.Setup) ? SetupMask : TargetingMask;

            //Firing a Raycast using the ray and passing data into the RaycastHit we have created.
            if (Physics.Raycast(screnRay, out hitObject, mask))
            {
                GameObject hit = hitObject.collider.gameObject;

                //Getting the GridCell and the GamePiece Components
                GridCell cell = hit.GetComponent<GridCell>();
                GamePiece gamePiece = hit.transform.parent.GetComponent<GamePiece>();   // We need to get the parent of the hit object as currently the collider of game pieces is on a child of the parent object.

                // Checking to see if the GridCell is null
                if (cell != null)
                {
                    // As there are multiple reasons why we could have pressed on a GridCell, we shall check to see
                    // what PlayerState we are in to see which logic we should run.
                    if (CurrentPlayerState == PlayerState.Setup)
                    {
                        // If we are in setup, then we want to place the game piece at the selected cell.
                        if (SelectedPiece == null)
                        {
                            Debug.LogFormat("Selected GridCell: {0}, with no Selected GamePiece", cell.GridPosition);
                        }
                        else
                        {
                            Debug.LogFormat("We have placed {0} at the GridCell: {1}", SelectedPiece.name, cell.GridPosition);
                            SelectedPiece.TemporarilyPlacePiece(cell);
                        }

                    }
                    else if (CurrentPlayerState == PlayerState.Targetting)
                    {
                        // If the player is in a Targeting state, then we want to assume that they are checking to
                        // see if there is a game piece in this current cell.
                        bool validTarget = m_GridManager.QueryCell(cell.GridPosition);

                        if (validTarget)
                        {
                            if (cell.ContainsGamePiece)
                            {
                                Debug.LogFormat("{0}: HIT!", cell.GridPosition);
                            }
                            else
                            {
                                Debug.LogFormat("{0}: MISS!", cell.GridPosition);
                            }
                        }
                        else
                        {
                            Debug.Log("Please Select another Target");
                        }
                    }
                }

                // Checking to see if we are trying to select a valid gamePiece and if we are in the Setup State.
                if (gamePiece != null && CurrentPlayerState == PlayerState.Setup)
                {
                    SelectedPiece = gamePiece;
                }
            }
        }
    }

    public void RotateSelectedPiece()
    {
        if(SelectedPiece != null)
        {
            SelectedPiece.RotatePiece();
        }
    }

    public void PlaceSelectedPiece()
    {
        if (SelectedPiece != null)
        {
            m_GridManager.PlacePiece(SelectedPiece.Origin, SelectedPiece);
            SelectedPiece = null;

            RegisterPiecePlaced();
        }
    }

    public void RegisterPiecePlaced()
    {
        unplacedPieces--;

        if (unplacedPieces == 0)
        {
            Debug.Log("you have placed all GamePieces. Now entering TARGETING state.");
            CurrentPlayerState = PlayerState.Targetting;
        }
    }
}
