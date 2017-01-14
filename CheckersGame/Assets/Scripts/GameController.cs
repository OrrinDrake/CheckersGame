using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private GameObject SelectedPiece;
    private Player Player1, Player2;
    private Player _activePlayer, _inactivePlayer;
    public GameObject Space1, Space2, Space3, Space4;

    void Start()
    {
        Player1 = gameObject.AddComponent<Player>();
        Player2 = gameObject.AddComponent<Player>();

        Player1.pieces = GameObject.FindGameObjectsWithTag("White").ToList();
        Player2.pieces = GameObject.FindGameObjectsWithTag("Black").ToList();

        Player1.Orientation = -1;
        Player2.Orientation = 1;
        _activePlayer = Player1;
        _inactivePlayer = Player2;

        HideSpaces();
    }
    
    private void Hide(GameObject gameobj)
    {
        gameobj.gameObject.SetActive(false);
    }

    private void Show(GameObject gameobj)
    {
        gameobj.gameObject.SetActive(true);
    }

    private void HideSpaces()
    {
        Hide(Space1);
        Hide(Space2);
        Hide(Space3);
        Hide(Space4);
    }

    public void SelectPiece(GameObject piece)
    {
        if ((SelectedPiece != null && SelectedPiece.Equals(piece)) || _inactivePlayer.pieces.Contains(piece))
        {
            SelectedPiece = null;
            HideSpaces();
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
            if (IsOnBoard(spacePos))
                spaces[i].GetComponent<BoardSpace>().MoveTo(spacePos);
            i++;

            spacePos = new Vector3(piecePos.x - 1, 0, piecePos.z + _activePlayer.Orientation * direction);
            if (IsOnBoard(spacePos))
                spaces[i].GetComponent<BoardSpace>().MoveTo(spacePos);
            i++;

            direction = -1;
        }
        while (SelectedPiece.GetComponent<Piece>().IsKing && i < 4);

    }

    private bool IsOnBoard(Vector3 pos)
    {
        return (pos.x >= 0 && pos.x <= 7) && (pos.z >= 0 && pos.z <= 7);
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
