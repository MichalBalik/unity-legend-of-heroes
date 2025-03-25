using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class PlayerInventory : MonoBehaviour
{
    
    public int NumberOfDiamonds { get; set; }
    public int health { get;  set; }
    public int healthPotion { get;  set; }
    public int shieldPotion { get; set; }

    private InventoryUI inventoryUi;
    
    void Start()
    {
        
        this.health = 100;
        this.healthPotion = 2;
        this.shieldPotion = 2;
        inventoryUi = GetComponent<InventoryUI>();
        inventoryUi.UpdateUI(this);
    }
        public void AddDiamond()
    {
        this.NumberOfDiamonds++;
        inventoryUi.UpdateUI(this);

    }
  
    public void AddHealthPotion()
    {
        healthPotion++;
        inventoryUi.UpdateUI(this);
    }
    public void AddShieldPotion()
    {
        shieldPotion++;
        inventoryUi.UpdateUI(this);
    }
    public void DrinkPotion(int type)
    {
        
        if(type == 1 && healthPotion >0 && health <100)
        {

            this.health += 20;
            healthPotion--;
            if (health > 100)
            {
                this.health = 100;
            }
        }
        else if (type == 2 && shieldPotion > 0)
        {
            shieldPotion--;
        }
        inventoryUi.UpdateUI(this);
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if(health <= 0)
        {

            Debug.Log("ZOMREL SI"); 
        }
        inventoryUi.UpdateUI(this);
    }
    public void UpdateUI(PlayerInventory PI)
    {
        inventoryUi.UpdateUI(PI);
    }
    public void UpdateHealtUI(PlayerInventory PI)
    {
        inventoryUi.UpdateUI(PI);
    }
    public void UpdateUIPanels(bool deadMessagePanelState, bool actionBarPanelState, bool QuestBarPanelState, bool profilePictureState, bool healthBarState, bool pauseMenuState)
    {
        inventoryUi.UpdateUIPanels(deadMessagePanelState, actionBarPanelState, QuestBarPanelState, profilePictureState, healthBarState, pauseMenuState);
    }

    public void setHealth(int healthPoints)
    {
        health = healthPoints;
        
    }
    public void UpdateQuest(string questText)
    {
        inventoryUi.UpdateQuest(questText);
    }
    public void setEndScenePanelState(bool hodnota)
    {
        inventoryUi.setEndScenePanelState(hodnota);
    }

}
