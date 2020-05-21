using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossdoorScript : MonoBehaviour
{
    public GameObject confirmation;
    public GameObject sacrificing;
    public GameObject player;

    public List<Sacrifice> sacrifice = new List<Sacrifice>();
    public SacrificeDatabase SacrificeDatabase;

    public int level;

    public bool IsInRange;
    public bool PromptReady = true;
    public bool SacrificeMade = false;

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            SacrificeDatabase = player.GetComponentInChildren<SacrificeDatabase>();
            GiveSacrifice(level);
            return;
        }
        if (IsInRange == true && PromptReady == true)
        {
            if (!SacrificeMade)
            {
                Time.timeScale = 0;

            }
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
            sacrificing.SetActive(true);
        }

        if (sacrificing.GetComponent<SacrificeScript>().choiceMade == 1 && SacrificeMade == false)
        {
            Time.timeScale = 1;

            Sacrifice01();
        }
        if (sacrificing.GetComponent<SacrificeScript>().choiceMade == 2 && SacrificeMade == false)
        {
            Time.timeScale = 1;

            Sacrifice02();
        }
    }
    private void TextboxGone()
    {
        sacrificing.GetComponent<SacrificeScript>().choiceMade = 0;
        sacrificing.SetActive(false);
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
            if (!SacrificeMade)
            {
                IsInRange = true;
            }
            else
            {
                Time.timeScale = 1;
                MenuManager menu = FindObjectOfType<MenuManager>();
                menu.ToAlphaBoss();
            }
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

    public void GiveSacrifice(int level)
    {
        List<Sacrifice> sacrificeToAdd = SacrificeDatabase.GetSacrifice(level);
        sacrifice = sacrificeToAdd;

        List<Sacrifice> sacrificeCheck = CheckforSacrifice(sacrificeToAdd);
    }

   public List<Sacrifice> CheckforSacrifice(List<Sacrifice> sacrifice)
    {
        return sacrifice;
    }
}
