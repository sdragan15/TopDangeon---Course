using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "player_0")
        {
            //teleport the player

            GameManager.instance.SaveState();
            string name = sceneNames[0];

            SceneManager.LoadScene(name);
        }
    }
}
