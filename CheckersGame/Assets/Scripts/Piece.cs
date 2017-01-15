using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool IsCaptured;
    public bool IsKing;
    
    void OnMouseDown()
    {
        var script = GameObject.Find("GameController").GetComponent<GameController>();
        script.SelectPiece(gameObject);
    }

}
