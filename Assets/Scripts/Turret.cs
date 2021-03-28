using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public Transform tank;
	public float speed = 1f;
	float accuracy = 1.0f;
	public float rotSpeed = 0.4f;
	[SerializeField] int maxHealth = 100;
	[SerializeField] int currentHealth;
	[SerializeField] float shootdistance = 5f;
	[SerializeField] HealthBar healthBar;
	private float nextTimeToFire = 0f;

	[SerializeField] ParticleSystem muzzleflash;
	public GameObject player;
	Animator anim;
	public GameObject bullet;
	public GameObject firepoint;

	// Use this for initialization
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void LateUpdate()
	{
        //	Vector3 lookAtGoal = new Vector3(tank.position.x,
        //									tank.position.y,
        //									tank.position.z);
        //	Vector3 direction = lookAtGoal - this.transform.position;

        //	this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //											Quaternion.LookRotation(direction),
        //											Time.deltaTime * rotSpeed);

        //       //if(Vector3.Distance(transform.position,lookAtGoal) > accuracy)
        //       //this.transform.Translate(0,0,speed*Time.deltaTime);

        //       if (InAttackRangeOfTank())
        //       {
        //		nextTimeToFire = Time.time + 1f / fireRate;
        //	  // Shoot(); 
        //	}
        if (tank)
        {
			anim.SetFloat("distance", Vector3.Distance(transform.position, tank.transform.position));
        }
		
	}


    public void TakeDamage(int amount)
    {
		currentHealth = Mathf.Max(currentHealth - amount, 0);
		healthBar.SetHealth(currentHealth);
		if (currentHealth == 0)
		{
			Die();
		}
	}

    private void Die()
    {
		Destroy(gameObject);
    }

	private bool InAttackRangeOfTank()
	{
		float distanceToPlayer = Vector3.Distance(tank.transform.position, transform.position);
		return distanceToPlayer < shootdistance;
	}

	void Fire()
	{
		muzzleflash.Play();
		GameObject b = Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
		b.GetComponent<Rigidbody>().AddForce(firepoint.transform.forward * 5000);
	}

	public void StopFiring()
	{
		CancelInvoke("Fire");
	}

	public void StartFiring()
	{
		InvokeRepeating("Fire", 0.5f, 0.5f);
	}

	// add code for picking the closes 
	public GameObject GetPlayer()
	{
		return player;
	}
}
