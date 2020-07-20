using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    public UI_Inventory uiInventory;
    private Inventory inventory;
    

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                Debug.Log("HEAL");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                Debug.Log("ManaPotion");
                break;
            case Item.ItemType.Coin:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Coin, amount = 1 });
                Debug.Log("Money");
                break;
            case Item.ItemType.Sword:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
                Debug.Log("Sword");
                break;
        }
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void recieveDamage(Damage dmg)
    {
        base.recieveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        UpdateMotor(new Vector3 (x, y, 0));
    }
    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }
    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for(int i=0; i<level; i++)
            OnLevelUp();
    }
    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "HP", 25, Color.green, transform.position, Vector3.up *25, 1f);
        GameManager.instance.OnHitpointChange();
    }
    protected override void Death()
    { 
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");
    }
    public void Respawn()
    {
        Heal(maxHitpoint);
        lastInmune = Time.time;
        pushDirection = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

}
