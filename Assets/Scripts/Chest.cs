using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 250;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + pesosAmount + " rsd!", 60, Color.white, transform.position, Vector3.up * 25, 2.0f);

            GameManager.instance.pesos += pesosAmount;
        }
    }
}
