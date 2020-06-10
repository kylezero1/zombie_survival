using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private bool isShowing;
    public bool isGameOver;
    public CanvasGroup canvasGroup;

    private PlayerInventoryController player;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerMovement playerMovement;

    private AudioSource audio_source;
    public AudioClip item_pickup;
    public AudioClip item_drop;

    public Shooting shooting;

    private void Update()
    {
        if (Input.GetKeyDown("tab") && isGameOver == false)
        {
            isShowing = !isShowing;
            if (isShowing)
            {
                canvasGroup.alpha = 1;
            } else
            {
                canvasGroup.alpha = 0;
                shooting.canShoot = true;
            }
        }
    }

    public void SetPlayer(PlayerInventoryController player)
    {
        this.player = player;
    }

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        canvasGroup = GetComponent<CanvasGroup>();

        audio_source = GetComponent<AudioSource>();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject); //if child is not itemSlotTemplate
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 130f; //Size of each item container

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOverFunc = () => //Mouse hover       
            {
                if (isShowing)
                {
                    shooting.canShoot = false;
                }                
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOutOnceFunc = () =>
            {
                shooting.canShoot = true;
            };

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => //Left click            
            {
                //Use item
                if (isShowing)
                {
                    if (item.itemType == Item.ItemType.Heart) //Heal player
                    {
                        if (playerMovement.health < 1f)
                        {
                            playerMovement.health += .20f;
                            if (playerMovement.health > 1f) playerMovement.health = 1f;
                            healthBar.SetSize(playerMovement.health);
                            inventory.RemoveItem(item);
                        }
                    }
                    if (item.itemType == Item.ItemType.Apple) //Heal player
                    {
                        if (playerMovement.health < 1f)
                        {
                            playerMovement.health += .05f;
                            if (playerMovement.health > 1f) playerMovement.health = 1f;
                            healthBar.SetSize(playerMovement.health);
                            inventory.RemoveItem(item);
                        }
                    }
                    if (item.itemType == Item.ItemType.Wood && item.amount >= 4) //Craft barricade
                    {
                        Item tempBarricade = new Item { itemType = Item.ItemType.Barricade, amount = 1 };
                        if (!inventory.isInventoryFull(tempBarricade))
                        {
                            item.amount -= 4;
                            inventory.AddItem(new Item { itemType = Item.ItemType.Barricade, amount = 1 });
                            if (item.amount == 0)
                            {
                                inventory.RemoveItem(item);
                            }
                        }                        
                    }
                    shooting.canShoot = true;
                }                
            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => //Right click
            {
                //Drop item
                if (isShowing)
                {
                    Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                    inventory.RemoveItem(item);
                    PlayDrop();
                    ItemWorld.DropItem(player.GetPosition(), duplicateItem);
                    shooting.canShoot = true;
                }                               
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            } else
            {
                uiText.SetText("");
            }


            x++;
            if (x > 6)
            {
                x = 0;
                y++;
            }
        }
    }

    public void PlayPickup()
    {
        audio_source.PlayOneShot(item_pickup);
    }

    public void PlayDrop()
    {
        audio_source.PlayOneShot(item_drop);
    }

    public void BuyVending1(Vector3 dropPosition) //Buy heart item
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Coin && item.amount >= 100)
            {
                item.amount -= 100;
                if (item.amount <= 0)
                {
                    inventory.RemoveItem(item);
                }
                Item heartItem = new Item { itemType = Item.ItemType.Heart, amount = 1 };
                ItemWorld itemWorld = ItemWorld.SpawnItemWorld(dropPosition + new Vector3(0, -1.4f, 0), heartItem);

                RefreshInventoryItems();
                return;
            }
        }
    }
    public void BuyVending2 (Vector3 dropPosition) //Buy apple item
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Coin && item.amount >= 25)
            {
                item.amount -= 25;
                if (item.amount == 0)
                {
                    inventory.RemoveItem(item);
                }
                Item appleItem = new Item { itemType = Item.ItemType.Apple, amount = 1 };
                ItemWorld itemWorld = ItemWorld.SpawnItemWorld(dropPosition + new Vector3(0, -1.4f, 0), appleItem);


                RefreshInventoryItems();
                return;
            }
        }
    }
}
