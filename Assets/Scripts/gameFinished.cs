using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameFinished : Collidable
{
    private bool active = false;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "player_0" && !active)
        {
            GameManager.instance.GameFinished();
            active = true;
        }
    }

}
