using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    [SerializeField] List<Transform> enemyPath;
    [SerializeField] float speed;

    int enemyPathIndex = 0;
    
    // Use this for initialization
    void Start () {
        transform.position = enemyPath[enemyPathIndex].position;
		
	}
	
	// Update is called once per frame
	void Update ()
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
