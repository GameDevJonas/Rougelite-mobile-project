using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletForce;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
