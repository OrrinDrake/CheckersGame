using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    private int _playerId;
    private bool _isCaptured;
    private bool _isKing;

    public int PlayerId
    {
        get { return _playerId; }
        set { _playerId = value; }
    }

    public bool IsCaptured
    {
        get { return _isCaptured;  }
        set { _isCaptured = value; }
    }

    public bool IsKing
    {
        get { return _isKing; }
        set { _isKing = value; }
    }

	// Use this for initialization
	void Start ()
    {
	    if(name == "WhitePiece")
        {
            PlayerId = 1;
        }
        else
        {
            PlayerId = 2;
        }
	}	
	// Update is called once per frame
	void Update ()
    {
	
	}


}
