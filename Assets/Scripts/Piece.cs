using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] float visionDistance = 20.0f;
    [SerializeField] float visionAngle = 30.0f;
    [SerializeField] HealthBar healthBar;
    public int teamNumber;

    [SerializeField] float timebtw;

    //public float offset;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootPower = 400f;
   


    bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthBar.SetHealth(currentHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            TakeDamage(col.transform.GetComponent<Bullet1>().GetDmg());
        }
    }
    private void Die()
    {
        if (isDead) return;

        isDead = true;
    }

    public float GetVisionDistance()
    {
        return (visionDistance);
    }

    public float GetVisionAngle()
    {
        return (visionAngle);
    }

    private void Update()
    {



        //if (timebtw <= 0)
        //{


        //    Instantiate(projectile, FirePoint.position, transform.rotation);
        //    timebtw = startTimeBtw;

        //}

        //else
        //{
        //    timebtw -= Time.deltaTime;
        //}

    }


    private void Fire()
    {
        GameObject bullet = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * shootPower);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", timebtw, timebtw);
    }
}
