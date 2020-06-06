using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isParented;

    public PlayerStats stats;
    public GameObject fire;
    public int critRoll;
    public float critChance;
    public bool crit;
    public bool burn = true;

    void Awake()
    {
        isParented = false;
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        critRoll = Random.Range(1, 101);
        critChance = stats.CritChance.Value;
        CritCheck();
        //Destroy(gameObject, 5f);
        //Debug.Log(critRoll);
        //Debug.Log(critChance);
    }

    void Update()
    {
        if (stats.FireArrows.Value > 0 && burn == true && GetComponent<Collider2D>().enabled == true)
        {
            fire.SetActive(true);
        }
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
        rb.AddRelativeForce(transform.up * 6500);
    }

    public void ShootyShoot(string dir, float HasEye)
    {
        if (dir == "UL" && HasEye <= 0)
        {
            //transform.Rotate(0, 0, 45);
            transform.Rotate(0, 0, 0);
        }
        else if (dir == "U" && HasEye <= 0)
        {
            transform.Rotate(0, 0, 0);
        }
        else if (dir == "UR" && HasEye <= 0)
        {
            //transform.Rotate(0, 0, -45);
            transform.Rotate(0, 0, 0);
        }
        else if (dir == "R" && HasEye <= 0)
        {
            transform.Rotate(0, 0, -90);
        }
        else if (dir == "DR" && HasEye <= 0)
        {
            //transform.Rotate(0, 0, -135);
            transform.Rotate(0, 0, -180);
        }
        else if (dir == "D" && HasEye <= 0)
        {
            transform.Rotate(0, 0, -180);
        }
        else if (dir == "DL" && HasEye <= 0)
        {
            //transform.Rotate(0, 0, 135);
            transform.Rotate(0, 0, -180);
        }
        else if (dir == "L" && HasEye <= 0)
        {
            transform.Rotate(0, 0, 90);
        }
        else if (dir == "UL" && HasEye > 0)
        {
            //transform.Rotate(0, 0, 45);
            transform.Rotate(0, 0, 15);
        }
        else if (dir == "U" && HasEye > 0)
        {
            transform.Rotate(0, 0, 15);
        }
        else if (dir == "UR" && HasEye > 0)
        {
            //transform.Rotate(0, 0, -45);
            transform.Rotate(0, 0, 15);
        }
        else if (dir == "R" && HasEye > 0)
        {
            transform.Rotate(0, 0, -85);
        }
        else if (dir == "DR" && HasEye > 0)
        {
            //transform.Rotate(0, 0, -135);
            transform.Rotate(0, 0, -175);
        }
        else if (dir == "D" && HasEye > 0)
        {
            transform.Rotate(0, 0, -175);
        }
        else if (dir == "DL" && HasEye > 0)
        {
            //transform.Rotate(0, 0, 135);
            transform.Rotate(0, 0, -175);
        }
        else if (dir == "L" && HasEye > 0)
        {
            transform.Rotate(0, 0, 105);
        }
        AddForce();
    }
    public void ShootyShoot1(string dir)
    {
        if (dir == "UL")
        {
            //transform.Rotate(0, 0, 45);
            transform.Rotate(0, 0, 10);
        }
        else if (dir == "U")
        {
            transform.Rotate(0, 0, 10);
        }
        else if (dir == "UR")
        {
            //transform.Rotate(0, 0, -45);
            transform.Rotate(0, 0, 10);
        }
        else if (dir == "R")
        {
            transform.Rotate(0, 0, -100);
        }
        else if (dir == "DR")
        {
            //transform.Rotate(0, 0, -135);
            transform.Rotate(0, 0, -190);
        }
        else if (dir == "D")
        {
            transform.Rotate(0, 0, -190);
        }
        else if (dir == "DL")
        {
            //transform.Rotate(0, 0, 135);
            transform.Rotate(0, 0, -190);
        }
        else if (dir == "L")
        {
            transform.Rotate(0, 0, 100);
        }
        AddForce();
    }
    public void ShootyShoot2(string dir)
    {
        if (dir == "UL")
        {
            //transform.Rotate(0, 0, 45);
            transform.Rotate(0, 0, -10);
        }
        else if (dir == "U")
        {
            transform.Rotate(0, 0, -10);
        }
        else if (dir == "UR")
        {
            //transform.Rotate(0, 0, -45);
            transform.Rotate(0, 0, -10);
        }
        else if (dir == "R")
        {
            transform.Rotate(0, 0, -80);
        }
        else if (dir == "DR")
        {
            //transform.Rotate(0, 0, -135);
            transform.Rotate(0, 0, -170);
        }
        else if (dir == "D")
        {
            transform.Rotate(0, 0, -170);
        }
        else if (dir == "DL")
        {
            //transform.Rotate(0, 0, 135);
            transform.Rotate(0, 0, -170);
        }
        else if (dir == "L")
        {
            transform.Rotate(0, 0, 80);
        }
        AddForce();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "RoomRoot" && collision.tag != "Arrow" && collision.tag != "EnemyAttack" && collision.tag != "Loot" && stats.FireArrows.Value <= 0)
        {
            //Debug.Log(collision.gameObject.name, collision.gameObject);
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

        if (collision.tag != "Player" && collision.tag != "Enemy" && collision.tag != "RoomRoot" && collision.tag != "Obstacle" && collision.tag != "Arrow" && collision.tag != "EnemyAttack" && collision.tag != "Loot" && stats.FireArrows.Value > 0)
        {
            //Debug.Log(collision.gameObject.name, collision.gameObject);
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().enabled = false;
            burn = false;
            fire.SetActive(false);
        }
    }
}
