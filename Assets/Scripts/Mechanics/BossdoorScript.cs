using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

//ADD 2 SPRITES, ONE LOCKED AND ONE OPEN
public class BossdoorScript : MonoBehaviour
{
    public GameObject confirmation;
    public GameObject sacrificing;
    public GameObject rue;
    public Player player;
    public PlayerStats playerStats;

    public List<Sacrifice> sacrifice = new List<Sacrifice>();
    public List<Item> commonsac = new List<Item>();
    public SacrificeDatabase SacrificeDatabase;
    public ItemDatabase ItemDatabase;

    public int level;

    public bool IsInRange;
    public bool PromptReady = true;
    public bool SacrificeMade = false;
    public bool gotSac = false;
    public bool remove = false;

    public SacrificeType loot;
    public SacrificeType consumable;
    public SacrificeType mechanic;
    public SacrificeType debuff;
    private void Start()
    {
        loot = SacrificeType.loot;
        consumable = SacrificeType.consumable;
        mechanic = SacrificeType.mechanic;
        debuff = SacrificeType.debuff;
        if (rue == null)
        {
            rue = GameObject.FindGameObjectWithTag("Player");
            player = rue.GetComponent<Player>();
            playerStats = rue.GetComponent<PlayerStats>();
            SacrificeDatabase = rue.GetComponentInChildren<SacrificeDatabase>();
            ItemDatabase = player.GetComponentInChildren<ItemDatabase>();
            level = SceneManager.GetActiveScene().buildIndex;
            if (level > 5)
            {
                level -= 5;
            }
        }
        AddSacrifices();
        RemoveSacrifices();

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

    public void AddSacrifices()
    {
        var Playercommon = playerStats.Loot.Any
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase) 
            && playerStats.Loot.Any(r => r.collection >= 3));
        var Sacrificecommon = sacrifice.Any
            (des => des.description.Equals("common", System.StringComparison.InvariantCultureIgnoreCase));
        List<Item> commonitem = playerStats.Loot.FindAll((des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 3)));

        if (player.potion >= 2 )
        {
            GetSacrifice(consumable, 0, level);
        }

        if (commonsac != commonitem)
        {
            commonsac.RemoveRange(0, commonsac.Count);
            commonsac.AddRange(commonitem);
            if (Playercommon && !Sacrificecommon)
            {
                GetSacrifice(loot, 0, level);
            }
            
        }
    }
    public void GetSacrifice(SacrificeType type, int intensity, int level)
    {
        Sacrifice sacrificeToAdd = SacrificeDatabase.GetSacrifice(type, intensity, level);
        sacrifice.Add(sacrificeToAdd);
    }
    public void RemoveSacrifices()
    {
        Item Sacrificecommon = commonsac.Find(des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
        && commonsac.Any(r => r.collection < 3));
        var commonitem = commonsac.Any(r => r.collection <= 3);

        if (player.potion >= 2)
        {
            RemoveSacrifice(consumable, 0);
        }
        if (commonitem)
        {
            commonsac.Remove(Sacrificecommon);
        }
        if (commonsac.Count == 0)
        {
            RemoveSacrifice(loot, 0);
        }
    }
    public void RemoveSacrifice(SacrificeType type, int intensity)
    {
        Sacrifice item = CheckforSacrifice(type, intensity);
        if (item != null)
        {
            sacrifice.Remove(item);
        }
    }
    public Sacrifice CheckforSacrifice(SacrificeType type, int intensity)
    {
        return sacrifice.Find(item => (item.type == type) && (item.intensity == intensity));
    }

}
