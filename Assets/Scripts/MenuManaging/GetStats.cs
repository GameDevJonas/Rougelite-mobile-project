using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GetStats : MonoBehaviour
{
    public GameObject player;

    public PlayerStats currentStats;

    public TextMeshProUGUI currentMaxHealth;
    public TextMeshProUGUI currentStrength;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                currentStats = player.GetComponent<PlayerStats>();
            }

            currentMaxHealth.text = "Max health: " + currentStats.Health.Value;
            currentStrength.text = "Strength: " + currentStats.Strength.Value;
        }


    }
}
