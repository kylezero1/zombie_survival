using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {        
        if (item.itemType == Item.ItemType.Barricade)
        {
            Transform transform = Instantiate(ItemAssets.Instance.barricadePrefab, position, Quaternion.identity).GetComponent<Transform>();
            ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
            return itemWorld;
        } else
        {
            Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

            if (item.itemType == Item.ItemType.Wood)
            {
                transform.localScale -= new Vector3(4.75f, 4.75f, 0);
            }
            if (item.itemType == Item.ItemType.Apple)
            {
                transform.localScale -= new Vector3(4.75f, 4.75f, 0);
            }

            ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
            itemWorld.SetItem(item);

            return itemWorld;
        }                
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        if (item.itemType == Item.ItemType.Barricade)
        {
            ItemWorld itemWorld = SpawnItemWorld(dropPosition, item);
            return itemWorld;
        } else
        {
            Vector3 randomDir = UtilsClass.GetRandomDir();
            ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 1.0f, item);
            return itemWorld;
        }        
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("amountText").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            if (item.itemType == Item.ItemType.Wood)
            {
                textMeshPro.transform.position += new Vector3(.1f, 0, 0);
                textMeshPro.transform.localScale += new Vector3(.04f, .04f, 0);
            }
            textMeshPro.SetText(item.amount.ToString());
        } else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
