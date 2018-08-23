using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    [SerializeField] List<Transform> enemyPath;
    [SerializeField] float speed;
    [SerializeField] GameObject enemy;
    [SerializeField] float ShootDelay;
    [SerializeField] GameObject laser;
    [SerializeField] float LaserSpeed;

    int enemyPathIndex = 0;
    GameObject enemyShip;

    // Use this for initialization
    void Start ()
    {
        GetStartLocation();
        FireLaser();
    }

   
    void Update ()
    {
        FollowPath();

    }


    private void FireLaser()
    {
        InvokeRepeating("ShootAndWait",0.000001f,ShootDelay);
    }

    void ShootAndWait()
    {
        var lasers=  Instantiate(laser, transform.position, Quaternion.identity);
        lasers.GetComponent<Rigidbody2D>().velocity = Vector2.down * LaserSpeed;
    }
   

    
    private void GetStartLocation()
    {
        enemyShip = Instantiate(enemy, enemyPath[enemyPathIndex].position, transform.rotation);
        enemyShip.transform.parent = transform;
    }
    private void FollowPath()
    {
        if (enemyPathIndex <= enemyPath.Count - 1)
        {

            var targetLocation = enemyPath[enemyPathIndex].position;
            var enemySpeed = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, enemySpeed);

            if (transform.position == targetLocation)
                enemyPathIndex++;

        }

        else
            Destroy(gameObject);
    }
}