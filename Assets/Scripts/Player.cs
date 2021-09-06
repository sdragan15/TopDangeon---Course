using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Mover
{
    public Vector2 speed = new Vector2(1.0f, 0.75f);
  

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        UpdateMotor(new Vector3(x, y, 0), speed);
    }

    protected override void Death()
    {
        Time.timeScale = 0;
        GameManager.instance.ShowText("YOU DIED!", 120, Color.red, transform.position, Vector3.zero, 1.0f);
        GameManager.instance.endGame.TriggerEndAnimation();
    }

    // Swap Sprite
    public void SwapSprite(int id)
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.sprite = GameManager.instance.playerSprites[id];
    }

    public void RespownPlayer()
    {
        Time.timeScale = 1;
        Debug.Log("8888888888888888"); 
        GameManager.instance.player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        GameManager.instance.player.hitpoint = maxHitpoint;
        
    }

}
