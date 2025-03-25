using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Animator animator;
    private CharacterController characterController;
    private PlayerInventory playerInventory;
    private float health;

    private GameObject endpanel;
    private GameContoller gameContoller;
    private GameObject player;
    private bool waitingForRespawn;
    string InventorySavePath => Application.persistentDataPath + "/inventory.save";

    private PlayerAttacks pa;
    private float timerOnFire = 0;
    public float enemyKiled { get; set; }
    public float enemyTotal { get; set; }
    
    [Header("Sounds")]
    private AudioSource audios;
    public AudioClip hitSound;
    public AudioClip fireCastSound;
    public AudioClip thunder;
    public AudioClip drinkPotion1;
    public AudioClip drinkPotion2;
    public AudioClip gainShieldSound;
    public AudioClip itemPickupSound;
    public AudioClip woodenEnemyHitSound;
    public AudioClip fireImpactSound;
    public AudioClip thunderSound;
    public AudioClip explosionSound;
    public AudioClip poisionSound;

    // Start is called before the first frame update


    
    private float thunderSpellStart = 0f;
    private float thunderSpellCooldown = 5f;


    
    public GameObject poisionWave;
    public Transform poisionWavePosition;



    [Header("Ability 1")]
    public Image Ability1Image;
    public TextMeshProUGUI ability1Text;
    public float ability1Cooldown = 5;

    [Header("Ability 2")]
    public Image Ability2Image;
    public TextMeshProUGUI ability2Text;
    public float ability2Cooldown = 10;

    [Header("Ability 3")]
    public Image Ability3Image;
    public TextMeshProUGUI ability3Text;
    public float ability3Cooldown = 3;

    private bool isAbility1OnCooldown = false;
    private bool isAbility2OnCooldown = false;
    private bool isAbility3OnCooldown = false;




    private float currentAbility1Cooldown;
    private float currentAbility2Cooldown;
    private float currentAbility3Cooldown;


    [Header("Ability Shield")]
    public GameObject shieldSphere;
    public Image ShieldPotionImage;
    public TextMeshProUGUI ShieldPotionText;
    public float ShieldPotionCooldown = 5;
    private bool isShieldOnCooldown = false;
    private float currentShieldCooldown;

    //Interaktivne itemy
    private bool triggerState = false;
    private GameObject objektNaInterakciu;
    private bool isOnFire = false;
    public bool getAbility1State()
    {
        return isAbility1OnCooldown;
    }

    public void setIsAbility1OnCooldown(bool state)
    {
        isAbility1OnCooldown = state;
    }

    public void setCurrentAbility1Cooldown()
    {
        currentAbility1Cooldown = ability1Cooldown;
    }
    
    
    public bool getAbility2State()
    {
        return isAbility2OnCooldown;
    }

    public void setIsAbility2OnCooldown(bool state)
    {
        isAbility2OnCooldown = state;
    }

    public void setCurrentAbility2Cooldown()
    {
        currentAbility2Cooldown = ability2Cooldown;
    }





    public bool getAbility3State()
    {
        return isAbility3OnCooldown;
    }

    public void setIsAbility3OnCooldown(bool state)
    {
        isAbility3OnCooldown = state;
    }

    public void setCurrentAbility3Cooldown()
    {
        currentAbility3Cooldown = ability3Cooldown;
    }


    public bool getShieldState()
    {
        return isShieldOnCooldown;
    }

    public void setShieldOnCooldown(bool state)
    {
        isShieldOnCooldown = state;
    }

    public void setCurrentShieldCooldown()
    {
        currentShieldCooldown = ShieldPotionCooldown;
    }

    void Start()
    {
        //COOLDOWN MECHANICS
        Ability1Image.fillAmount = 0;
        ability1Text.text = "";


        Ability2Image.fillAmount = 0;
        ability2Text.text = "";

        Ability3Image.fillAmount = 0;
        ability3Text.text = "";


        ShieldPotionImage.fillAmount = 0;
        ShieldPotionText.text = "";




        animator = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();
        //shieldSphere = GameObject.FindGameObjectWithTag("Shield");
        playerInventory = GetComponent<PlayerInventory>();
        gameContoller = GetComponent<GameContoller>();
        pa = GetComponent<PlayerAttacks>();
        this.health = playerInventory.health;
        this.health = 20;
        waitingForRespawn = false;

        audios = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();



        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemiesSentry = GameObject.FindGameObjectsWithTag("EnemySentry");
        GameObject[] EnemyBoomer = GameObject.FindGameObjectsWithTag("EnemyBoomer");
        
        enemyTotal = enemies.Length;
        enemyTotal += enemiesSentry.Length;
        enemyTotal += EnemyBoomer.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -3f)
        {
            gameContoller.PauseGame();
            
            Debug.Log("Vypadol si mimo mapu");
 
            playerInventory.UpdateUIPanels(true, false, false, false, false, false);
        }

        if (isOnFire)
        {
            timerOnFire += Time.deltaTime;


            if (timerOnFire > 1.0f)
            {

                TakeDamage(1);
                timerOnFire = 0.0f;
               

                Debug.Log("Burning" + health);
            }




        }



        abilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1OnCooldown, Ability1Image, ability1Text);
        abilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2OnCooldown, Ability2Image, ability2Text);
        abilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3OnCooldown, Ability3Image, ability3Text);
        abilityCooldown(ref currentShieldCooldown, ShieldPotionCooldown, ref isShieldOnCooldown, ShieldPotionImage, ShieldPotionText);

        if (!isShieldOnCooldown)
        {
            shieldSphere.SetActive(false);
        }




        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude);




        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1) )
        {

            if(!isAbility1OnCooldown)
            {



                isAbility1OnCooldown = true;
                currentAbility1Cooldown = ability1Cooldown;

                
                playsound("ability1");
                animator.SetTrigger("fireBallAttack");
                


            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (!isAbility2OnCooldown)
            {

                //animator.SetTrigger("fireBallAttack");
                playsound("ability2");
                thunderAbility();
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {

            
            





            if (!isAbility3OnCooldown)
            {
                animator.SetTrigger("poisionAttack");


                poisonAbility();

            }


        }






        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            SaveInventory();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            LoadInventory();
            
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {


            
            playsound("potion1");
            playerInventory.DrinkPotion(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {


            if (!isShieldOnCooldown && playerInventory.shieldPotion > 0 )
            {
                aktivujShield();
                playsound("potion2");
                playerInventory.DrinkPotion(2);
                playsound("gainShield");
            }
            

        }
        if (Input.GetKeyDown(KeyCode.E) && triggerState)
        {
            if (objektNaInterakciu.tag == "Crystal")
            {
                Debug.Log("znic objekt" + objektNaInterakciu.name, objektNaInterakciu);
                triggerState = false;
                Destroy(objektNaInterakciu);
                Debug.Log("Zberam Kristal");
                playerInventory.AddShieldPotion();
            }

  


        }



        this.health = playerInventory.health;
        
        if (health <= 0 )
        {

                gameContoller.PauseGame();

            Debug.Log("ZOMREL SI PAUZUJEM HRU");

            playerInventory.UpdateUIPanels(true, false, false, false,false,false);

            
        }

    }
 


    public void playsound(string typeOfSound)
    {

        switch (typeOfSound)
        {
            case "ability1":
                audios.PlayOneShot(fireCastSound);
                break;
            case "ability2":
                audios.PlayOneShot(thunderSound);
                break;
            case "potion1":
                audios.PlayOneShot(drinkPotion1);
                break;
            case "potion2":
                audios.PlayOneShot(drinkPotion2);
                break;
            case "gainShield":
                audios.PlayOneShot(gainShieldSound);
                break;
            case "playerHit":
                audios.PlayOneShot(hitSound);
                break;
            case "itemPickup":
                audios.PlayOneShot(itemPickupSound);
                break;
            case "woodenEnemyHit":
                audios.PlayOneShot(woodenEnemyHitSound);
                break;
            case "fireImpact":
                audios.PlayOneShot(fireImpactSound);
                break;
            case "explosion":
                audios.PlayOneShot(explosionSound);
                break;
            case "poision":
                audios.PlayOneShot(poisionSound);
                break;

        }
    }



    private void abilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, TextMeshProUGUI skillText) {
        if (isCooldown)
        {

            currentCooldown -= Time.deltaTime;

            if(currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                //skillImage.enabled = false;

                if (skillImage != null)
                {

                    skillImage.fillAmount = 0f;

                }
                if(skillText != null)
                {
                    skillText.text = "";

                }

            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if(skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();

                }
            }
        }
    
    
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBall")
        {
            Destroy(other.gameObject);

            Debug.Log("ZASAH v player contoleri");
            
        }

        else if (other.tag == "Hranica")
        {
            UpdateQuest();

        }
        else if (other.tag == "Hearth")
        {
            playsound("itemPickup");
        }

        else if (other.tag == "Crystal")
        {
            objektNaInterakciu = other.gameObject;
            triggerState = true;
        }
        else if(other.tag == "Fire")
        {
            isOnFire = true;
        }
        else if (other.tag== "PortalFinal")
        {

            
            gameContoller.PauseGame();
            playerInventory.setEndScenePanelState(true);
            characterController.enabled = false;
            characterController.transform.position = new Vector3(0f, 0f, 0f);
            characterController.enabled = true;
        }



    }

    private void OnTriggerExit(Collider other)
    {
        triggerState = false;
        Debug.Log("Nie si v radiuse" + other.gameObject.name, other.gameObject);
        objektNaInterakciu = null;

        if(other.tag== "Fire")
        {
            isOnFire = false;
        }
    }

    public void poisonAbility()
    {
        if (!isAbility3OnCooldown)
        {
            playsound("poision");
            isAbility3OnCooldown = true;
            currentAbility3Cooldown = ability3Cooldown;


        }

    }

    public void aktivujShield()
    {
        if (!isShieldOnCooldown && playerInventory.shieldPotion > 0)
        {
            isShieldOnCooldown = true;
            currentShieldCooldown = ShieldPotionCooldown;
            shieldSphere.SetActive(true);

        }
    }



    public void thunderAbility()
    {
        if (!isAbility2OnCooldown)
        {

            isAbility2OnCooldown = true;
        currentAbility2Cooldown = ability2Cooldown;
            
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            if (Vector3.Distance(e.transform.position, transform.position) < 10)
            {


                    thunderSpellStart = Time.time;

                    
                    Debug.Log("Thunder ability Player controler");




                    
                    
                    e.TakeDamage(15); // Hit the enemy for example

                //}
            }
            }

            foreach (EnemySentry e in FindObjectsOfType<EnemySentry>())
            {
                if (Vector3.Distance(e.transform.position, transform.position) < 10)
                {


                    thunderSpellStart = Time.time;

                    
                    Debug.Log("Thunder ability Player controler");





                    
                    e.TakeDamage(40); // Hit the enemy for example

                    
                }
            }
            foreach (EnemyBoomer e in FindObjectsOfType<EnemyBoomer>())
            {
                if (Vector3.Distance(e.transform.position, transform.position) < 10)
                {


                    thunderSpellStart = Time.time;

                   
                    Debug.Log("Thunder ability Player controler");





                    
                    e.TakeDamage(50); // Hit the enemy for example

                    //}
                }
            }



        }
    }

    private void OnColisionEnter(Collider other)
    {

        if (other.tag == "FireBall")
        {
            
            Debug.Log( "zasah hrac od " +other.gameObject.tag);
            Debug.Log(this.gameObject.tag);

            }


        }








    public void TakeDamage (int damage)
    {
        if (!shieldSphere.activeSelf)
        {
            playsound("playerHit");
            playerInventory.TakeDamage(damage);
        }
        

    }
    public void RespawnPlayer()
    {
        playerInventory.setHealth(100);
        gameContoller.ResumeGame();
        playerInventory.UpdateUIPanels(false, true, true, true, true,false);
        playerInventory.UpdateHealtUI(playerInventory);

        characterController.enabled = false;
        characterController.transform.position = new Vector3(0f, 1f, 33f);
        characterController.enabled = true;
        UpdateQuest();
        gameContoller.ResetovanieLevelu();
        
            
        


    }
    public void UpdateQuest() {
        if(enemyKiled == enemyTotal) { 
            playerInventory.UpdateQuest("Destroy Crystal with Fireball");
        }
        else
        {
            playerInventory.UpdateQuest("Clear Dungeon " + enemyKiled + "/" + enemyTotal);
        }
        
    }



    [System.Serializable]
    public class SaveData
    {
        public int numberOfDiamonds;
        public int health;
        public int healthPotion;
        public int shieldPotion;

        public SaveData(int numberOfDiamondsp, int healthp, int healthPotionp, int numberOfShieldPotions)
        {
            numberOfDiamonds = numberOfDiamondsp;
            health = healthp;
            healthPotion = healthPotionp;
            shieldPotion = numberOfShieldPotions;
        }
    }


   public void SaveInventory()
    {
        SaveData sd = new SaveData(playerInventory.NumberOfDiamonds, playerInventory.health, playerInventory.healthPotion,playerInventory.shieldPotion);
        Debug.Log($"Saving inventory to file @ {InventorySavePath}");
        var stream = new FileStream(InventorySavePath, FileMode.Create, FileAccess.Write);
        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize(stream, sd);
        stream.Close();
    }

    void LoadInventory()
    {
        Debug.Log($"Loading inventory from file @ {InventorySavePath}");
        if (File.Exists(InventorySavePath))
        {

            var stream = new FileStream(InventorySavePath, FileMode.Open, FileAccess.Read);
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            SaveData sd = (SaveData)formatter.Deserialize(stream);
            stream.Close();

            playerInventory.NumberOfDiamonds = sd.numberOfDiamonds;
            playerInventory.health = sd.health;
            playerInventory.healthPotion = sd.healthPotion;
            playerInventory.shieldPotion = sd.shieldPotion;
            playerInventory.UpdateUI(playerInventory);
        }
        else { 
            Debug.LogError("Save file does not exist!"); 
        }
         
    }
}
