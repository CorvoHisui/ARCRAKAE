using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 10;

    protected override void OnCollect()
    {
        if(collected == false)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += goldAmount;

            //Llama al texto
            GameManager.instance.ShowText("+" + goldAmount + "Gold" , 25, Color.yellow, transform.position, Vector3.up * 25, 2f); //fontSize, color, position, movimiento, tiempo q se muestra
        }
        
    }
}
