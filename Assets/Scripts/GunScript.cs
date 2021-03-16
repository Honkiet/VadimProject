
using System;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage = 10;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 15;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleflash;

    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        //{
        //    nextTimeToFire = Time.time + 1f / fireRate;
        //    Shoot();  
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Turret turret = hit.transform.GetComponent<Turret>();
            if(turret != null)
            {
                turret.TakeDamage(damage);
            }

        }
    }
}
