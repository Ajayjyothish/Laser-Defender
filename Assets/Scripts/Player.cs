using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //config parameters
    [SerializeField] float PlayerSpeed = 1f;
    [SerializeField] float BoundaryPadding = 1f;
    [SerializeField] GameObject PlayerLaser;
    [SerializeField] float LaserSpeed = 5f;
    [SerializeField] float LaserDelay=0.1f;
    [SerializeField] float PlayerHealth=5f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip destroyedSound;
    
    [SerializeField] TextMeshProUGUI Health;

    //cached references
    GameObject laser;
    float xMaxBound, xMinBound, yMinBound, yMaxBound;
    Coroutine fireContinuously;
    SceneLoader scene;
    ScoreKeeper score;
    


    void Start()
    {
        SetUpMoveBoundaries();
        Health.text = "Health: " + PlayerHealth;
        scene = FindObjectOfType<SceneLoader>();
        score = FindObjectOfType<ScoreKeeper>();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMinBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMaxBound = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMinBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMaxBound = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {

        if (Input.GetButtonDown("Fire1"))
        {

            fireContinuously = StartCoroutine(FireAndWait());

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
            GetComponent<AudioSource>().PlayOneShot(laserSound);
            
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LaserSpeed);
            yield return new WaitForSeconds(LaserDelay);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMinBound + BoundaryPadding, xMaxBound - BoundaryPadding);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * PlayerSpeed;
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMinBound + BoundaryPadding, yMaxBound - BoundaryPadding);

        transform.position = new Vector2(newXpos, newYpos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var missle = collision.gameObject.GetComponent<EnemyLaserHit>();
        if (missle)
        {
            PlayerHealth -= 1;
            Health.text = "Health: " + PlayerHealth;
            Destroy(collision.gameObject);
            

        }
        if (PlayerHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(destroyedSound,new Vector3(transform.position.x,transform.position.y));
            Destroy(gameObject);
            score.Reset();
            scene.loadNextScene();
        }
    }
    

}
