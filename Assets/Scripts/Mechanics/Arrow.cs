using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isParented;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isParented = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddForce()
    {
        rb.AddRelativeForce(transform.up * Time.deltaTime * 400000);
    }

    public void ShootyShoot(string dir)
    {
        if (dir == "UL")
        {
            transform.Rotate(0, 0, 30);
        }
        else if (dir == "U")
        {
            transform.Rotate(0, 0, 0);
        }
        else if (dir == "UR")
        {
            transform.Rotate(0, 0, -30);
        }
        else if (dir == "R")
        {
            transform.Rotate(0, 0, -90);
        }
        else if (dir == "DR")
        {
            transform.Rotate(0, 0, -120);
        }
        else if (dir == "D")
        {
            transform.Rotate(0, 0, -180);
        }
        else if (dir == "DL")
        {
            transform.Rotate(0, 0, 120);
        }
        else if (dir == "L")
        {
            transform.Rotate(0, 0, 90);
        }
        AddForce();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (!isParented)
            {
                transform.SetParent(collision.gameObject.transform);
                isParented = false;
            }
        }
    }
}
