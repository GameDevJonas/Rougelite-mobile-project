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
    public List<Item> raresac = new List<Item>();
    public List<Item> legendarysac = new List<Item>();
    public List<Item> ancientsac = new List<Item>();
    public List<Item> legendaryancientsac = new List<Item>();
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
    }
    private void Update()
    {
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
        var q = playerStats.Loot.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

        var PlayerCommonLowIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 3));

        var PlayerCommonMedIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 6));

        var PlayerRareLowIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerRareMedIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 3));

        var PlayerRareHighIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 7));

        var PlayerLegendaryLowIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerWhite = playerStats.Loot.Any
            (des => des.title.Equals("White Light Particle", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerPurple = playerStats.Loot.Any
            (des => des.title.Equals("Purple Light Particle", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerLegendayMedIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.GroupBy(x => x.tier).Any(g => g.Count() > 1));

        var PlayerAncient = playerStats.Loot.Any
           (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerLegendaryAncient = playerStats.Loot.Any
           (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase) 
           && playerStats.Loot.Any(dis => dis.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)));


        var SacrificeCommon = sacrifice.Any
            (des => des.description.Equals("common", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeRare = sacrifice.Any
            (des => des.description.Equals("rare", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeWhite = sacrifice.Any
            (des => des.description.Equals("white", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificePurple = sacrifice.Any
            (des => des.description.Equals("purple", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeLegendaryLowIntensity = sacrifice.Any
            (des => des.description.Equals("legendary", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 7));

        var SacrificeLegendaryMedIntensity = sacrifice.Any
            (des => des.description.Equals("legendary", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 8));

        var SacrificeAncient = sacrifice.Any
            (des => des.description.Equals("ancient", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeLegendaryAncient = sacrifice.Any
           (des => des.description.Equals("legendary ancient", System.StringComparison.InvariantCultureIgnoreCase));

        var Potion = sacrifice.Any
            (des => des.description.Equals("potion", System.StringComparison.InvariantCultureIgnoreCase));

        var MechanicLowIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase) 
            && sacrifice.Any(r => r.intensity == 0));

        var MechanicMedIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 1));

        var MechanicHighIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 2));

        var MechanicHigherIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 3));

        var MechanicHighestIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 4));

        var DebuffLowIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 0));

        var DebuffMedIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 1));

        var DebuffHighIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 2));

        var DebuffHigherIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 3));

        var DebuffHighestIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 4));

        var DebuffGalaxyIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 5));


        List<Item> commonitemlowitensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 3));

        List<Item> rareitemlowintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> commonitemmedintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 6));

        List<Item> rareitemmedintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 3));

        List<Item> rareitemhighintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.Any(r => r.collection >= 7));

        List<Item> legendaryitemlowintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> legendaryitemhighintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.GroupBy(x => x.tier).Any(g => g.Count() > 1));

        List<Item> ancientitem = playerStats.Loot.FindAll
            (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> legendaryancientitem = ancientitem.Concat(legendaryitemlowintensity).ToList();


        if (PlayerCommonLowIntensity && !SacrificeCommon)
        {
            GetSacrifice(loot, 0, level);
            if (commonsac != commonitemlowitensity)
            {
                commonsac.RemoveRange(0, commonsac.Count);
                commonsac.AddRange(commonitemlowitensity);
            }
        }

        if (PlayerCommonMedIntensity && !SacrificeCommon)
        {
            GetSacrifice(loot, 1, level);
            if (commonitemmedintensity != commonsac)
            {
                commonsac.RemoveRange(0, commonsac.Count);
                commonsac.AddRange(commonitemmedintensity);
            }
        }

        if (PlayerRareLowIntensity && !SacrificeRare)
        {
            GetSacrifice(loot, 2, level);
            if (rareitemlowintensity != raresac)
            {
                raresac.RemoveRange(0, raresac.Count);
                raresac.AddRange(rareitemlowintensity);
            }
        }

        if (PlayerRareMedIntensity && !SacrificeRare)
        {
            GetSacrifice(loot, 3, level);
            if (rareitemmedintensity != raresac)
            {
                commonsac.RemoveRange(0, commonsac.Count);
                commonsac.AddRange(commonitemmedintensity);
            }
        }

        if (PlayerRareHighIntensity && !SacrificeRare)
        {
            GetSacrifice(loot, 4, level);
            if (rareitemhighintensity != raresac)
            {
                raresac.RemoveRange(0, raresac.Count);
                raresac.AddRange(rareitemhighintensity);
            }
        }

        if (PlayerWhite && !SacrificeWhite)
        {
            GetSacrifice(loot, 5, level);
        }

        if (PlayerWhite && !SacrificeWhite)
        {
            GetSacrifice(loot, 6, level);
        }

        if (PlayerLegendaryLowIntensity && !SacrificeLegendaryLowIntensity)
        {
            GetSacrifice(loot, 7, level);
            if (legendaryitemlowintensity != legendarysac)
            {
                legendarysac.RemoveRange(0, legendarysac.Count);
                legendarysac.AddRange(legendaryitemlowintensity);
            }
        }

        if (PlayerLegendayMedIntensity && !SacrificeLegendaryMedIntensity)
        {
            GetSacrifice(loot, 8, level);
            if (legendaryitemhighintensity != legendarysac)
            {
                legendarysac.RemoveRange(0, legendarysac.Count);
                legendarysac.AddRange(legendaryitemhighintensity);
            }
        }

        if (PlayerAncient && !SacrificeAncient)
        {
            GetSacrifice(loot, 9, level);
            if (ancientitem != ancientsac)
            {
                ancientsac.RemoveRange(0, ancientsac.Count);
                ancientsac.AddRange(ancientitem);
            }
        }

        if (PlayerLegendaryAncient && !SacrificeLegendaryAncient)
        {
            GetSacrifice(loot, 10, level);
            if (legendaryancientitem != legendaryancientsac)
            {
                legendaryancientsac.RemoveRange(0, legendaryancientsac.Count);
                legendaryancientsac.AddRange(legendaryancientitem);
            }
        }

        if (player.potion >= 2 && !Potion)
        {
            GetSacrifice(consumable, 0, level);
        }

        if (player.potion >= 5 && !Potion)
        {
            GetSacrifice(consumable, 1, level);
        }

        if (playerStats.HasEye.Value == 0 && !MechanicLowIntensity)
        {
            GetSacrifice(mechanic, 0, level);
        }

        if (playerStats.CanDodge.Value == 0 && !MechanicMedIntensity)
        {
            GetSacrifice(mechanic, 1, level);
        }

        if (playerStats.ShieldArm.Value == 0 && !MechanicHighIntensity)
        {
            GetSacrifice(mechanic, 2, level);
        }

        if (playerStats.HasSword.Value == 0 && !MechanicHigherIntensity)
        {
            GetSacrifice(mechanic, 3, level);
        }

        if (playerStats.HasCrossbow.Value == 0 && !MechanicHighestIntensity)
        {
            GetSacrifice(mechanic, 4, level);
        }


        if (playerStats.LessHP.Value == 0 && !DebuffLowIntensity)
        {
            GetSacrifice(debuff, 0, level);
        }

        if (playerStats.LessMS.Value == 0 && !DebuffMedIntensity)
        {
            GetSacrifice(debuff, 1, level);
        }

        if (playerStats.LessStr.Value == 0 && !DebuffHighIntensity)
        {
            GetSacrifice(debuff, 2, level);
        }

        if (playerStats.LessDex.Value == 0 && !DebuffHigherIntensity)
        {
            GetSacrifice(debuff, 3, level);
        }

        if (playerStats.WeaknessCurse.Value == 0 && !DebuffHighestIntensity)
        {
            GetSacrifice(debuff, 4, level);
        }

        if (playerStats.FrailtyCurse.Value == 0 && !DebuffGalaxyIntensity)
        {
            GetSacrifice(debuff, 5, level);
        }

    }
    public void GetSacrifice(SacrificeType type, int intensity, int level)
    {
        Sacrifice sacrificeToAdd = SacrificeDatabase.GetSacrifice(type, intensity, level);
        if (sacrificeToAdd != null)
        {
            sacrifice.Add(sacrificeToAdd);
        }
        else return;
    }
    public void RemoveSacrifices()
    {
        sacrifice.RemoveRange(0, sacrifice.Count);
        commonsac.RemoveRange(0, commonsac.Count);
        raresac.RemoveRange(0, raresac.Count);
        legendarysac.RemoveRange(0, legendarysac.Count);
        ancientsac.RemoveRange(0, ancientsac.Count);
        legendaryancientsac.RemoveRange(0, legendaryancientsac.Count);
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
