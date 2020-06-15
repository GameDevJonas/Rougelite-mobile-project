using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlPattern : MonoBehaviour
{
    public float rotationSpeed, shootFreq, shootTimer, bulletForce, duration;

    public GameObject bullet;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
        if (shootTimer <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            shootTimer = shootFreq;
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void ShootTimer()
    {

    }
}
