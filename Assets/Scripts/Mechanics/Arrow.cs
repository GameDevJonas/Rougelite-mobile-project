using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isParented;

    public PlayerStats stats;
    public int critRoll;
    public float critChance;
    public bool crit;

    void Awake()
    {
        isParented = false;
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        critRoll = Random.Range(0, 100);
        critChance = stats.CritChance.Value;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        critChance = stats.CritChance.Value;
    }

    public void CritCheck()
    {
        if (critRoll > critChance)
        {
            crit = false;
        }
        if (critRoll < critChance || critRoll == critChance)
        {
            crit = true;
        }
    }

    public void AddForce()
    {
        rb.AddRelativeForce(transform.up * Time.deltaTime * 450000);
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
        if (collision.tag != "Player" && collision.tag != "RoomRoot")
        {
            Debug.Log(collision.gameObject.name, collision.gameObject);
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (!isParented && collision.tag == "Enemy")
            {
                transform.SetParent(collision.gameObject.transform);
                this.enabled = false;
                isParented = false;
            }
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
