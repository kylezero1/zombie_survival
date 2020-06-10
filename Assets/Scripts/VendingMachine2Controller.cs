using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine2Controller : MonoBehaviour
{
    public UI_Inventory ui_inventory;

    private Transform vendingTransform;

    // Start is called before the first frame update
    void Start()
    {
        ui_inventory = GameObject.Find("UI_Inventory").GetComponent<UI_Inventory>();
        vendingTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ui_inventory.BuyVending2(vendingTransform.position);
        }
    }
}
