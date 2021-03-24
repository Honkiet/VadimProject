using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TankBehaivior : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PatrolPath patrolPath;
    [SerializeField] Mover mover;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [SerializeField] HealthBar healthBar;

    [SerializeField] int maxHealth = 300;
    [SerializeField] int currentHealth;


    Vector3 guardPosition;

    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    public int currentWaypointIndex = 0;

    private void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        guardPosition = transform.position;
    }

    private void Update()
    {
        PatrolBehaviour();

        if(currentHealth < 40)
        {
            this.GetComponent<NavMeshAgent>().speed = 0;
        }
        else
        {
            this.GetComponent<NavMeshAgent>().speed = 3.5f;
        }
        
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition);
        }

        if (currentWaypointIndex == 5)
        {
            SceneManager.LoadScene(2);
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
        
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        healthBar.SetHealth(currentHealth);
        //if (currentHealth == 0)
        //{
            
        //}
    }

    public void Heal()
    {
        currentHealth = Mathf.Min(currentHealth + 10, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    //private void Die()
    //{
    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            TakeDamage(10);
        }
        if (collision.gameObject.tag == "goal")
        {
            SceneManager.LoadScene(2);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "goal")
    //    {
    //        SceneManager.LoadScene(2);
    //    }
    //}

}
