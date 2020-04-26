using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossdoorScript : MonoBehaviour
{
    public GameObject confirmation;
    public GameObject sacrifice;
    public GameObject player;
    public bool IsInRange;
    public bool PromptReady = true;
    public bool SacrificeMade = false;

    private void Update()
    {
        if (IsInRange == true && PromptReady == true)
        {
            Time.timeScale = 0;
            PromptReady = false;
            confirmation.SetActive(true);
        }

        if (confirmation.GetComponent<ConfirmationScript>().choiceMade == 2)
        {
            confirmation.GetComponent<ConfirmationScript>().choiceMade = 0;
            confirmation.SetActive(false);
            Time.timeScale = 1;
        }

        if (confirmation.GetComponent<ConfirmationScript>().choiceMade == 1)
        {
            confirmation.GetComponent<ConfirmationScript>().choiceMade = 0;
            confirmation.SetActive(false);
            sacrifice.SetActive(true);
        }
        
        if (sacrifice.GetComponent<SacrificeScript>().choiceMade == 1 && SacrificeMade == false)
        {
            Time.timeScale = 1;

            Sacrifice01();
        }
        if (sacrifice.GetComponent<SacrificeScript>().choiceMade == 2 && SacrificeMade == false)
        {
            Time.timeScale = 1;

            Sacrifice02();
        }
    }
    private void TextboxGone()
    {
        sacrifice.GetComponent<SacrificeScript>().choiceMade = 0;
        sacrifice.SetActive(false);
        Destroy(this);
    }
    private void Sacrifice01()
    {
        SacrificeMade = true;
        Player Player = player.GetComponent<Player>();
        Player.HealthSystem.CurrentHealth(Player.maxHealth * 0.5f);

        Invoke("TextboxGone", 3f);
    }
    private void Sacrifice02()
    {
        SacrificeMade = true;
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        playerStats.AddPercentModifier(playerStats.Strength, -0.5f);

        Invoke("TextboxGone", 3f);
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            IsInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            IsInRange = false;
            PromptReady = true;
        }
    }
}
