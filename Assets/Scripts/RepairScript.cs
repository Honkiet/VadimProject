using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage = -10;
    [SerializeField] float range = 4f;
    [SerializeField] float fireRate = 15;

    [SerializeField] Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        //{
        //    nextTimeToFire = Time.time + 1f / fireRate;
        //    Shoot();  
        //}

        if (Input.GetKeyDown("e") )
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            TankBehaivior tank = hit.transform.GetComponent<TankBehaivior>();
            if (tank != null)
            {
                tank.Heal();
            }

        }
    }
}
