using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public GameObject JumpedPiece;
    public bool selectable;
    public bool isKingSpace;

    private Vector3 _piecePos;
    private bool _firstTrigger;
    //private bool _jumpsOnly;

    void Start()
    {
        _firstTrigger = true;
        selectable = true;
        //_jumpsOnly = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        var gameController = FindObjectOfType<GameController>().GetComponent<GameController>();

        if (_firstTrigger && !gameController.IsActivePlayerPiece(other.gameObject))
        {
            //if(_jumpsOnly)
            if(gameController.jumpsOnly)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                selectable = true;
            }

            _firstTrigger = false;
            JumpedPiece = other.gameObject;
            gameObject.SetActive(true);

            float deltaX = gameObject.transform.position.x - _piecePos.x;
            float deltaZ = gameObject.transform.position.z - _piecePos.z;
            Vector3 nextPos = new Vector3(gameObject.transform.position.x + deltaX, 0, gameObject.transform.position.z + deltaZ);

            isKingSpace = gameController.IsOnKingSpace((int)nextPos.z);

            if (FindObjectOfType<GameController>().GetComponent<GameController>().IsOnBoard((int)nextPos.x, (int)nextPos.z))
                gameObject.transform.position = nextPos;
            else
                Deactivate();
        }
        else
        {
            Deactivate();
        }
    }

    void OnMouseDown()
    {
        //if(!(_jumpsOnly && !selectable))
        if(selectable)
        {
            FindObjectOfType<GameController>().GetComponent<GameController>().MoveSelected(this);
        }
    }

    public void Move(Vector3 piece, Vector3 space)
    {
        gameObject.transform.position = space;
        _piecePos = piece;
        Activate();
    }

    public void Deactivate()
    {
        JumpedPiece = null;
        _firstTrigger = false;
        //_jumpsOnly = true;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// To be called after the BoardSpace game object is moved to its initail position.
    /// </summary>
    public void Activate()
    {
        _firstTrigger = true;
        //_jumpsOnly = false;
        gameObject.SetActive(true);
    }
    
}
