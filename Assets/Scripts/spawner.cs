using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    //config parameters
    [SerializeField] GameObject lightShips;
    [SerializeField] float height;
    [SerializeField] float width;
    [Range(1,10)] [SerializeField] float shipSpeed = 1f;
    [SerializeField] float shipBound=1.5f;
    [SerializeField] GameObject laser;
    [Range(1, 10)] [SerializeField] float laserSpeed = 2f;
    [Range(0.1f,5)][SerializeField] float LaserDelay = 1f;

    public bool moveLeft=true;
    float xMaxBound, xMinBound, yMinBound, yMaxBound;
    

   
    void Start ()
    {
        SetUpMoveBoundaries();
        spawnEnemies();
        FireLaser();
    }

        void Update()
    {
        MoveOnScreenEnemy();
       

    }

    
    


    private void spawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject ships = Instantiate(lightShips, child.position, child.rotation) as GameObject;
            ships.transform.parent = child;

        }
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
    }

    private void FireLaser()
    {
        
        InvokeRepeating("FireAndWait", 0.00001f, LaserDelay);
    }

    void FireAndWait()
    {
        
        
            foreach (Transform child in transform)
            {
                GameObject lasers = Instantiate(laser, child.position, child.rotation);
                lasers.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
                
               
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
