using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordscript : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerstats;
    public int critRoll;
    public float critChance;
    public bool Crit;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerstats = player.GetComponent<PlayerStats>();
        critRoll = Random.Range(0, 100);
        critChance = playerstats.CritChance.Value;

        CritCheck();
    }
    private void Update()
    {
        critChance = playerstats.CritChance.Value;
    }

    public void RotateMeBaby(string dir)
    {
        if (dir == "UL")
        {
            transform.Rotate(0, 0, 45);
        }
        else if (dir == "U")
        {
            transform.Rotate(0, 0, 0);
        }
        else if (dir == "UR")
        {
            transform.Rotate(0, 0, -45);
        }
        else if (dir == "R")
        {
            transform.Rotate(0, 0, -90);
        }
        else if (dir == "DR")
        {
            transform.Rotate(0, 0, -135);
        }
        else if (dir == "D")
        {
            transform.Rotate(0, 0, -180);
        }
        else if (dir == "DL")
        {
            transform.Rotate(0, 0, 135);
        }
        else if (dir == "L")
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void CritCheck()
    {
        
        if (critRoll > critChance)
        {
            Crit = false;
        }
        if (critRoll < critChance || critRoll == critChance)
        {
            Crit = true;
        }
    }
}
