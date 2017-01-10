using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public float xcoord, zcoord;
	// Use this for initialization
	void Start ()
    {
        xcoord = transform.position.x;
        zcoord = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
