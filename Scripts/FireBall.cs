using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    
    public int damageAmount = 40;
    public float destroyTime = 5;
    public AudioClip hitSound;
    
    

    private AudioSource audios;
    private PlayerController playerController;
    void Start()
    {
        audios = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        Destroy(gameObject, destroyTime);

    }
    


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            

            
            other.GetComponent<Obstacle>().TakeDamage(damageAmount);
            
            Destroy(gameObject);
           
        }


        if (other.tag == "Player")
        {

            other.GetComponent<PlayerController>().TakeDamage(30);
            Debug.Log("Zasah playera v FIREBALL");

        }
        if (other.tag == "Shield")
        {

            
           Destroy(this);
            Debug.Log("Yasah Shieldu nicim fireball");

        }
        if (other.tag == "Enemy")
        {
            playerController.playsound("fireImpact");
            
            other.GetComponent<Enemy>().TakeDamage(50);
            Debug.Log("Zasah Enemaka v FIREBALL");

        }
        if (other.tag == "EnemySentry")
        {
            
            playerController.playsound("fireImpact");
            other.GetComponent<EnemySentry>().TakeDamage(50);
            Debug.Log("Zasah Enemaka v FIREBALL");

        }
        if (other.tag == "EnemyBoomer")
        {
            playerController.playsound("woodenEnemyHit");
            
            other.GetComponent<EnemyBoomer>().TakeDamage(25);
            Debug.Log("Zasah Enemaka v FIREBALL");

        }
    }

}
