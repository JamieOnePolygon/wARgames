using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GamePiece SelectedGameObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Getting any object under the mouse/touch position
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(screenRay, out hit))
            {
                GridCell cell = hit.collider.gameObject.GetComponent<GridCell>();

                if (cell != null)
                {
                    Debug.Log(cell.gameObject.name);

                    if (SelectedGameObject != null)
                    {
                        SelectedGameObject.TemporarilyPlacePiece(cell);
                    }
                }

                GamePiece gamePiece = hit.collider.transform.parent.GetComponent<GamePiece>();

                if (gamePiece != null)
                {
                    SelectedGameObject = gamePiece;
                }

            }
        }

    }

    public void RotateSelectedPiece()
    {
        if (SelectedGameObject != null)
        {
            SelectedGameObject.RotatePiece();
        }
    }
}
