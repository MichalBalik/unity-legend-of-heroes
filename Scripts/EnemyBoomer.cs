using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomer : MonoBehaviour
{
    private Transform player;
    private PlayerController pc;
    private float distance;
    public float moveSpeed;
    public float howClose;
    new Rigidbody rigidbody;
    private Animator animator;

    public float fireBallSpeed = 500;
    private float fireSpellStart = 0f;
    private float fireSpellCooldown = 2f;
    public int health = 10;

    public GameObject explosion;
    private float movementTimeStart = 0f;

    private float movementTime = 5f;
    private Transform myTransform;



    public GameObject healtItem;
    private bool uzDropolSrdce = false;

    private bool isPoisioned = false;
    private float timer = 0;
    private float poisonDuration = 5f;
    private float poisonDamage = 5f;

    private int poisionSecDuration = 5;



    private float TimeLeft;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {



        Vector3 targetPosition = player.position;
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, targetPosition);

        if (distance <= howClose && health > 0)
        {
            //animator.SetBool("isMoving", true);
        }


        if (distance > 1 && distance <= howClose && health > 0)
        {
            //animator.SetBool("isMoving", false);
            Vector3 directionOfTravel = targetPosition - currentPosition;
            directionOfTravel.Normalize();

            animator.SetBool("isMoving", true);




            rigidbody.MovePosition(currentPosition + (directionOfTravel * moveSpeed * Time.deltaTime));

            if (distance < 1.5)
            {
                Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);


                pc.TakeDamage(30);
                pc.playsound("explosion");
                Die();

                Debug.Log("BOOOM");
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

    }
    // Update is called once per frame
    void Update()
    {
       




        if (isPoisioned)
        {
            timer += Time.deltaTime;


            if (timer > 1.0f)
            {

                this.health -= (int)poisonDamage;
                timer = 0.0f;
                poisonDuration = poisonDuration - 1f;
                
                Debug.Log(timer + "poison Wave timer" + health + "zdravie");
            }


            if (poisonDuration <= 0)
            {
                isPoisioned = false;
                timer = 0;
            }


            if (health <= 0)
            {
                //50 % percentna sanca na spavnutie itemu
                if (!uzDropolSrdce && Random.value <= 0.5)
                {
                    
                    uzDropolSrdce = true;
                    Instantiate(healtItem, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);
                }

                
                Die();

            }


        }


        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= howClose && health > 0)
        {
            transform.LookAt(player);






            if (distance <= howClose)
            {
                

                if (Time.time > fireSpellStart + fireSpellCooldown)
                {
                    fireSpellStart = Time.time;
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                   
                    Debug.Log("strielam");


                }


            }
            
        }
        else
        {

            int x = 0;
            int z = 0;
            if (Time.time > movementTimeStart + fireSpellCooldown)
            {
                movementTimeStart = Time.time;
                float moveDirection = Random.Range(0, 4);
                //Debug.Log(moveDirection);

                switch (moveDirection)
                {
                    case 0:
                        // MOVE FORWARD
                        z = 1;
                        break;
                    case 1:
                        //MOVE BACWARD
                        z = -1;
                        break;
                    case 2:
                        //MOVE LEFT
                        x = -1;
                        break;
                    case 3:
                        //MOVE RIGHT
                        x = 1;
                        break;
                    default:
                        // code block
                        break;
                }
            }



            Vector3 tempVect = new Vector3(x, 0, z);
            tempVect = tempVect.normalized * moveSpeed * Time.deltaTime;



            if (tempVect != Vector3.zero)
            {

                Quaternion toRotation = Quaternion.LookRotation(tempVect, Vector3.up);


                Vector3 tempVect2 = new Vector3(0, 0, 1);

                
                float increment = 700 * Time.fixedDeltaTime;
               

            }

        }


    }
    public void TakeDamage(int damage)
    {


        health -= damage;

        player.GetComponent<PlayerController>().playsound("woodenEnemyHit");




        Debug.Log("curent health" + health);
        if (health <= 0)
        {
            //50 % percentna sanca na spavnutie itemu
            if (Random.value <= 0.5)
            {

                Instantiate(healtItem, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);
            }


            Die();

        }
    }
    public void PoisonAttack(float poisionDamage, float poisonDuration)
    {
        this.isPoisioned = true;
        this.poisonDuration = poisonDuration;
        this.poisonDamage = poisionDamage;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBall")
        {
            Destroy(other.gameObject);
            //player.GetComponent<PlayerController>().playsound("woodenEnemyHit");
            Debug.Log("ZASAH ENEMAKA");
        }
        if (other.tag == "PoisonWave")
        {
            //Destroy(other.gameObject);
            Debug.Log("ZASAH ENEMAKA Poison wavevov Skript Enemy");
        }

    }
    public void Die()
    {
        Debug.Log("Dead");
        //asi zle
        //animator.Play("die");

        player.GetComponent<PlayerController>().enemyKiled = player.GetComponent<PlayerController>().enemyKiled + 1;
        player.GetComponent<PlayerController>().UpdateQuest();

        //animator.SetTrigger("isDead");
        //float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        Destroy(this.gameObject);

    }
}
