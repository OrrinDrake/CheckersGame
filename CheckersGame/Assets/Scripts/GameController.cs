using UnityEngine;
using System.Collections;
using System.Linq;

public class GameController : MonoBehaviour
{

    private Player _activePlayer;
    public Player ActivePlayer
    {
        get { return _activePlayer; }
        set { _activePlayer = value; }
    }
    public Player InactivePlayer;
    public GameObject SelectedPiece;
    public Player Player1, Player2;
    public BoardSpace Space1, Space2, Space3, Space4; //(Empty GameObjects with box colliders)


    void Start()
    {
        Player1.pieces = FindObjectsOfType<GameObject>().Where(x => x.name == "WhitePiece").ToList();

        Player2.pieces = FindObjectsOfType<GameObject>().Where(x => x.name == "BlackPiece").ToList();

        Player1.Orientation = -1;
        Player2.Orientation = 1;
        ActivePlayer = Player1;
        InactivePlayer = Player2;
        Hide(Space1);
        Hide(Space2);
        Hide(Space3);
        Hide(Space4);

    }

    void Update()
    {

    }

    private void Hide(BoardSpace gameobj)
    {
        gameobj.gameObject.SetActive(false);
    }

    void HideSpaces()
    {
        Hide(Space1);
        Hide(Space2);
        Hide(Space3);
        Hide(Space4);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))

        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);

            if (hit.transform.tag == "black" || hit.transform.tag == "white")
            {
                if (SelectedPiece == null && ActivePlayer.pieces.Contains(hit.transform.gameObject))

                {
                    SelectedPiece = hit.transform.gameObject;

                    //ShowSpaces(hit.transform.position);

                }
                else if (SelectedPiece.Equals(hit))
                {
                    SelectedPiece = null;

                    HideSpaces();

                }
            }
        }
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
