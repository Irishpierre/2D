using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Enumeration; set of names for values
    enum ItemType { Coin, Health, Ammo, InventoryItem }
    [SerializeField] private ItemType itemType;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Trigger 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            if (itemType == ItemType.Coin)
            {
                Player.Instance.coinsCollected += 1;
            }
            else if (itemType == ItemType.Health)
            {
                if (Player.Instance.health < 100)
                {
                    Player.Instance.health += 1;
                }
            }
            else if (itemType == ItemType.Ammo)
            {

            }
            else if (itemType == ItemType.InventoryItem)
            {
                Player.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            else
            {

            }

            Player.Instance.UpdateUI();
            //Destroy after collect
            Destroy(gameObject);

        }
    }
}
