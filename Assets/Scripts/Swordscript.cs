using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordscript : MonoBehaviour
{
    public GameObject player;
    public GameObject Enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy = GameObject.FindGameObjectWithTag("Enemy");

            PlayerStats playerstats = player.GetComponent<PlayerStats>();
            
            EnemyAI enemy = Enemy.GetComponent<EnemyAI>();

            enemy.HealthSystem.Damage(playerstats.Strength.Value);
        }
    }
}
