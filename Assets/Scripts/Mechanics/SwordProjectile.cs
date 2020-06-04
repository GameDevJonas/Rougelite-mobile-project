using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    public PlayerStats stats;
    public int critRoll;
    public float critChance;
    public bool crit;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        critRoll = Random.Range(1, 101);
        critChance = stats.CritChance.Value;
        CritCheck();
        //Destroy(gameObject, 5f);
        //Debug.Log(critRoll);
        //Debug.Log(critChance);
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
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
