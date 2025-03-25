using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler


{


    private GameObject player;
    private Animator animator;
    private PlayerController pc;

    void Start()
    {

        player=GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<PlayerController>().GetComponent<Animator>();
        pc = player.GetComponent<PlayerController>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }


    public void IwasClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name == "Health") {

            
            player.GetComponent<PlayerInventory>().DrinkPotion(1);
            
        }
        if (name == "FireAbility dark" && !player.GetComponent<PlayerController>().getAbility1State())
        {
            
                //FireAbility
            player.GetComponent<PlayerController>().setIsAbility1OnCooldown(true);
            player.GetComponent<PlayerController>().setCurrentAbility1Cooldown();

            Debug.Log("Clickol si fireballAction");
            animator.SetTrigger("fireBallAttack");


        }

        if (name == "LightningAbility Dark" && !player.GetComponent<PlayerController>().getAbility2State())
        {

            //FireAbility
            player.GetComponent<PlayerController>().setIsAbility2OnCooldown(true);
            player.GetComponent<PlayerController>().setCurrentAbility2Cooldown();
            player.GetComponent<PlayerController>().thunderAbility();
            pc.playsound("ability2");

            Debug.Log("Clickol si lightning");


        }

        if (name == "Poison Dark" && !player.GetComponent<PlayerController>().getAbility3State())
        {


            player.GetComponent<PlayerController>().poisonAbility();


            Debug.Log("Clickol si poison Dark");


        }

        if (name == "Shield Dark" && !player.GetComponent<PlayerController>().getShieldState())
        {



            player.GetComponent<PlayerController>().aktivujShield();
            player.GetComponent<PlayerInventory>().DrinkPotion(2);


            Debug.Log("Clickol si shield dark v ClickButton");


        }





    }



  


    void Update()
    {
        
    }
}
