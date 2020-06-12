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
    public SacrificeScript SacrificeScript;

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
            SacrificeScript = sacrificing.GetComponent<SacrificeScript>();
            level = SceneManager.GetActiveScene().buildIndex;
            if (level > 5)
            {
                level -= 5;
            }
        }
    }
    private void Update()
    {
        
        if (IsInRange == true && PromptReady == true)
        {
            if (!SacrificeMade)
            {
                Time.timeScale = 0;
            }
            AddSacrifices();
            PromptReady = false;
            confirmation.SetActive(true);
        }

        if (confirmation.GetComponent<ConfirmationScript>().choiceMade == 2)
        {
            confirmation.GetComponent<ConfirmationScript>().choiceMade = 0;
            confirmation.SetActive(false);
            RemoveSacrifices();
            Time.timeScale = 1;
        }

        if (confirmation.GetComponent<ConfirmationScript>().choiceMade == 1)
        {
            confirmation.GetComponent<ConfirmationScript>().choiceMade = 0;
            confirmation.SetActive(false);
            sacrificing.SetActive(true);
        }

        if (sacrificing.GetComponent<SacrificeScript>().choiceMade > 0 && SacrificeMade == false)
        {
            RemoveSacrifices();
            Time.timeScale = 1;

            Invoke("Sacrifice", 3f);
        }
    }

    private void TextboxGone()
    {
        sacrificing.GetComponent<SacrificeScript>().choiceMade = 0;
        sacrificing.SetActive(false);
    }
    private void Sacrifice()
    {
        SacrificeMade = true;
        MenuManager menu = FindObjectOfType<MenuManager>();
        menu.ToAlphaBoss();
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!SacrificeMade)
            {
                IsInRange = true;
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
            && des.collection >= 3);

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
            (des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerViolet = playerStats.Loot.Any
            (des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerLegendayMedIntensity = playerStats.Loot.Any
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.GroupBy(x => x.tier).Any(g => g.Count() > 1));

        var PlayerAncient = playerStats.Loot.Any
           (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerLegendaryAncient = playerStats.Loot.Any
           (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase) 
           && playerStats.Loot.Any(dis => dis.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)));


        var SacrificeCommonLowIntensity = sacrifice.Any
            (des => des.description.Equals("common", System.StringComparison.InvariantCultureIgnoreCase) 
            && des.intensity == 0);

        var SacrificeCommon = sacrifice.Any
            (des => des.description.Equals("common", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeRare = sacrifice.Any
            (des => des.description.Equals("rare", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeWhite = sacrifice.Any
            (des => des.description.Equals("white", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeViolet = sacrifice.Any
            (des => des.description.Equals("violet", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeLegendaryLowIntensity = sacrifice.Any
            (des => des.description.Equals("legendary", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 7);

        var SacrificeLegendaryMedIntensity = sacrifice.Any
            (des => des.description.Equals("legendary", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 8);

        var SacrificeAncient = sacrifice.Any
            (des => des.description.Equals("ancient", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeLegendaryAncient = sacrifice.Any
           (des => des.description.Equals("legendary ancient", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificePotion = sacrifice.Any
            (des => des.description.Equals("potion", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeMechanicLowIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase) 
            && des.intensity == 0);

        var SacrificeMechanicMedIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 1);

        var SacrificeMechanicHighIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 2);

        var SacrificeMechanicHigherIntensity = sacrifice.Any
            (des => des.description.Equals("mechanic", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 3);

        var SacrificeMechanicHighestIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 4);

        var SacrificeDebuffLowIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 0);

        var SacrificeDebuffMedIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 1);

        var SacrificeDebuffHighIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 2);

        var SacrificeDebuffHigherIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 3));

        var SacrificeDebuffHighestIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 4));

        var SacrificeDebuffGalaxyIntensity = sacrifice.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && sacrifice.Any(r => r.intensity == 5));


        List<Item> commonitemlowitensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 3);

        List<Item> rareitemlowintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> commonitemmedintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 6);

        List<Item> rareitemmedintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 3);

        List<Item> rareitemhighintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 7);

        List<Item> legendaryitemlowintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> legendaryitemhighintensity = playerStats.Loot.FindAll
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && playerStats.Loot.GroupBy(x => x.tier).Any(y => y.Count() > 1));

        List<Item> ancientitem = playerStats.Loot.FindAll
            (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> legendaryancientitem = ancientitem.Concat(legendaryitemlowintensity).ToList();


        if (PlayerCommonLowIntensity && !SacrificeCommonLowIntensity)
        {
            GetSacrifice(loot, 0, level);
            if (commonsac != commonitemlowitensity)
            {
                commonsac.AddRange(commonitemlowitensity);
            }
        }

        if (PlayerCommonMedIntensity && !SacrificeCommonLowIntensity)
        {
            GetSacrifice(loot, 1, level);
            if (commonitemmedintensity != commonsac)
            {
                commonsac.AddRange(commonitemmedintensity);
            }
        }

        if (PlayerRareLowIntensity && !SacrificeRare)
        {
            GetSacrifice(loot, 2, level);
            if (rareitemlowintensity != raresac)
            {
                raresac.AddRange(rareitemlowintensity);
            }
        }

        if (PlayerRareMedIntensity && !SacrificeRare)
        {
            GetSacrifice(loot, 3, level);
            if (rareitemmedintensity != raresac)
            {
                commonsac.AddRange(commonitemmedintensity);
            }
        }

        if (PlayerRareHighIntensity && !SacrificeRare)
        {
            GetSacrifice(loot, 4, level);
            if (rareitemhighintensity != raresac)
            {
                raresac.AddRange(rareitemhighintensity);
            }
        }

        if (PlayerWhite && !SacrificeWhite)
        {
            GetSacrifice(loot, 5, level);
        }

        if (PlayerViolet && !SacrificeViolet)
        {
            GetSacrifice(loot, 6, level);
        }

        if (PlayerLegendaryLowIntensity && !SacrificeLegendaryLowIntensity)
        {
            GetSacrifice(loot, 7, level);
            if (legendaryitemlowintensity != legendarysac)
            {
                legendarysac.AddRange(legendaryitemlowintensity);
            }
        }

        if (PlayerLegendayMedIntensity && !SacrificeLegendaryMedIntensity)
        {
            GetSacrifice(loot, 8, level);
            if (legendaryitemhighintensity != legendarysac)
            {
                legendarysac.AddRange(legendaryitemhighintensity);
            }
        }

        if (PlayerAncient && !SacrificeAncient)
        {
            GetSacrifice(loot, 9, level);
            if (ancientitem != ancientsac)
            {
                ancientsac.AddRange(ancientitem);
            }
        }

        if (PlayerLegendaryAncient && !SacrificeLegendaryAncient)
        {
            GetSacrifice(loot, 10, level);
            if (legendaryancientitem != legendaryancientsac)
            {
                legendaryancientsac.AddRange(legendaryancientitem);
            }
        }

        if (player.potion >= 6 && !SacrificePotion)
        {
            GetSacrifice(consumable, 0, level);
        }

        if (player.potion >= 6 && !SacrificePotion)
        {
            GetSacrifice(consumable, 1, level);
        }

        if (playerStats.HasEye.Value == 0 && !SacrificeMechanicLowIntensity)
        {
            GetSacrifice(mechanic, 0, level);
        }

        if (playerStats.CanDodge.Value == 0 && !SacrificeMechanicMedIntensity)
        {
            GetSacrifice(mechanic, 1, level);
        }

        if (playerStats.ShieldArm.Value == 0 && !SacrificeMechanicHighIntensity)
        {
            GetSacrifice(mechanic, 2, level);
        }

        if (playerStats.HasSword.Value == 0 && !SacrificeMechanicHigherIntensity)
        {
            GetSacrifice(mechanic, 3, level);
        }

        if (playerStats.HasCrossbow.Value == 0 && !SacrificeMechanicHighestIntensity)
        {
            GetSacrifice(mechanic, 4, level);
        }


        if (playerStats.LessHP.Value == 0 && !SacrificeDebuffLowIntensity)
        {
            GetSacrifice(debuff, 0, level);
        }

        if (playerStats.LessMS.Value == 0 && !SacrificeDebuffMedIntensity)
        {
            GetSacrifice(debuff, 1, level);
        }

        if (playerStats.LessStr.Value == 0 && !SacrificeDebuffHighIntensity)
        {
            GetSacrifice(debuff, 2, level);
        }

        if (playerStats.LessDex.Value == 0 && !SacrificeDebuffHigherIntensity)
        {
            GetSacrifice(debuff, 3, level);
        }

        if (playerStats.FrailtyCurse.Value == 0 && !SacrificeDebuffHighestIntensity)
        {
            GetSacrifice(debuff, 4, level);
        }

        if (playerStats.WeaknessCurse.Value == 0 && !SacrificeDebuffGalaxyIntensity)
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
    public Sacrifice CheckforSacrifice(SacrificeType type, int intensity)
    {
        return sacrifice.Find(item => (item.type == type) && (item.intensity == intensity));
    }
}
