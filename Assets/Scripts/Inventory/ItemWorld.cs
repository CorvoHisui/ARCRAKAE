using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

        if (item.amount > 1)
            textMeshPro.SetText(item.amount.ToString());
        else
            textMeshPro.SetText("");
    }
    public Item GetItem()
    {
        return item;
    }
    public static ItemWorld DropItem(Vector3 position, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld =SpawnItemWorld(position + randomDir * 0.5f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 0.5f, ForceMode2D.Impulse);
        return itemWorld;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
