using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tellObjectives : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "player_0")
        {
            GameManager.instance.ShowText("Go trough the Dangeon and find a room full of GOLD!!!", 20, Color.black, transform.position, Vector3.zero, 10.0f);
        }
    }
}
