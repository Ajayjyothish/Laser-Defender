using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    [SerializeField] List<Transform> enemyPath;
    [SerializeField] float speed;
    [SerializeField] GameObject enemy;
    [SerializeField] float radius = 0.5f;
    [SerializeField] float spawnDelay = 2f;
   

    int enemyPathIndex = 0;
    GameObject enemyShip;
    Transform initialTransform;

    private void OnDrawGizmos()
    {
        foreach(Transform point in enemyPath)
        {
            Gizmos.DrawWireSphere(point.position, radius);
        }
    }

    // Use this for initialization
    void Start ()
    {
        
         GetStartLocation();
        
    }

   
    void Update ()
    {
       
       
        respawn();

    }

    private void respawn()
    {
        if (isAllChildrenDead())
        {
            enemyPathIndex = 0;
            GetStartLocation();
            Invoke("FollowPath", spawnDelay);

        }
        else
            FollowPath();
         
            
    }

    bool isAllChildrenDead()
    {
        if (transform.childCount > 0)
            return false;
        else  { 
            return true;
        }
    }

   
   

    
    private void GetStartLocation()

    {   
        enemyShip = Instantiate(enemy, enemyPath[enemyPathIndex].position, transform.rotation) as GameObject;
        enemyShip.transform.parent = transform;
    }
    private void FollowPath()
    {
        if (enemyPathIndex <= enemyPath.Count - 1)
        {

            var targetLocation = enemyPath[enemyPathIndex].position;
            var enemySpeed = speed * Time.deltaTime;
            enemyShip. transform.position = Vector2.MoveTowards( enemyShip. transform.position, targetLocation, enemySpeed);

            if (enemyShip. transform.position == targetLocation)
                enemyPathIndex++;

        }

        else
        {

            Destroy(enemyShip);
           
        }
    }
}