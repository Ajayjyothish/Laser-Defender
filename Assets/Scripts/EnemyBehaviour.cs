using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var missile = collision.gameObject.GetComponent<LaserHit>();
        if (missile)
        {
            Destroy(gameObject);
        }
    }
}
