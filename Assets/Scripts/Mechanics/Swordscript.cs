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
