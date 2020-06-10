using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{

    private Inventory inventory;

    public UI_Inventory uiInventory;

    private Transform playerTransform;

    private void Start()
    {
        inventory = new Inventory();
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        playerTransform = GetComponent<Transform>();

        //ItemWorld.SpawnItemWorld(new Vector3(1, 1), new Item { itemType = Item.ItemType.Coin, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(-1, 1), new Item { itemType = Item.ItemType.Heart, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(1, -1), new Item { itemType = Item.ItemType.Gem, amount = 1 });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null && !inventory.isInventoryFull(itemWorld.GetItem()))
        {
            inventory.AddItem(itemWorld.GetItem());
            uiInventory.PlayPickup();
            itemWorld.DestroySelf();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
