using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public GameObject JumpedPiece;

    private Vector3 _initialPoint;
    private bool _alreadyTriggered;

    void Start()
    {
        _alreadyTriggered = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter " + gameObject.name + " and " + other.gameObject.name);
        if(_alreadyTriggered)
        {
            _alreadyTriggered = false;
            gameObject.SetActive(false);
            JumpedPiece = null;
        }
        else
        {
            JumpedPiece = other.gameObject;
            _alreadyTriggered = true; 

            float deltaX = _initialPoint.x - gameObject.transform.position.x;
            float deltaZ = _initialPoint.z - gameObject.transform.position.z;
            Vector3 nextPos = new Vector3(_initialPoint.x + deltaX, 0, _initialPoint.z + deltaZ);
           
            gameObject.transform.position = nextPos;
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Not what i need for consecutive collision
        Debug.Log("Stay " + gameObject.name + " and " + other.gameObject.name);
    }


    public void MoveTo(Vector3 pos)
    {
        gameObject.SetActive(true);
        _initialPoint = pos;
        gameObject.transform.position = pos;
    }
}
