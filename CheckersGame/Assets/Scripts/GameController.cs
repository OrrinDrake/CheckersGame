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

    void Update()
    {

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

    void FixedUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //    //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        //    RaycastHit hit = new RaycastHit();
        //    Physics.Raycast(ray, out hit);
            
        //    if (hit.transform.tag == "White" || hit.transform.tag == "Black")
        //    {
        //        //if ((SelectedPiece != null && SelectedPiece.Equals(hit)) || _inactivePlayer.pieces.Contains(hit.transform.gameObject))
        //        //{
        //        //    SelectedPiece = null;
        //        //    HideSpaces();
        //        //}
        //        //else
        //        //{
        //        //    SelectedPiece = hit.transform.gameObject;
        //        //    //ShowSpaces(hit.transform.position);
        //        //}
        //    }
        //    else if (hit.transform.gameObject.GetComponent("BoardSpace"))
        //    {
        //        //MoveTo(hit.transform.position);
        //    }
            
        //}
    }

    private void ShowSpaces(Vector3 piecePos)
    {
        int i = 0;
        int reverse = 1;
        GameObject[] spaces = new GameObject[] { Space1, Space2, Space3, Space4 };

        do
        {
            Vector3 spacePos = new Vector3(piecePos.x++, piecePos.z + _activePlayer.Orientation * reverse);
            spaces[i].transform.position = spacePos;
            CheckSpace(spaces[i], piecePos);
            i++;

            spacePos = new Vector3(piecePos.x--, piecePos.z + _activePlayer.Orientation * reverse);
            spaces[i].transform.position = spacePos;
            CheckSpace(spaces[i], piecePos);
            i++;

            reverse = -1;
        }
        while (SelectedPiece.GetComponent<Piece>().IsKing && i < 4);

    }

    private void CheckSpace(GameObject spaceObj, Vector3 piecePos)
    {
        Vector3 spacePos = spaceObj.transform.position;

        if ((spacePos.x >= 0 && spacePos.x <= 7) && (spacePos.z >= 0 && spacePos.z <= 7))
        {
            GameObject occupant = FindOccupant(spaceObj);

            if(occupant == null)
            {
                occupant = FindOccupant(spaceObj);

                if(occupant == null)
                {
                    spaceObj.GetComponent<BoardSpace>().JumpedOver = null;
                    Show(spaceObj);
                }
                else
                {
                    float deltaX = spacePos.x - piecePos.x;
                    float deltaZ = spacePos.z - piecePos.z;
                    Vector3 nextPos = new Vector3(spacePos.x + deltaX, spacePos.z + deltaZ);

                    if(FindOccupant(spaceObj) == null && FindOccupant(spaceObj) == null)
                    {
                        spaceObj.transform.position = nextPos;
                        spaceObj.GetComponent<BoardSpace>().JumpedOver = occupant;
                        Show(spaceObj);
                    }
                    else
                    {
                        spaceObj.GetComponent<BoardSpace>().JumpedOver = null;
                    }
                }
            }
        }
    }

    private GameObject FindOccupant(GameObject space)
    {
        //if colliding with Piece game object, return that game object
        //else return null
        //space.GetComponent<BoxCollider>().
        return null;
    }
}
//                else if (hit is a Space)
//    			{
//                    SelectedPiece.MoveTo(hit)


//                    After movement is finished:
//    				if ((Space)hit.JumpedOver != null)
//                    {
//                        ((Space)hit.).JumpedOver.Capture()

//                        ShowJumpSpaces(hit.transform.position)

//                    }
//                }
//    			else
//    			{
//                    SelectedPiece = null

//                }
//            }
//        }

//        void ShowSpaces(Vector3 piecePos)
//{
//    Space[] spaces = new Space[] { Space1, Space2, Space3, Space4 }

//            Int i = 0

//            Int reverse = 1

//            do
//    {
//        Vector3 spacePos = new Vector3(pos.x++,
//pos.z + ActivePlayer.Orientation * reverse)

//                spaces[i].transform.position = spacePos;
//        CheckSpace(spaces[i], piecePos)

//                i++

//                spacePos = new Vector3(pos.x--,
//    pos.z + ActivePlayer.Orientation * reverse)

//                spaces[i].transform.position = spacePos;
//        CheckSpace(spaces[i], piecePos)

//                i++

//                reverse = -1

//            }
//    while (SelectedPiece.IsKing && i < 4);
//}

//void ShowJumpSpaces(Vector3 pos)
//{
//    //only shows spaces that jump over pieces
//}

//void CheckSpace(Space spaceObj, Vector3 piecePos)
//{
//    Vector3 spacePos = spaceObj.transform.position


//            If(spacePos >= 0 && spacePos <= 7)

//            {
//        GameObject occupant = FindOccupant(ActivePlayer.Pieces, spacePos)


//                If(occupant == null)

//                {
//            occupant = FindOccupant(InactivePlayer.Pieces, spacePos)


//    If(occupant == null)
//    {
//                spaceObj.JumpedOver = null

//        Show(spaceObj)
//    }
//    else
//    {
//                deltaX = spacePos.x - piecePos.x

//        deltaZ = spacePos.z - piecePos.z

//        Vector3 nextPos = new Vector3(spacePos.x + deltaX,
//                        spacePos.z + deltaZ)

//        If(FindOccupantAt(ActivePlayer.Pieces, nextPos) == null
//                           && FindOccupantAt(InactivePlayer.Pieces, nextPos) == null)

//        {
//                    spaceObj.transform.position = nextPos

//            spaceObj.JumpedOver = occupant

//            Show(spaceObj)

//        }
//    	else
//    	{
//                    spaceObj.JumpedOver = null

//        }
//            }
//        }
//    }
//}

//public GameObject FindOccupantAt(GameObject[] pieceList, Vector3 pos)
//{
//    foreach (GameObject piece in pieceList)
//    {
//        If(piece.transform.position.Equals(pos))

//                    return piece;
//    }

//    return null

//        }

//void Capture(Piece p)
//{
//    InactivePlayer.Pieces.Remove(p)

//            Destroy gameobject p

//        }

//    }


//}
