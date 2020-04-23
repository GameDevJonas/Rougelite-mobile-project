﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage;
    public GameObject player;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Player Player = player.GetComponent<Player>();

            Player.HealthSystem.Damage(damage);
        }
    }
}
