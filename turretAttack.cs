using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAttack : MonoBehaviour
{

    public GameObject projectile;
    private bool canFire = false;
    public float fireRate = 1f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + fireRate;
    }

    void Attack()
    {
        if(canFire == true)
        {
            GameObject newProjectile = Instantiate(projectile, this.transform.position, this.transform.rotation);
            Rigidbody instaProjectile = newProjectile.GetComponent<Rigidbody>();
            instaProjectile.AddForce(transform.forward * 100);
            
        }
        canFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            canFire = true;
            timer = Time.time + fireRate;
        }
    }

}
