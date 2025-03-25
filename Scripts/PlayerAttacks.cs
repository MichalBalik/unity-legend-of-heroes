using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject fireBall;
    public Transform fireBallPoint;
    public float fireBallSpeed = 1000;

    public GameObject poisionWave;
    public Transform poisionWavePoint;
    public float poisionWaveSpeed = 1000;


    public void FireballAttack()
    {

        GameObject ball =Instantiate(fireBall, fireBallPoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(fireBallPoint.forward * fireBallSpeed);
    }

    public void PoisionAttack()
    {
        GameObject posionWave = Instantiate(poisionWave, poisionWavePoint.position, Quaternion.identity);

        //Spravne otacanie wave
       posionWave.transform.LookAt(transform.position + posionWave.GetComponent<Rigidbody>().velocity);



        posionWave.GetComponent<Rigidbody>().AddForce(poisionWavePoint.forward * 1000);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("zasah skript playerAttack");
           
        }
    }
}
