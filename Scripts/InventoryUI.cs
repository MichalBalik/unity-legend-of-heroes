using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI diamondText; 
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI healthPotionNumber;
    public TextMeshProUGUI shieldPotionNumber;
    public TextMeshProUGUI questBarText;
    public TextMeshProUGUI storyAndEndText;
    public GameObject deadMessagePanel;
    public GameObject endScenePanel;
    public GameObject actionBarPanel;
    public GameObject QuestBarPanel;
    public GameObject profilePicture;
    public GameObject healtBar;
    public GameObject PauseMenuPanel;
    public Slider healthBarSlider;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        questBarText.text =("-");
    }

    public void UpdateUI(PlayerInventory playerInventory)
    {
        diamondText.text = playerInventory.NumberOfDiamonds.ToString() + " Coins";
        healthText.text = playerInventory.health.ToString() + " Lives";
        healthPotionNumber.text = playerInventory.healthPotion.ToString();
        healthBarSlider.value = playerInventory.health;
        shieldPotionNumber.text = playerInventory.shieldPotion.ToString();
    }

    public void UpdateQuest(string questText)
    {
        this.questBarText.text = questText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIPanels(bool deadMessagePanelState, bool actionBarPanelState, bool QuestBarPanelState, bool profilePictureState, bool healthBarState,bool pauseMenuState)
    {
        //UpdateUI();
        deadMessagePanel.SetActive(deadMessagePanelState);
        actionBarPanel.SetActive(actionBarPanelState);
        QuestBarPanel.SetActive(QuestBarPanelState);
        profilePicture.SetActive(profilePictureState);
        healtBar.SetActive(healthBarState);
        PauseMenuPanel.SetActive(pauseMenuState);
    }
    public void setEndScenePanelState(bool hodnota)
    {
        endScenePanel.SetActive(hodnota);
    }
}
