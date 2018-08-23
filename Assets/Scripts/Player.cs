using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float PlayerSpeed =1f;
    [SerializeField] float BoundaryPadding = 1f;
    float xMaxBound,xMinBound,yMinBound,yMaxBound;
    
	void Start () {
        SetUpMoveBoundaries();
	}

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMinBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMaxBound = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMinBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMaxBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        

    }

    void Update () {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime* PlayerSpeed;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX,xMinBound+BoundaryPadding,xMaxBound-BoundaryPadding);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * PlayerSpeed;
        var newYpos = Mathf.Clamp(transform.position.y + deltaY,yMinBound+BoundaryPadding,yMaxBound-BoundaryPadding);

        transform.position = new Vector2(newXpos, newYpos);


       
    }
}
