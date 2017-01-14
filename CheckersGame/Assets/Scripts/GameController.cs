using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameObject Space1, Space2, Space3, Space4;

    private GameObject SelectedPiece;
    private Player _activePlayer, _inactivePlayer;

    void Start()
    {
        _activePlayer = gameObject.AddComponent<Player>();
        _inactivePlayer = gameObject.AddComponent<Player>();

        _activePlayer.pieces = GameObject.FindGameObjectsWithTag("White").ToList();
        _inactivePlayer.pieces = GameObject.FindGameObjectsWithTag("Black").ToList();

        _activePlayer.Orientation = -1;
        _inactivePlayer.Orientation = 1;

        HideSpaces();
    }
    
    private void Hide(GameObject gameobj)
    {
        gameobj.GetComponent<BoardSpace>().Deactivate();
    }

    private void HideSpaces()
    {
        Space1.GetComponent<BoardSpace>().Deactivate();
        Space2.GetComponent<BoardSpace>().Deactivate();
        Space3.GetComponent<BoardSpace>().Deactivate();
        Space4.GetComponent<BoardSpace>().Deactivate();
    }

    public void SelectPiece(GameObject piece)
    {
        HideSpaces();

        if ((SelectedPiece != null && SelectedPiece.Equals(piece)) || _inactivePlayer.pieces.Contains(piece))
        {
            SelectedPiece = null;
        }
        else
        {
            SelectedPiece = piece;
            ShowSpaces(piece.transform.position);
        }
    }

    private void ShowSpaces(Vector3 piecePos)
    {
        int i = 0;
        int direction = 1;
        GameObject[] spaces = new GameObject[] { Space1, Space2, Space3, Space4 };

        do
        {
            Vector3 spacePos = new Vector3(piecePos.x + 1, 0, piecePos.z + _activePlayer.Orientation * direction);
            if (IsOnBoard((int)spacePos.x, (int)spacePos.z))
                spaces[i].GetComponent<BoardSpace>().Move(piecePos, spacePos);
            i++;

            spacePos = new Vector3(piecePos.x - 1, 0, piecePos.z + _activePlayer.Orientation * direction);
            if (IsOnBoard((int)spacePos.x, (int)spacePos.z))
                spaces[i].GetComponent<BoardSpace>().Move(piecePos, spacePos);
            i++;

            direction = -1;
        }
        while (SelectedPiece.GetComponent<Piece>().IsKing && i < 4);

    }

    private bool IsOnBoard(int posX, int posZ)
    {
        return (posX >= 0 && posX <= 7) && (posZ >= 0 && posZ <= 7);
    }

    public bool IsActivePlayerPiece(GameObject piece)
    {
        return _activePlayer.pieces.Contains(piece);
    }

    //private void CheckSpace(GameObject spaceObj, Vector3 piecePos)
    //{
    //    Vector3 spacePos = spaceObj.transform.position;

    //    if ((spacePos.x >= 0 && spacePos.x <= 7) && (spacePos.z >= 0 && spacePos.z <= 7))
    //    {
    //        GameObject occupant = FindOccupant(spaceObj);

    //        if (occupant == null)
    //        {
    //            occupant = FindOccupant(spaceObj);

    //            if (occupant == null)
    //            {
    //                //spaceObj.GetComponent<BoardSpace>().JumpedOver = null;
    //                Show(spaceObj);
    //            }
    //            else
    //            {
    //                float deltaX = spacePos.x - piecePos.x;
    //                float deltaZ = spacePos.z - piecePos.z;
    //                Vector3 nextPos = new Vector3(spacePos.x + deltaX, 0, spacePos.z + deltaZ);

    //                if (FindOccupant(spaceObj) == null && FindOccupant(spaceObj) == null)
    //                {
    //                    spaceObj.transform.position = nextPos;
    //                    //spaceObj.GetComponent<BoardSpace>().JumpedOver = occupant;
    //                    Show(spaceObj);
    //                }
    //                else
    //                {
    //                    //spaceObj.GetComponent<BoardSpace>().JumpedOver = null;
    //                }
    //            }
    //        }
    //    }
    //}

}
