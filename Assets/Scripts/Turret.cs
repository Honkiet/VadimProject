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
	[SerializeField] float health = 50f;
	[SerializeField] float shootdistance = 5f;
	[SerializeField] float fireRate = 15;
	private float nextTimeToFire = 0f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 lookAtGoal = new Vector3(tank.position.x,
										tank.position.y,
										tank.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;

		this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
												Quaternion.LookRotation(direction),
												Time.deltaTime * rotSpeed);

        //if(Vector3.Distance(transform.position,lookAtGoal) > accuracy)
        //this.transform.Translate(0,0,speed*Time.deltaTime);

        if (InAttackRangeOfTank())
        {
			nextTimeToFire = Time.time + 1f / fireRate;
		   Shoot(); 
		}
	}

    private void Shoot()
    {
        
    }

    public void TakeDamage(float amount)
    {
		health -= amount;
		if(health<= 0f)
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
}
