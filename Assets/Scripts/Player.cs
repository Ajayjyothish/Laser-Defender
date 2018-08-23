using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //config parameters
    [SerializeField] float PlayerSpeed =1f;
    [SerializeField] float BoundaryPadding = 1f;
    [SerializeField] GameObject PlayerLaser;
    [SerializeField] float LaserSpeed = 5f;
    [SerializeField] float LaserDelay;


    GameObject laser;
    float xMaxBound,xMinBound,yMinBound,yMaxBound;
    Coroutine fireContinuously;
    
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
        Fire();
    }

    private void Fire()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            
             fireContinuously= StartCoroutine(FireAndWait());
           
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireContinuously);
            
        }
    }

    IEnumerator FireAndWait()
    {
        while (true)
        {
            laser = Instantiate(PlayerLaser, transform.position, transform.rotation);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LaserSpeed);
            yield return new WaitForSeconds(LaserDelay);
        }
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
