using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
 
    private AudioSource auds;
    
    private void Start()
    {
        auds = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        InventoryUI inventoryUi = other.GetComponent<InventoryUI>();
        if(playerInventory != null)
        {
            Debug.Log(other.gameObject.tag);
            Debug.Log(this.gameObject.tag);
            
            Destroy(gameObject);
            switch (this.gameObject.tag)
            {
                case "Coin":
                    playerInventory.AddDiamond();
                    Debug.Log("Coin added" + this.gameObject.tag);
                    break;
                case "Hearth":
                    playerInventory.AddHealthPotion();
                    Debug.Log("Posion added added" + this.gameObject.tag);
                    break;
                
            }

            inventoryUi.UpdateUI(playerInventory);
           
        }
    }
}
