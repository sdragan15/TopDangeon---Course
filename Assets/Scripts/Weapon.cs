using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct

    public int[] damagePoint = { 1,2,3,5,10,20,50,100};
    public float[] pushForce = { 2.0f, 2.1f, 2.3f, 2.1f, 2.5f, 2.7f, 3.0f, 3.5f};
    public AudioSource swingAudio;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private float cooldown = 0.5f;
    private float lastSwing;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        swingAudio = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
                
            }
        }
    }

    private void Swing()
    {
        anim.SetTrigger("swing");
        swingAudio.Play();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if (coll.name == "player_0")
                return;

            Damage dmg = new Damage
            {
                damageAmaount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        SpriteRenderer sprite = this.GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = GameManager.instance.weaponSprites[weaponLevel];
        

        // change stats
    }

    public void SetWeapon(int level)
    {
        SpriteRenderer sprite = this.GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = GameManager.instance.weaponSprites[level];
    }
}
