using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    //[SerializeField] GameObject explosion;
    [SerializeField] int dmg = 10;


    void OnCollisionEnter(Collision col)
    {
        // To do look into Effects particle system

        //GameObject explosionEffect = Instantiate(explosion, this.transform.position, Quaternion.identity);
        //Destroy(explosionEffect, 2f);
        if(col.gameObject.tag == "tank" )
        {
            col.gameObject.GetComponent<TankBehaivior>().TakeDamage(dmg);
        }
        else if(col.gameObject.tag == "player")
        {

        }

        Destroy(this.gameObject);
    }

    public int GetDmg()
    {
        return (dmg);
    }
}
