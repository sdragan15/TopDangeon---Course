using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //damage
    public int damage;
    public float pushForce;
    public float cooldown;
    private float lastTime;

    protected override void OnCollide(Collider2D coll)
    {
        if(Time.time - lastTime >= cooldown)
        {
            
            if (coll.tag == "Fighter" && coll.name == "player_0")
            {
                Damage dmg = new Damage
                {
                    damageAmaount = damage,
                    origin = transform.position,
                    pushForce = pushForce
                };

                lastTime = Time.time;

                coll.SendMessage("ReceiveDamage", dmg);
            }
        }
        
    }
}
