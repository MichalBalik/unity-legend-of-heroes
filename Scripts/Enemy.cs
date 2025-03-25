using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private float distance;
    public float moveSpeed;
    public float howClose;
    new Rigidbody rigidbody;
    private Animator animator;
    public GameObject fireBall;
    public Transform fireBallPoint;
    public float fireBallSpeed = 500;
    private float fireSpellStart = 0f;
    private float fireSpellCooldown = 1.25f;
    public int health = 100;

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
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        myTransform = transform;
    }


    void Update()
    {
       




        if (isPoisioned)
        {
            timer += Time.deltaTime;

            
            if (timer >  1.0f)
            {
                
                this.health -= (int)poisonDamage;
                timer = 0.0f;
                poisonDuration = poisonDuration - 1f; 

                Debug.Log(timer + "poison Wave timer" + health + "zdravie");
            }


            if(poisonDuration <= 0)
            {
                isPoisioned = false;
                timer = 0;
            }


            if (health <= 0 )
            {
                //50 % percentna sanca na spavnutie itemu
                if (!uzDropolSrdce &&Random.value <= 0.5)
                {

                    uzDropolSrdce = true;
                    Instantiate(healtItem, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);
                }

                Die();

            }


        }
        
        
            
        






        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= howClose && health >0) {
            transform.LookAt(player);



            if (distance <= howClose)
            {


                if (Time.time > fireSpellStart + fireSpellCooldown)
                {
                    fireSpellStart = Time.time;
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;

                    Debug.Log("strielam");
                    animator.SetTrigger("fireBallAttack");
                    GameObject ball = Instantiate(fireBall, fireBallPoint.position, Quaternion.identity);
                    ball.GetComponent<Rigidbody>().AddForce(fireBallPoint.forward * fireBallSpeed);

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


        



        Debug.Log("curent health" + health);
        if (health <= 0)
        {
            //50 % percentna sanca na spavnutie itemu
            if (Random.value <= 0.5) {

                Instantiate(healtItem, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);
            }


            Die();

        }
    }
    public void PoisonAttack( float poisionDamage,float poisonDuration)
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
            Debug.Log("ZASAH ENEMAKA");
        }
        if (other.tag == "PoisonWave")
        {

            Debug.Log("ZASAH ENEMAKA Poison wavevov Skript Enemy");
        }

    }
    public void Die()
    {
        Debug.Log("Dead");
        //animator.SetTrigger("isDead");
        player.GetComponent<PlayerController>().enemyKiled = player.GetComponent<PlayerController>().enemyKiled + 1;
        player.GetComponent<PlayerController>().UpdateQuest();
        //animator.Play("die");
        //float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        //Debug.Log(animTime);
       // Destroy(this.gameObject,animTime);
        Destroy(this.gameObject);


    }
}
