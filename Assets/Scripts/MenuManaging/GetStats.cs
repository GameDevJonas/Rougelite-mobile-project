using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GetStats : MonoBehaviour
{
    public GameObject player;

    public PlayerStats currentStats;

    public Player currenthealth;

    public TextMeshProUGUI currentMaxHealth;
    public TextMeshProUGUI currentStrength;
    public TextMeshProUGUI currentDexterity;
    public TextMeshProUGUI currentMovementSpeed;
    public TextMeshProUGUI currentCritChance;
    public TextMeshProUGUI currentCritDamage;
    public TextMeshProUGUI currentLifeOnHit;
    public TextMeshProUGUI currentPotionPotency;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                currentStats = player.GetComponent<PlayerStats>();
                currenthealth = player.GetComponent<Player>();
            }

            currentMaxHealth.text = "Health: " + currenthealth.currentHealth + "/" + currentStats.Health.Value;
            currentStrength.text = "Strength: " + currentStats.Strength.Value;
            currentDexterity.text = "Dexterity: " + currentStats.Dexterity.Value;
            currentMovementSpeed.text = "Movement Speed: " + currentStats.MovementSpeed.Value;
            currentCritChance.text = "Critical Strike Chance: " + currentStats.CritChance.Value;
            currentCritDamage.text = "Critical Strike Damage: " + currentStats.CritDamage.Value;
            currentLifeOnHit.text = "Life On Hit: " + currentStats.LifeOnHit.Value;
            currentPotionPotency.text = "Potion Potency: " + currentStats.PotionPotency.Value;
        }
}
