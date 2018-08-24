using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    //config parameters
    [SerializeField] GameObject lightShips;
    [SerializeField] float radius=1f;
    [SerializeField] float height;
    [SerializeField] float width;
    [Range(1,10)] [SerializeField] float shipSpeed = 1f;
    [SerializeField] float shipBound=1.5f;
    [SerializeField] float spawnDelay = 0.5f;
   

    public bool moveLeft=true;
    float xMaxBound, xMinBound, yMinBound, yMaxBound;
    

   
    void Start ()
    {
        SetUpMoveBoundaries();
        spawnUntillFull();
      
    }

    void Update()
    {
        MoveOnScreenEnemy();
      
        respawn();

    }

    private void respawn()
    {
        if (AllChildrenAreDead())
        {

            spawnUntillFull();

        }
        
       
    }

    Transform NextFreePosition()
    {
        foreach(Transform child in transform)
        {
            if (child.childCount == 0)
                return child;
        }
        return null;
    }

    private bool AllChildrenAreDead()
    {
       foreach( Transform child in transform)
        {
            if (child.childCount > 0)
                return false;
            
                
        }
        return true;
    }

    private void spawnUntillFull()
    {
        var nextPosition = NextFreePosition();
        if (nextPosition) { 
        GameObject ships = Instantiate(lightShips, nextPosition.position, nextPosition.rotation) as GameObject;
        ships.transform.parent = nextPosition;
        }if (NextFreePosition())
            Invoke("spawnUntillFull", spawnDelay);
        
        
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMinBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMaxBound = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
        foreach(Transform child in transform)
        {
            Gizmos.DrawWireSphere(child.position, radius);
        }
    }


 

    private void MoveOnScreenEnemy()
    {
        var moveX = shipSpeed * Time.deltaTime;
        if (moveLeft)
        {

            transform.position += Vector3.left * moveX;
        }
        else
        {
            transform.position += Vector3.right * moveX;
        }


        if (transform.position.x <= xMinBound + shipBound)
        {
            moveLeft = false;
        }
        else if (transform.position.x >= xMaxBound - shipBound)
        {
            moveLeft = true;
        }
    }

}
