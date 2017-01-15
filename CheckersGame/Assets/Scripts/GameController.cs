using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameObject Space1, Space2, Space3, Space4;
    public Text GameInfo;
    public bool jumpsOnly;

    private GameObject SelectedPiece;
    private Player _activePlayer, _inactivePlayer;
    private bool canSelect;

    private GameObject[] _Spaces
    {
        get { return new GameObject[] { Space1, Space2, Space3, Space4 }; }
    }

    void Start()
    {
        _activePlayer = gameObject.AddComponent<Player>();
        _inactivePlayer = gameObject.AddComponent<Player>();
        _activePlayer.pieces = GameObject.FindGameObjectsWithTag("White").ToList();
        _inactivePlayer.pieces = GameObject.FindGameObjectsWithTag("Black").ToList();
        _activePlayer.Orientation = -1;
        _inactivePlayer.Orientation = 1;

        canSelect = true;
        jumpsOnly = false;

        HideSpaces();
        GameInfo.text = "White's Turn";
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
        if(canSelect)
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
    }

    private void ShowSpaces(Vector3 piecePos)
    {
        int i = 0;
        int direction = 1;
        HideSpaces();

        do
        {
            int side = 1;

            for (int j = 0; j < 2; j++)
            {
                Vector3 spacePos = new Vector3(piecePos.x + side, 0, piecePos.z + _activePlayer.Orientation * direction);

                if (IsOnBoard((int)spacePos.x, (int)spacePos.z))
                {
                    var boardSpace = _Spaces[i].GetComponent<BoardSpace>();
                    boardSpace.Move(piecePos, spacePos);
                    boardSpace.isKingSpace = IsOnKingSpace((int)spacePos.z);
                }

                if(jumpsOnly)
                    _Spaces[i].GetComponent<MeshRenderer>().enabled = false;
                else
                    _Spaces[i].GetComponent<MeshRenderer>().enabled = true;

                side = -1;
                i++;
            }

            direction = -1;
        }
        while (SelectedPiece.GetComponent<Piece>().IsKing && i < 4);

    }

    public bool IsOnBoard(int posX, int posZ)
    {
        return (posX >= 0 && posX <= 7) && (posZ >= 0 && posZ <= 7);
    }

    public bool IsOnKingSpace(int posZ)
    {
        return (_activePlayer.Orientation == 1 && posZ == 7) || (_activePlayer.Orientation == -1 && posZ == 0);
    }

    public bool IsActivePlayerPiece(GameObject piece)
    {
        return _activePlayer.pieces.Contains(piece);
    }

    public void MoveSelected(BoardSpace spaceObj)
    {
        canSelect = false;
        SetSelectable(false);
        SelectedPiece.transform.position = spaceObj.transform.position;

        if (spaceObj.isKingSpace)
            SelectedPiece.GetComponent<Piece>().IsKing = true;
        
        if (spaceObj.JumpedPiece != null)
        {
            Capture(spaceObj.JumpedPiece);

            if(_inactivePlayer.pieces.Count == 0)
            {
                //implement game over
                canSelect = false;
                HideSpaces();
                
                GameInfo.text = "Game Over : " + _activePlayer.pieces[0].tag + " Wins!";
            }
            else
            {
                jumpsOnly = true;
                ShowSpaces(SelectedPiece.transform.position);
            }
        }
        else
        {
            HideSpaces();
        }
    }
    
    private void Capture(GameObject piece)
    {
        _inactivePlayer.pieces.Remove(piece);
        piece.SetActive(false);
    }

    public void EndTurn()
    {
        var tmp = _activePlayer;
        _activePlayer = _inactivePlayer;
        _inactivePlayer = tmp;

        canSelect = true;
        jumpsOnly = false;
        //set space bools to default
        SetSelectable(true);

        HideSpaces();
        GameInfo.text = _activePlayer.pieces[0].tag + "'s Turn";
    }

    private void SetSelectable(bool value)
    {
        foreach(GameObject space in _Spaces)
            space.GetComponent<BoardSpace>().selectable = value;
    }
    
}
