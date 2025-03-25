using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWave : MonoBehaviour
{

    public float damageAmount = 5;
    public float poisionDuration = 5;
    public float destroyTime = 100;
    public AudioClip hitSound;


    private AudioSource audios;

    void Start()
    {
        audios = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }


    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {

        }
        if (other.tag == "Player")
        {



        }
        if (other.tag == "Enemy")
        {
            audios.PlayOneShot(hitSound);
            other.GetComponent<Enemy>().PoisonAttack(damageAmount,poisionDuration);
            Debug.Log("Zasah Enemaka v poison wave skript poison Wave");

        }
        if (other.tag == "EnemySentry")
        {
            audios.PlayOneShot(hitSound);
            other.GetComponent<EnemySentry>().PoisonAttack(damageAmount, poisionDuration);
            Debug.Log("Zasah Enemaka v poison wave skript poison Wave");

        }
        if (other.tag == "EnemyBoomer")
        {
            audios.PlayOneShot(hitSound);
            other.GetComponent<EnemyBoomer>().PoisonAttack(damageAmount, poisionDuration);
            Debug.Log("Zasah Enemaka v poison wave skript poison Wave");

        }
    }
}
