using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {


        if (this.tag == "FinalCoruptedCrystal" )
        {


        }
        else
        {
            health -= damage;
            Debug.Log("curent health" + health);
            if (health <= 0)
            {
                
                Die();

            }
        }


        



    }
    public float getNumberOfEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemiesSentry = GameObject.FindGameObjectsWithTag("EnemySentry");
        GameObject[] EnemyBoomer = GameObject.FindGameObjectsWithTag("EnemyBoomer");
        float enemiesLeft = enemies.Length;
        enemiesLeft += enemiesSentry.Length;
        enemiesLeft += EnemyBoomer.Length;
        return enemiesLeft;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBall" &&this.tag == "FinalCoruptedCrystal")
        {
            float enemies = getNumberOfEnemies();

            Destroy(other.gameObject);

            if (enemies == 0) {

                Debug.Log("zasiahol si crystal");
                Destroy(this.gameObject);
               
                GameObject FinalPortal = GameObject.FindGameObjectWithTag("PortalFinal");
                FinalPortal.transform.position = new Vector3(this.transform.position.x, 0.03f, this.transform.position.z);
            }
            else
            {
                Debug.Log("ENEMIES REMAINING " + enemies);
            }



            
        }
    }
    public void Die() {
        Debug.Log("Dead");
        Destroy(this.gameObject);

    }
}
