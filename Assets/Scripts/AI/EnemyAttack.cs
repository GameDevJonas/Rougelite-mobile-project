using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    public float damage;
    public GameObject player;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<JEnemy>() == null)
        {
            damage = GetComponentInParent<JBoss>().damage;
        }
        else
        {
            damage = GetComponentInParent<JEnemy>().damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Player Player = player.GetComponent<Player>();

            PlayerStats playerStats = player.GetComponentInChildren<PlayerStats>();

            if (GetComponentInParent<JEnemy>() == null)
            {
                Player.TakeDamageAndKnockBack(damage, GetComponentInParent<JBoss>().direction);
            }
            else
            {
                Player.TakeDamageAndKnockBack(damage, GetComponentInParent<JEnemy>().direction);
            }

            if (playerStats.ShieldReflectsDmg.Value > 0 && Player.canTakeDamage == false)
            {
                if (GetComponentInParent<JEnemy>() == null)
                {
                    GetComponentInParent<JBoss>().healthSystem.Damage(damage);
                }
                else
                {
                    GetComponentInParent<JEnemy>().healthSystem.Damage(damage);
                }
                
                print(damage);
            }
        }
    }
}
