using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour {


    [SerializeField] GameObject EnemyLaser;
    [SerializeField] float LaserSpeed = 5f;
    [SerializeField] float shotsPerSecond = 0.5f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip shipDestSound;
    public float shipScore = 10f;
    
    

    public static float scores=00f;

    private void Update()
    {
        CheckProbabilityandFire();
    }

    private void CheckProbabilityandFire()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (UnityEngine.Random.value < probability)
            FireLaser();
    }

    private void FireLaser()
    {
        
        var laser = Instantiate(EnemyLaser, transform.position, transform.rotation) as GameObject;
        GetComponent<AudioSource>().PlayOneShot(laserSound);
        laser.GetComponent<Rigidbody2D>().velocity = Vector2.down * LaserSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var missile = collision.gameObject.GetComponent<LaserHit>();
        if (missile)
        {
            var scoreSheet = FindObjectOfType<ScoreKeeper>();
            scoreSheet.KeepScore(shipScore);
            AudioSource.PlayClipAtPoint(shipDestSound, new Vector3(transform.position.x, transform.position.y));
            Destroy(gameObject);
            
            Destroy(collision.gameObject);
        }
    }

    
}
