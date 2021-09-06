using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 12;

    // Logic
    public float triggerLength = 0.1f;
    public float chaseLengt = 0.2f;
    private bool chasing;
    private bool collidingWithPlayer;
    public Vector2 speed = new Vector2(0.85f, 0.60f);
    private Transform playerTransform;
    private Vector3 startingPosition;

    // hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    public AudioSource enemySound;

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        enemySound = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {

        // Is the player in range?
        if(Vector3.Distance(playerTransform.position, transform.position) < chaseLengt)
        {
            if(Vector3.Distance(playerTransform.position, transform.position) < triggerLength)
            {
                chasing = true;
            }

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized, speed);
                }
                PlaySoundOfEnemy();
            }
            else
            {
                UpdateMotor(startingPosition - transform.position, speed);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position, speed);
            chasing = false;
            StopSoundOfEnemy();
        }

        // check for collide - same as class Collideable
        collidingWithPlayer = false;

        hitbox.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if(hits[i].tag == "Fighter" &&  hits[i].name == "player_0")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }

    }

    public void PlaySoundOfEnemy()
    {
        if (!enemySound.isPlaying)
        {
            Debug.Log("Playing sound");
            enemySound.Play();
        }
        
    }

    public void StopSoundOfEnemy()
    {
        enemySound.Stop();
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText("+" + xpValue + " XP", 60, Color.green, transform.position, Vector3.up * 30, 1.0f);
        StopSoundOfEnemy();
    }
}
