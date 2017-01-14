using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public GameObject JumpedPiece;

    private Vector3 _initialPoint;
    private bool _firstTrigger;

    void Start()
    {
        _firstTrigger = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " triggered " + other.gameObject.name);
        var gameController = FindObjectOfType<GameController>().GetComponent<GameController>();

        if (_firstTrigger && !gameController.IsActivePlayerPiece(other.gameObject))
        {
            _firstTrigger = false;
            JumpedPiece = other.gameObject;
            gameObject.SetActive(true);

            float deltaX = gameObject.transform.position.x - _initialPoint.x;
            float deltaZ = gameObject.transform.position.z - _initialPoint.z;
            Vector3 nextPos = new Vector3(gameObject.transform.position.x + deltaX, 0, gameObject.transform.position.z + deltaZ);

            gameObject.transform.position = nextPos;
        }
        else
        {
            Deactivate();
        }
    }

    public void Move(Vector3 piece, Vector3 space)
    {
        gameObject.transform.position = space;
        _initialPoint = piece;
        Activate();
    }

    public void Deactivate()
    {
        JumpedPiece = null;
        _firstTrigger = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// To be called after the BoardSpace game object is moved to its initail position.
    /// </summary>
    public void Activate()
    {
        _firstTrigger = true;
        gameObject.SetActive(true);
    }
}
