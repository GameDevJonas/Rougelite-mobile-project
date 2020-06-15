using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public MenuManager MenuManager;
    public GameObject rue;
    public Player player;
    public PlayerStats PlayerStats;
    GameObject playercanvas;
    public AudioSource sacsound;
    public GameObject otherAudioObject;
    public AudioSource[] otherAudio;

    public TextMeshProUGUI textbox;
    public GameObject choice01;
    public TextMeshProUGUI option01text;
    public GameObject choice02;
    public TextMeshProUGUI option02text;
    public GameObject choice03;
    public TextMeshProUGUI option03text;
    public GameObject choice04;
    public TextMeshProUGUI option04text;
    public GameObject choice05;
    public TextMeshProUGUI option05text;

    public List<Sacrifice> possiblesacrifices = new List<Sacrifice>();
    public List<Sacrifice> chosensacrifice = new List<Sacrifice>();
    public List<Item> commonsac = new List<Item>();
    public List<Item> raresac = new List<Item>();
    public List<Item> legendarysac = new List<Item>();
    public List<Item> ancientsac = new List<Item>();
    public List<Item> legendaryancientsac = new List<Item>();

    public Item commontosac;
    public Item raretosac;
    public Item white;
    public Item violet;
    public Item legendarytosac;
    public Item legendarytosac2;
    public Item ancienttosac;
    public Item legendaryancienttosac;
    public Item legendaryancienttosac2;

    public SacrificeDatabase SacrificeDatabase;
    public ItemDatabase ItemDatabase;

    public int level;
    public int levelIndex;
    public int choiceMade;
    public int sacsize;
    public int sacroll1;
    public int sacroll2;
    public int sacroll3;
    public int sacroll4;

    public bool SacrificeMade;
    public bool added = false;
    public bool option01Done = false;
    public bool option02Done = false;
    public bool option03Done = false;
    public bool option04Done = false;

    public SacrificeType loot;
    public SacrificeType consumable;
    public SacrificeType mechanic;
    public SacrificeType debuff;
    void Update()
    {
        loot = SacrificeType.loot;
        consumable = SacrificeType.consumable;
        mechanic = SacrificeType.mechanic;
        debuff = SacrificeType.debuff;
        if (rue == null)
        {
            rue = GameObject.FindGameObjectWithTag("Player");
            player = rue.GetComponent<Player>();
            PlayerStats = rue.GetComponent<PlayerStats>();
            SacrificeDatabase = rue.GetComponentInChildren<SacrificeDatabase>();
            ItemDatabase = player.GetComponentInChildren<ItemDatabase>();
            MenuManager = FindObjectOfType<MenuManager>();
            otherAudioObject = GameObject.Find("Audioes");
            otherAudio = otherAudioObject.GetComponentsInChildren<AudioSource>();
            sacsound = gameObject.GetComponentInChildren<AudioSource>();
            levelIndex = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(this);
            level = levelIndex;
            if (level > 5)
            {
                level -= 5;
            }
            choiceMade = 0;
            AddSacrifices();
            foreach (AudioSource audio in otherAudio)
            {
                audio.Stop();
            }
            textbox.text = "You Perished\nCarry on?";
            added = true;
            
            return;
        }

        //option01
        if (player.extraLives >= 1 && !option01Done && added)
        {
            sacsize = possiblesacrifices.Count;
            sacroll1 = Random.Range(0, sacsize);
            GetChosenSacrifice(possiblesacrifices[sacroll1].type, possiblesacrifices[sacroll1].intensity, level);
            possiblesacrifices.Remove(possiblesacrifices[sacroll1]);
            option01Done = true;
            //Sacrifice common, low intensity
            if (chosensacrifice[0].description == "common" && chosensacrifice[0].intensity == 0)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option01text.text = "Sacrifice 3 " + commontosac.plural;
                }
                else
                {
                    option01text.text = "Sacrifice 3 " + commontosac.title;
                }

                return;
            }

            //Sacrifice common, med intensity
            if (chosensacrifice[0].description == "common" && chosensacrifice[0].intensity == 1)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option01text.text = "Sacrifice half of your " + commontosac.plural;
                }
                else
                {
                    option01text.text = "Sacrifice half of your " + commontosac.title;
                }
                return;
            }

            //Sacrifice rare, low intensity
            if (chosensacrifice[0].description == "rare" && chosensacrifice[0].intensity == 2)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                option01text.text = "Sacrifice your " + raretosac.title;
                return;
            }

            //Sacrifice rare, med intensity
            if (chosensacrifice[0].description == "rare" && chosensacrifice[0].intensity == 3)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option01text.text = "Sacrifice 3 " + raretosac.plural;
                }
                else
                {
                    option01text.text = "Sacrifice 3 " + raretosac.title;
                }
                return;
            }

            //Sacrifice rare, high intensity
            if (chosensacrifice[0].description == "rare" && chosensacrifice[0].intensity == 4)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option01text.text = "Sacrifice 7 " + raretosac.plural;
                }
                else
                {
                    option01text.text = "Sacrifice 7 " + raretosac.title;
                }
            }

            //Sacrifice white light particle
            if (chosensacrifice[0].description == "white")
            {
                white = PlayerStats.Loot.Find(des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option01text.text = "Sacrifice your White light particle";
                return;
            }

            //Sacrifice violet light particle
            if (chosensacrifice[0].description == "violet")
            {
                violet = PlayerStats.Loot.Find(des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option01text.text = "Sacrifice your Violet light particle";
                return;
            }

            //Sacrifice legendary, low intensity
            if (chosensacrifice[0].description == "legendary" && chosensacrifice[0].intensity == 7)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                option01text.text = "Sacrifice your " + legendarytosac.title;
                return;
            }

            //Sacrifice legendary, med intensity
            if (chosensacrifice[0].description == "legendary" && chosensacrifice[0].intensity == 8)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                legendarysac.Remove(legendarytosac);
                int legendarytosac2Roll = Random.Range(0, (legendarysac.Count));
                legendarytosac2 = legendarysac[legendarytosac2Roll];
                option01text.text = "Sacrifice" + legendarytosac.title + " and " + legendarytosac2.title;
                return;
            }

            //Sacrifice ancient
            if (chosensacrifice[0].description == "ancient")
            {
                int ancienttosacRoll = Random.Range(0, (ancientsac.Count));
                ancienttosac = ancientsac[ancienttosacRoll];
                option01text.text = "Sacrifice your " + ancienttosac.title;
                return;
            }

            //Sacrifice ancient and legendary
            if (chosensacrifice[0].description == "legendary ancient")
            {
                int legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                legendaryancienttosac = legendaryancientsac[legendaryancienttosacRoll];
                if (legendaryancienttosac.tier != "Legendary Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Ancient Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option01text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }

                if (legendaryancienttosac.title != "Ancient Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Legendary Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option01text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }
                return;
            }

            //Sacrifice potions, low intensity
            if (chosensacrifice[0].description == "potion" && chosensacrifice[0].intensity == 0)
            {
                option01text.text = "Sacrifice half of your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice potions, med intensity
            if (chosensacrifice[0].description == "potion" && chosensacrifice[0].intensity == 1)
            {
                option01text.text = "Sacrifice all your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice debuff, low intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 0)
            {
                option01text.text = "Sacrifice your health.";
                return;
            }

            //Sacrifice debuff, med intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 1)
            {
                option01text.text = "Sacrifice your swiftness.";
                return;
            }

            //Sacrifice debuff, high intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 2)
            {
                option01text.text = "Sacrifice your strength.";
                return;
            }

            //Sacrifice debuff, higher intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 3)
            {
                option01text.text = "Sacrifice your dexterity.";
                return;
            }

            //Sacrifice debuff, highest intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 4)
            {
                option01text.text = "Suffer the Curse of Frailty. \n Health permanently reduced by 50%";
                return;
            }

            //Sacrifice debuff, galaxy intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 5)
            {
                option01text.text = "Suffer the Curse of Weakness. \n Strength permanently reduced by 25%";
                return;
            }

            return;

        }
        //option02
        if (player.extraLives >= 2 && option01Done && !option02Done)
        {
            sacsize = possiblesacrifices.Count;
            sacroll2 = Random.Range(0, sacsize);
            GetChosenSacrifice(possiblesacrifices[sacroll2].type, possiblesacrifices[sacroll2].intensity, level);
            possiblesacrifices.Remove(possiblesacrifices[sacroll2]);
            option02Done = true;
            //Sacrifice common, low intensity
            if (chosensacrifice[1].description == "common" && chosensacrifice[1].intensity == 0)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option02text.text = "Sacrifice 3 " + commontosac.plural;
                }
                else
                {
                    option02text.text = "Sacrifice 3 " + commontosac.title;
                }

                return;
            }

            //Sacrifice common, med intensity
            if (chosensacrifice[1].description == "common" && chosensacrifice[1].intensity == 1)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option02text.text = "Sacrifice half of your " + commontosac.plural;
                }
                else
                {
                    option02text.text = "Sacrifice half of your " + commontosac.title;
                }
                return;
            }

            //Sacrifice rare, low intensity
            if (chosensacrifice[1].description == "rare" && chosensacrifice[1].intensity == 2)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                option02text.text = "Sacrifice your " + raretosac.title;
                return;
            }

            //Sacrifice rare, med intensity
            if (chosensacrifice[1].description == "rare" && chosensacrifice[1].intensity == 3)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option02text.text = "Sacrifice 3 " + raretosac.plural;
                }
                else
                {
                    option02text.text = "Sacrifice 3 " + raretosac.title;
                }
                return;
            }

            //Sacrifice rare, high intensity
            if (chosensacrifice[1].description == "rare" && chosensacrifice[1].intensity == 4)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option02text.text = "Sacrifice 7 " + raretosac.plural;
                }
                else
                {
                    option02text.text = "Sacrifice 7 " + raretosac.title;
                }
            }

            //Sacrifice white light particle
            if (chosensacrifice[1].description == "white")
            {
                white = PlayerStats.Loot.Find(des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option02text.text = "Sacrifice your White light particle";
                return;
            }

            //Sacrifice violet light particle
            if (chosensacrifice[1].description == "violet")
            {
                violet = PlayerStats.Loot.Find(des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option02text.text = "Sacrifice your Violet light particle";
                return;
            }

            //Sacrifice legendary, low intensity
            if (chosensacrifice[1].description == "legendary" && chosensacrifice[1].intensity == 7)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                option02text.text = "Sacrifice your " + legendarytosac.title;
                return;
            }

            //Sacrifice legendary, med intensity
            if (chosensacrifice[1].description == "legendary" && chosensacrifice[1].intensity == 8)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                legendarysac.Remove(legendarytosac);
                int legendarytosac2Roll = Random.Range(0, (legendarysac.Count));
                legendarytosac2 = legendarysac[legendarytosac2Roll];
                option02text.text = "Sacrifice" + legendarytosac.title + " and " + legendarytosac2.title;
                return;
            }

            //Sacrifice ancient
            if (chosensacrifice[1].description == "ancient")
            {
                int ancienttosacRoll = Random.Range(0, (ancientsac.Count));
                ancienttosac = ancientsac[ancienttosacRoll];
                option02text.text = "Sacrifice your " + ancienttosac.title;
                return;
            }

            //Sacrifice ancient and legendary
            if (chosensacrifice[1].description == "legendary ancient")
            {
                int legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                legendaryancienttosac = legendaryancientsac[legendaryancienttosacRoll];
                if (legendaryancienttosac.tier != "Legendary Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Ancient Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option02text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }

                if (legendaryancienttosac.title != "Ancient Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Legendary Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option02text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }
                return;
            }

            //Sacrifice potions, low intensity
            if (chosensacrifice[1].description == "potion" && chosensacrifice[1].intensity == 0)
            {
                option02text.text = "Sacrifice half of your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice potions, med intensity
            if (chosensacrifice[1].description == "potion" && chosensacrifice[1].intensity == 1)
            {
                option02text.text = "Sacrifice all your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice debuff, low intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 0)
            {
                option02text.text = "Sacrifice your health.";
                return;
            }

            //Sacrifice debuff, med intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 1)
            {
                option02text.text = "Sacrifice your swiftness.";
                return;
            }

            //Sacrifice debuff, high intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 2)
            {
                option02text.text = "Sacrifice your strength.";
                return;
            }

            //Sacrifice debuff, higher intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 3)
            {
                option02text.text = "Sacrifice your dexterity.";
                return;
            }

            //Sacrifice debuff, highest intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 4)
            {
                option02text.text = "Suffer the Curse of Frailty. \n Health permanently reduced by 50%";
                return;
            }

            //Sacrifice debuff, galaxy intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 5)
            {
                option02text.text = "Suffer the Curse of Weakness. \n Strength permanently reduced by 25%";
                return;
            }

            return;
        }
        //option03
        if (player.extraLives >= 3 && option01Done && option02Done && !option03Done)
        {
            sacsize = possiblesacrifices.Count;
            sacroll3 = Random.Range(0, sacsize);
            GetChosenSacrifice(possiblesacrifices[sacroll3].type, possiblesacrifices[sacroll3].intensity, level);
            possiblesacrifices.Remove(possiblesacrifices[sacroll3]);
            option03Done = true;
            //Sacrifice common, low intensity
            if (chosensacrifice[2].description == "common" && chosensacrifice[2].intensity == 0)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option03text.text = "Sacrifice 3 " + commontosac.plural;
                }
                else
                {
                    option03text.text = "Sacrifice 3 " + commontosac.title;
                }

                return;
            }

            //Sacrifice common, med intensity
            if (chosensacrifice[2].description == "common" && chosensacrifice[2].intensity == 1)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option03text.text = "Sacrifice half of your " + commontosac.plural;
                }
                else
                {
                    option03text.text = "Sacrifice half of your " + commontosac.title;
                }
                return;
            }

            //Sacrifice rare, low intensity
            if (chosensacrifice[2].description == "rare" && chosensacrifice[2].intensity == 2)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                option03text.text = "Sacrifice your " + raretosac.title;
                return;
            }

            //Sacrifice rare, med intensity
            if (chosensacrifice[2].description == "rare" && chosensacrifice[2].intensity == 3)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option03text.text = "Sacrifice 3 " + raretosac.plural;
                }
                else
                {
                    option03text.text = "Sacrifice 3 " + raretosac.title;
                }
                return;
            }

            //Sacrifice rare, high intensity
            if (chosensacrifice[2].description == "rare" && chosensacrifice[2].intensity == 4)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option03text.text = "Sacrifice 7 " + raretosac.plural;
                }
                else
                {
                    option03text.text = "Sacrifice 7 " + raretosac.title;
                }
            }

            //Sacrifice white light particle
            if (chosensacrifice[2].description == "white")
            {
                white = PlayerStats.Loot.Find(des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option03text.text = "Sacrifice your White light particle";
                return;
            }

            //Sacrifice violet light particle
            if (chosensacrifice[2].description == "violet")
            {
                violet = PlayerStats.Loot.Find(des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option03text.text = "Sacrifice your Violet light particle";
                return;
            }

            //Sacrifice legendary, low intensity
            if (chosensacrifice[2].description == "legendary" && chosensacrifice[2].intensity == 7)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                option03text.text = "Sacrifice your " + legendarytosac.title;
                return;
            }

            //Sacrifice legendary, med intensity
            if (chosensacrifice[2].description == "legendary" && chosensacrifice[2].intensity == 8)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                legendarysac.Remove(legendarytosac);
                int legendarytosac2Roll = Random.Range(0, (legendarysac.Count));
                legendarytosac2 = legendarysac[legendarytosac2Roll];
                option03text.text = "Sacrifice" + legendarytosac.title + " and " + legendarytosac2.title;
                return;
            }

            //Sacrifice ancient
            if (chosensacrifice[2].description == "ancient")
            {
                int ancienttosacRoll = Random.Range(0, (ancientsac.Count));
                ancienttosac = ancientsac[ancienttosacRoll];
                option03text.text = "Sacrifice your " + ancienttosac.title;
                return;
            }

            //Sacrifice ancient and legendary
            if (chosensacrifice[2].description == "legendary ancient")
            {
                int legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                legendaryancienttosac = legendaryancientsac[legendaryancienttosacRoll];
                if (legendaryancienttosac.tier != "Legendary Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Ancient Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option03text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }

                if (legendaryancienttosac.title != "Ancient Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Legendary Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option03text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }
                return;
            }

            //Sacrifice potions, low intensity
            if (chosensacrifice[2].description == "potion" && chosensacrifice[2].intensity == 0)
            {
                option03text.text = "Sacrifice half of your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice potions, med intensity
            if (chosensacrifice[2].description == "potion" && chosensacrifice[2].intensity == 1)
            {
                option03text.text = "Sacrifice all your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice debuff, low intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 0)
            {
                option03text.text = "Sacrifice your health.";
                return;
            }

            //Sacrifice debuff, med intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 1)
            {
                option03text.text = "Sacrifice your swiftness.";
                return;
            }

            //Sacrifice debuff, high intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 2)
            {
                option03text.text = "Sacrifice your strength.";
                return;
            }

            //Sacrifice debuff, higher intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 3)
            {
                option03text.text = "Sacrifice your dexterity.";
                return;
            }

            //Sacrifice debuff, highest intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 4)
            {
                option03text.text = "Suffer the Curse of Frailty. \n Health permanently reduced by 50%";
                return;
            }

            //Sacrifice debuff, galaxy intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 5)
            {
                option03text.text = "Suffer the Curse of Weakness. \n Strength permanently reduced by 25%";
                return;
            }

            return;
        }
        //option04
        if (player.extraLives >= 4 && option01Done && option02Done && option03Done && !option04Done)
        {
            sacroll4 = Random.Range(0, sacsize);
            GetChosenSacrifice(possiblesacrifices[sacroll4].type, possiblesacrifices[sacroll4].intensity, level);
            possiblesacrifices.Remove(possiblesacrifices[sacroll4]);
            option04Done = true;
            //Sacrifice common, low intensity
            if (chosensacrifice[3].description == "common" && chosensacrifice[3].intensity == 0)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option04text.text = "Sacrifice 3 " + commontosac.plural;
                }
                else
                {
                    option04text.text = "Sacrifice 3 " + commontosac.title;
                }

                return;
            }

            //Sacrifice common, med intensity
            if (chosensacrifice[3].description == "common" && chosensacrifice[3].intensity == 1)
            {
                int commontosacRoll = Random.Range(0, (commonsac.Count));
                commontosac = commonsac[commontosacRoll];
                if (commontosac.plural != null)
                {
                    option04text.text = "Sacrifice half of your " + commontosac.plural;
                }
                else
                {
                    option04text.text = "Sacrifice half of your " + commontosac.title;
                }
                return;
            }

            //Sacrifice rare, low intensity
            if (chosensacrifice[3].description == "rare" && chosensacrifice[3].intensity == 2)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                option04text.text = "Sacrifice your " + raretosac.title;
                return;
            }

            //Sacrifice rare, med intensity
            if (chosensacrifice[3].description == "rare" && chosensacrifice[3].intensity == 3)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option04text.text = "Sacrifice 3 " + raretosac.plural;
                }
                else
                {
                    option04text.text = "Sacrifice 3 " + raretosac.title;
                }
                return;
            }

            //Sacrifice rare, high intensity
            if (chosensacrifice[3].description == "rare" && chosensacrifice[3].intensity == 4)
            {
                int raretosacRoll = Random.Range(0, (raresac.Count));
                raretosac = raresac[raretosacRoll];
                if (raretosac.plural != null)
                {
                    option04text.text = "Sacrifice 7 " + raretosac.plural;
                }
                else
                {
                    option04text.text = "Sacrifice 7 " + raretosac.title;
                }
            }

            //Sacrifice white light particle
            if (chosensacrifice[3].description == "white")
            {
                white = PlayerStats.Loot.Find(des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option04text.text = "Sacrifice your White light particle";
                return;
            }

            //Sacrifice violet light particle
            if (chosensacrifice[3].description == "violet")
            {
                violet = PlayerStats.Loot.Find(des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option04text.text = "Sacrifice your Violet light particle";
                return;
            }

            //Sacrifice legendary, low intensity
            if (chosensacrifice[3].description == "legendary" && chosensacrifice[3].intensity == 7)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                option04text.text = "Sacrifice your " + legendarytosac.title;
                return;
            }

            //Sacrifice legendary, med intensity
            if (chosensacrifice[3].description == "legendary" && chosensacrifice[3].intensity == 8)
            {
                int legendarytosacRoll = Random.Range(0, (legendarysac.Count));
                legendarytosac = legendarysac[legendarytosacRoll];
                legendarysac.Remove(legendarytosac);
                int legendarytosac2Roll = Random.Range(0, (legendarysac.Count));
                legendarytosac2 = legendarysac[legendarytosac2Roll];
                option04text.text = "Sacrifice" + legendarytosac.title + " and " + legendarytosac2.title;
                return;
            }

            //Sacrifice ancient
            if (chosensacrifice[3].description == "ancient")
            {
                int ancienttosacRoll = Random.Range(0, (ancientsac.Count));
                ancienttosac = ancientsac[ancienttosacRoll];
                option04text.text = "Sacrifice your " + ancienttosac.title;
                return;
            }

            //Sacrifice ancient and legendary
            if (chosensacrifice[3].description == "legendary ancient")
            {
                int legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                legendaryancienttosac = legendaryancientsac[legendaryancienttosacRoll];
                if (legendaryancienttosac.tier != "Legendary Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Ancient Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option04text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }

                if (legendaryancienttosac.title != "Ancient Loot")
                {
                    legendaryancientsac.RemoveAll(x => x.tier == "Legendary Loot");
                    legendaryancienttosacRoll = Random.Range(0, (legendaryancientsac.Count));
                    legendaryancienttosac2 = legendaryancientsac[legendaryancienttosacRoll];
                    option04text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }
                return;
            }

            //Sacrifice potions, low intensity
            if (chosensacrifice[3].description == "potion" && chosensacrifice[3].intensity == 0)
            {
                option04text.text = "Sacrifice half of your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice potions, med intensity
            if (chosensacrifice[3].description == "potion" && chosensacrifice[3].intensity == 1)
            {
                option04text.text = "Sacrifice all your potions";
                possiblesacrifices.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice debuff, low intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 0)
            {
                option04text.text = "Sacrifice your health.";
                return;
            }

            //Sacrifice debuff, med intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 1)
            {
                option04text.text = "Sacrifice your swiftness.";
                return;
            }

            //Sacrifice debuff, high intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 2)
            {
                option04text.text = "Sacrifice your strength.";
                return;
            }

            //Sacrifice debuff, higher intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 3)
            {
                option04text.text = "Sacrifice your dexterity.";
                return;
            }

            //Sacrifice debuff, highest intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 4)
            {
                option04text.text = "Suffer the Curse of Frailty. \n Health permanently reduced by 50%";
                return;
            }

            //Sacrifice debuff, galaxy intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 5)
            {
                option04text.text = "Suffer the Curse of Weakness. \n Strength permanently reduced by 25%";
                return;
            }

            return;
        }

        if (choiceMade >= 1)
        {
            choice01.SetActive(false);
            choice02.SetActive(false);
            choice03.SetActive(false);
            choice04.SetActive(false);
            choice05.SetActive(false);
        }
    }
    public void Option01()
    {
        textbox.transform.localPosition = new Vector2(0, 0);
        if (player.extraLives < 1)
        {
            textbox.text = "Game Over";
            choiceMade = 5;
            Invoke("RestartGame", 0.013f);
        }
        else
        {
            //Sacrifice common, low intensity
            if (chosensacrifice[0].description == "common" && chosensacrifice[0].intensity == 0)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.plural + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.title + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection -= 3;
                PlayerStats.SacrificeModifier(commontosac);

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }
            }

            //sacrifice common, med intensity
            if (chosensacrifice[0].description == "common" && chosensacrifice[0].intensity == 1)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.plural + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.title + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection /= 2;
                PlayerStats.SacrificeModifier(commontosac);

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }
            }

            //sacrifice rare, low intensity
            if (chosensacrifice[0].description == "rare" && chosensacrifice[0].intensity == 2)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue * 100 + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice rare, med intensity
            if (chosensacrifice[0].description == "rare" && chosensacrifice[0].intensity == 3)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 3) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + (raretosac.statValue * 3) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice rare, high intensity
            if (chosensacrifice[0].description == "rare" && chosensacrifice[0].intensity == 4)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 7 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + (raretosac.statValue * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice white
            if (chosensacrifice[0].description == "white")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + white.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == white.id).collection -= 1;
                PlayerStats.SacrificeModifier(white);
                if (white.collection <= 0)
                {
                    PlayerStats.Loot.Remove(white);
                    white.collection = 1;
                }  
            }

            //sacrifice violet
            if (chosensacrifice[0].description == "violet")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + violet.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == violet.id).collection -= 1;
                PlayerStats.SacrificeModifier(violet);
                if (violet.collection <= 0)
                {
                    PlayerStats.Loot.Remove(violet);
                    violet.collection = 1;
                }
            }

            //sacrifice legendary, low intensity
            if (chosensacrifice[0].description == "legendary" && chosensacrifice[0].intensity == 7)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);
                if (violet.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
            }

            //sacrifice legendary, med intensity
            if (chosensacrifice[0].description == "legendary" && chosensacrifice[0].intensity == 8)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and " + legendarytosac2.title + " and the powers they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendarytosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);
                PlayerStats.SacrificeModifier(legendarytosac2);
                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
                if (legendarytosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac2);
                    legendarytosac2.collection = 1;
                }
            }

            //sacrifice ancient item
            if (chosensacrifice[0].description == "ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + ancienttosac.title + " and the unrivaled power it yielded.";
                PlayerStats.Loot.Single(x => x.id == ancienttosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(ancienttosac);
                if (ancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(ancienttosac);
                    ancienttosac.collection = 1;
                }
            }

            //sacrifice legendary and ancient item
            if (chosensacrifice[0].description == "legendary ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title + " and the powers they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendaryancienttosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendaryancienttosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendaryancienttosac);
                PlayerStats.SacrificeModifier(legendaryancienttosac2);
                if (legendaryancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac);
                    legendaryancienttosac.collection = 1;
                }
                if (legendaryancienttosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac2);
                    legendaryancienttosac2.collection = 1;
                }
            }

            //sacrifice potion, low intensity
            if (chosensacrifice[0].description == "potion" && chosensacrifice[0].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost half of my potions.";
                player.potion /= 2;
            }

            //sacrifice potion, high intensity
            if (chosensacrifice[0].description == "potion" && chosensacrifice[0].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost all my potions.";
                player.potion = 0;
            }

            //sacrifice debuff, low intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% health.";
                PlayerStats.AddFlatModifier(PlayerStats.LessHP, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, med intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Movement Speed.";
                PlayerStats.AddFlatModifier(PlayerStats.LessMS, 1);
                PlayerStats.AddPercentModifier(PlayerStats.MovementSpeed, -0.25f);
            }

            //sacrifice debuff, high intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 2)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Strenght.";
                PlayerStats.AddFlatModifier(PlayerStats.LessStr, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Strength, -0.25f);
            }

            //sacrifice debuff, higher intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 3)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Dexterity.";
                PlayerStats.AddFlatModifier(PlayerStats.LessDex, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Dexterity, -0.25f);
            }

            //sacrifice debuff, highest intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 4)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with frailty, my health is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.FrailtyCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, galaxy intensity
            if (chosensacrifice[0].description == "debuff" && chosensacrifice[0].intensity == 5)
            {
                textbox.transform.position = new Vector2(0, 95);
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with weakness, my strength is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.WeaknessCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Strength, -0.25f);
            }

            RestartScene();
        }
        choiceMade = 1;
        sacsound.Play();
        return;
    }
    public void Option02()
    {
        textbox.transform.localPosition = new Vector2(0, 0);
        if (player.extraLives < 2)
        {
            textbox.text = "Game Over";
            choiceMade = 5;
            Invoke("RestartGame", 0.013f);
        }
        else
        {
            //sacrifice common, low intensity
            if (chosensacrifice[1].description == "common" && chosensacrifice[1].intensity == 0)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.plural + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.title + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection -= 3;
                PlayerStats.SacrificeModifier(commontosac);

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }

            }

            //sacrifice common, med intensity
            if (chosensacrifice[1].description == "common" && chosensacrifice[1].intensity == 1)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.plural + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.title + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection /= 2;
                PlayerStats.SacrificeModifier(commontosac);

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }
            }

            //sacrifice rare, low intensity
            if (chosensacrifice[1].description == "rare" && chosensacrifice[1].intensity == 2)
            {
                print(raretosac);
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue * 100 + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                    if (raretosac.collection <= 0)
                    {
                        PlayerStats.Loot.Remove(raretosac);
                        raretosac.collection = 1;
                    }
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                    if (raretosac.collection <= 0)
                    {
                        PlayerStats.Loot.Remove(raretosac);
                        raretosac.collection = 1;
                    }
                }
            }

            //sacrifice rare, med intensity
            if (chosensacrifice[1].description == "rare" && chosensacrifice[1].intensity == 3)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 3) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue * 3 + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice rare, high intensity
            if (chosensacrifice[1].description == "rare" && chosensacrifice[1].intensity == 4)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 7 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + (raretosac.statValue * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice white
            if (chosensacrifice[1].description == "white")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + white.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == white.id).collection -= 1;
                PlayerStats.SacrificeModifier(white);

                if (white.collection <= 0)
                {
                    PlayerStats.Loot.Remove(white);
                    white.collection = 1;
                }
            }

            //sacrifice violet
            if (chosensacrifice[1].description == "violet")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + violet.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == violet.id).collection -= 1;
                PlayerStats.SacrificeModifier(violet);

                if (violet.collection <= 0)
                {
                    PlayerStats.Loot.Remove(violet);
                    violet.collection = 1;
                }
            }

            //sacrifice legendary, low intensity
            if (chosensacrifice[1].description == "legendary" && chosensacrifice[1].intensity == 7)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);

                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
            }

            //sacrifice legendary, med intensity
            if (chosensacrifice[1].description == "legendary" && chosensacrifice[1].intensity == 8)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and " + legendarytosac2.title + " and the power they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendarytosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);
                PlayerStats.SacrificeModifier(legendarytosac2);
                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
                if (legendarytosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac2);
                    legendarytosac2.collection = 1;
                }
            }

            //sacrifice ancient item
            if (chosensacrifice[1].description == "ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + ancienttosac.title + " and the unrivaled power it yielded.";
                PlayerStats.Loot.Single(x => x.id == ancienttosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(ancienttosac);

                if (ancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(ancienttosac);
                    ancienttosac.collection = 1;
                }
            }

            //sacrifice legendary and ancient item
            if (chosensacrifice[1].description == "legendary ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title + " and the powers they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendarytosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendaryancienttosac);
                PlayerStats.SacrificeModifier(legendaryancienttosac2);
                if (legendaryancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac);
                }
                if (legendaryancienttosac2.collection <= 0)
                {
                PlayerStats.Loot.Remove(legendaryancienttosac2);
                }
            }

            //sacrifice potion, low intensity
            if (chosensacrifice[1].description == "potion" && chosensacrifice[1].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost half of my potions.";
                player.potion /= 2;
            }

            //sacrifice potion, high intensity
            if (chosensacrifice[1].description == "potion" && chosensacrifice[1].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost all my potions.";
                player.potion = 0;
            }

            //sacrifice debuff, low intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% health.";
                PlayerStats.AddFlatModifier(PlayerStats.LessHP, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, med intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Movement Speed.";
                PlayerStats.AddFlatModifier(PlayerStats.LessMS, 1);
                PlayerStats.AddPercentModifier(PlayerStats.MovementSpeed, -0.25f);
            }

            //sacrifice debuff, high intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 2)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Strenght.";
                PlayerStats.AddFlatModifier(PlayerStats.LessStr, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Strength, -0.25f);
            }

            //sacrifice debuff, higher intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 3)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Dexterity.";
                PlayerStats.AddFlatModifier(PlayerStats.LessDex, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Dexterity, -0.25f);
            }

            //sacrifice debuff, highest intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 4)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with frailty, my health is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.FrailtyCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, galaxy intensity
            if (chosensacrifice[1].description == "debuff" && chosensacrifice[1].intensity == 5)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with weakness, my strength is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.WeaknessCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Strength, -0.25f);
            }
            RestartScene();
        }
        choiceMade = 2;
        sacsound.Play();
        return;
    }
    public void Option03()
    {
        textbox.transform.localPosition = new Vector2(0, 0);
        if (player.extraLives < 3)
        {
            textbox.text = "Game Over";
            choiceMade = 5;
            Invoke("RestartGame", 0.013f);
        }
        else
        {
            //Sacrifice common, low intensity
            if (chosensacrifice[2].description == "common" && chosensacrifice[2].intensity == 0)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.plural + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.title + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection -= 3;
                PlayerStats.SacrificeModifier(commontosac);

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }
            }

            //sacrifice common, med intensity
            if (chosensacrifice[2].description == "common" && chosensacrifice[2].intensity == 1)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.plural + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.title + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection /= 2;
                PlayerStats.SacrificeModifier(commontosac);

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }
            }

            //sacrifice rare, low intensity
            if (chosensacrifice[2].description == "rare" && chosensacrifice[2].intensity == 2)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue * 100 + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice rare, med intensity
            if (chosensacrifice[2].description == "rare" && chosensacrifice[2].intensity == 3)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 3) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + (raretosac.statValue * 3) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice rare, high intensity
            if (chosensacrifice[2].description == "rare" && chosensacrifice[2].intensity == 4)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 7 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + (raretosac.statValue * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice white
            if (chosensacrifice[2].description == "white")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + white.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == white.id).collection -= 1;
                PlayerStats.SacrificeModifier(white);

                if (white.collection <= 0)
                {
                    PlayerStats.Loot.Remove(white);
                    white.collection = 1;
                }
            }

            //sacrifice violet
            if (chosensacrifice[2].description == "violet")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + violet.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == violet.id).collection -= 1;
                PlayerStats.SacrificeModifier(violet);

                if (violet.collection <= 0)
                {
                    PlayerStats.Loot.Remove(violet);
                    violet.collection = 1;
                }
            }

            //sacrifice legendary, low intensity
            if (chosensacrifice[2].description == "legendary" && chosensacrifice[2].intensity == 7)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);

                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
            }

            //sacrifice legendary, med intensity
            if (chosensacrifice[2].description == "legendary" && chosensacrifice[2].intensity == 8)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and " + legendarytosac2.title + " and the powers they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendarytosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);
                PlayerStats.SacrificeModifier(legendarytosac2);
                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
                if (legendarytosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac2);
                    legendarytosac2.collection = 1;
                }
            }

            //sacrifice ancient item
            if (chosensacrifice[2].description == "ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + ancienttosac.title + " and the unrivaled power it yielded.";
                PlayerStats.Loot.Single(x => x.id == ancienttosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(ancienttosac);

                if (ancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(ancienttosac);
                    ancienttosac.collection = 1;
                }
            }

            //sacrifice legendary and ancient item
            if (chosensacrifice[2].description == "legendary ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title + " and the powers they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendarytosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendaryancienttosac);
                PlayerStats.SacrificeModifier(legendaryancienttosac2);
                if (legendaryancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac);
                }
                if (legendaryancienttosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac2);
                }
            }

            //sacrifice potion, low intensity
            if (chosensacrifice[2].description == "potion" && chosensacrifice[2].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost half of my potions.";
                player.potion /= 2;
            }

            //sacrifice potion, high intensity
            if (chosensacrifice[2].description == "potion" && chosensacrifice[2].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost all my potions.";
                player.potion = 0;
            }

            //sacrifice debuff, low intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% health.";
                PlayerStats.AddFlatModifier(PlayerStats.LessHP, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, med intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Movement Speed.";
                PlayerStats.AddFlatModifier(PlayerStats.LessMS, 1);
                PlayerStats.AddPercentModifier(PlayerStats.MovementSpeed, -0.25f);
            }

            //sacrifice debuff, high intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 2)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Strenght.";
                PlayerStats.AddFlatModifier(PlayerStats.LessStr, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Strength, -0.25f);
            }

            //sacrifice debuff, higher intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 3)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Dexterity.";
                PlayerStats.AddFlatModifier(PlayerStats.LessDex, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Dexterity, -0.25f);
            }

            //sacrifice debuff, highest intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 4)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with frailty, my health is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.FrailtyCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, galaxy intensity
            if (chosensacrifice[2].description == "debuff" && chosensacrifice[2].intensity == 5)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with weakness, my strength is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.WeaknessCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Strength, -0.25f);
            }
            RestartScene();
        }
        choiceMade = 3;
        sacsound.Play();
        return;
    }
    public void Option04()
    {
        textbox.transform.localPosition = new Vector2(0, 0);
        if (player.extraLives < 4)
        {
            textbox.text = "Game Over";
            choiceMade = 5;
            Invoke("RestartGame", 0.013f);
        }
        else
        {
            //sacrifice common, low intensity
            if (chosensacrifice[3].description == "common" && chosensacrifice[3].intensity == 0)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.plural + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + commontosac.title + " and the " + (commontosac.statValue * 3) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection -= 3;
                PlayerStats.SacrificeModifier(commontosac);
                

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }

            }

            //sacrifice common, med intensity
            if (chosensacrifice[3].description == "common" && chosensacrifice[3].intensity == 1)
            {
                if (commontosac.plural != null)
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.plural + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost half of my " + commontosac.title + " and the " + ((commontosac.statValue * commontosac.collection) / 2) + " " + commontosac.statString + " they yielded.";
                }
                PlayerStats.Loot.Single(x => x.id == commontosac.id).collection /= 2;
                PlayerStats.SacrificeModifier(commontosac);
                

                if (commontosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(commontosac);
                    commontosac.collection = 1;
                }
            }

            //sacrifice rare, low intensity
            if (chosensacrifice[3].description == "rare" && chosensacrifice[3].intensity == 2)
            {
                print(raretosac);
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue * 100 + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                    if (raretosac.collection <= 0)
                    {
                        PlayerStats.Loot.Remove(raretosac);
                        raretosac.collection = 1;
                    }
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 1;
                    PlayerStats.SacrificeModifier(raretosac);
                    if (raretosac.collection <= 0)
                    {
                        PlayerStats.Loot.Remove(raretosac);
                        raretosac.collection = 1;
                    }
                }
            }

            //sacrifice rare, med intensity
            if (chosensacrifice[3].description == "rare" && chosensacrifice[3].intensity == 3)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 3 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 3) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + raretosac.statValue * 3 + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 3;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice rare, high intensity
            if (chosensacrifice[3].description == "rare" && chosensacrifice[3].intensity == 4)
            {
                if (raretosac.statString != "Critical strike chance" && raretosac.statString != "Critical strike damage")
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost 7 " + raretosac.title + " and the " + ((raretosac.statValue * 100) * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                else
                {
                    textbox.text = "Sacrifice accepted... \n " +
                    "Lost " + raretosac.title + " and the " + (raretosac.statValue * 7) + "%" + " " + raretosac.statString + " it yielded.";
                    PlayerStats.Loot.Single(x => x.id == raretosac.id).collection -= 7;
                    PlayerStats.SacrificeModifier(raretosac);
                }
                if (raretosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(raretosac);
                    raretosac.collection = 1;
                }
            }

            //sacrifice white
            if (chosensacrifice[3].description == "white")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + white.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == white.id).collection -= 1;
                PlayerStats.SacrificeModifier(white);

                if (white.collection <= 0)
                {
                    PlayerStats.Loot.Remove(white);
                    white.collection = 1;
                }
            }

            //sacrifice violet
            if (chosensacrifice[3].description == "violet")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + violet.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == violet.id).collection -= 1;
                PlayerStats.SacrificeModifier(violet);

                if (violet.collection <= 0)
                {
                    PlayerStats.Loot.Remove(violet);
                    violet.collection = 1;
                }
            }

            //sacrifice legendary, low intensity
            if (chosensacrifice[3].description == "legendary" && chosensacrifice[3].intensity == 7)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and the power it yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);

                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
            }

            //sacrifice legendary, med intensity
            if (chosensacrifice[3].description == "legendary" && chosensacrifice[3].intensity == 8)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendarytosac.title + " and " + legendarytosac2.title + " and the power they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendarytosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendarytosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendarytosac);
                PlayerStats.SacrificeModifier(legendarytosac2);
                if (legendarytosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac);
                    legendarytosac.collection = 1;
                }
                if (legendarytosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendarytosac2);
                    legendarytosac2.collection = 1;
                }
            }

            //sacrifice ancient item
            if (chosensacrifice[3].description == "ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + ancienttosac.title + " and the unrivaled power it yielded.";
                PlayerStats.Loot.Single(x => x.id == ancienttosac.id).collection -= 1;
                PlayerStats.SacrificeModifier(ancienttosac);

                if (ancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(ancienttosac);
                    ancienttosac.collection = 1;
                }
            }

            //sacrifice legendary and ancient item
            if (chosensacrifice[3].description == "legendary ancient")
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title + " and the powers they yielded.";
                PlayerStats.Loot.Single(x => x.id == legendaryancienttosac.id).collection -= 1;
                PlayerStats.Loot.Single(x => x.id == legendaryancienttosac2.id).collection -= 1;
                PlayerStats.SacrificeModifier(legendaryancienttosac);
                PlayerStats.SacrificeModifier(legendaryancienttosac2);
                if (legendaryancienttosac.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac);
                    legendaryancienttosac.collection = 1;
                }
                if (legendaryancienttosac2.collection <= 0)
                {
                    PlayerStats.Loot.Remove(legendaryancienttosac2);
                    legendaryancienttosac2.collection = 1;
                }
            }

            //sacrifice potion, low intensity
            if (chosensacrifice[3].description == "potion" && chosensacrifice[3].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost half of my potions.";
                player.potion /= 2;
            }

            //sacrifice potion, high intensity
            if (chosensacrifice[3].description == "potion" && chosensacrifice[3].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost all my potions.";
                player.potion = 0;
            }

            //sacrifice debuff, low intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 0)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% health.";
                PlayerStats.AddFlatModifier(PlayerStats.LessHP, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, med intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 1)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Movement Speed.";
                PlayerStats.AddFlatModifier(PlayerStats.LessMS, 1);
                PlayerStats.AddPercentModifier(PlayerStats.MovementSpeed, -0.25f);
            }

            //sacrifice debuff, high intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 2)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Strenght.";
                PlayerStats.AddFlatModifier(PlayerStats.LessStr, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Strength, -0.25f);
            }

            //sacrifice debuff, higher intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 3)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Lost 25% Dexterity.";
                PlayerStats.AddFlatModifier(PlayerStats.LessDex, 1);
                PlayerStats.AddPercentModifier(PlayerStats.Dexterity, -0.25f);
            }

            //sacrifice debuff, highest intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 4)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with frailty, my health is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.FrailtyCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Health, -0.25f);
            }

            //sacrifice debuff, galaxy intensity
            if (chosensacrifice[3].description == "debuff" && chosensacrifice[3].intensity == 5)
            {
                textbox.text = "Sacrifice accepted... \n " +
                "Cursed with weakness, my strength is forever reduced.";
                PlayerStats.AddFlatModifier(PlayerStats.WeaknessCurse, 1);
                PlayerStats.AddPercentMultModifier(PlayerStats.Strength, -0.25f);
            }

            RestartScene();
        }
        choiceMade = 4;
        sacsound.Play();
        return;
    }
    public void Option05()
    {
        textbox.transform.localPosition = new Vector2(0, 0);
        textbox.text = "Game Over";
        choiceMade = 5;
        Invoke("RestartGame", 0.013f);
        return;
    }

    void RestartGame()
    {
        if (MenuManager != null)
        {
            Time.timeScale = 1;
            MenuManager.ToStartScreen();
            Destroy(gameObject);
        }
    }

    void RestartScene()
    {
        if (MenuManager != null)
        {
            foreach (AudioSource audio in otherAudio)
            {
                audio.Play();
            }
            MenuManager.ReloadLevel(levelIndex);
            RemoveSacrifices();
            player.HealthSystem.Heal(PlayerStats.Health.Value);
        }
        Destroy(gameObject, 4f);
    }
    public void AddSacrifices()
    {
        var q = PlayerStats.Loot.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

        var PlayerCommonLowIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 3);

        var PlayerCommonMedIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && PlayerStats.Loot.Any(r => r.collection >= 6));

        var PlayerRareLowIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerRareMedIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && PlayerStats.Loot.Any(r => r.collection >= 3));

        var PlayerRareHighIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && PlayerStats.Loot.Any(r => r.collection >= 7));

        var PlayerLegendaryLowIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerWhite = PlayerStats.Loot.Any
            (des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerViolet = PlayerStats.Loot.Any
            (des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerLegendayMedIntensity = PlayerStats.Loot.Any
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && PlayerStats.Loot.GroupBy(x => x.tier).Any(g => g.Count() > 1));

        var PlayerAncient = PlayerStats.Loot.Any
           (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase));

        var PlayerLegendaryAncient = PlayerStats.Loot.Any
           (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase)
           && PlayerStats.Loot.Any(dis => dis.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)));


        var SacrificeCommonLowIntensity = possiblesacrifices.Any
            (des => des.description.Equals("common", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 0);

        var SacrificeCommon = possiblesacrifices.Any
            (des => des.description.Equals("common", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeRare = possiblesacrifices.Any
            (des => des.description.Equals("rare", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeWhite = possiblesacrifices.Any
            (des => des.description.Equals("white", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeViolet = possiblesacrifices.Any
            (des => des.description.Equals("violet", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeLegendaryLowIntensity = possiblesacrifices.Any
            (des => des.description.Equals("legendary", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 7);

        var SacrificeLegendaryMedIntensity = possiblesacrifices.Any
            (des => des.description.Equals("legendary", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 8);

        var SacrificeAncient = possiblesacrifices.Any
            (des => des.description.Equals("ancient", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificeLegendaryAncient = possiblesacrifices.Any
           (des => des.description.Equals("legendary ancient", System.StringComparison.InvariantCultureIgnoreCase));

        var SacrificePotion = possiblesacrifices.Any
            (des => des.description.Equals("potion", System.StringComparison.InvariantCultureIgnoreCase));


        var SacrificeDebuffLowIntensity = possiblesacrifices.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 0);

        var SacrificeDebuffMedIntensity = possiblesacrifices.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 1);

        var SacrificeDebuffHighIntensity = possiblesacrifices.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && des.intensity == 2);

        var SacrificeDebuffHigherIntensity = possiblesacrifices.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && possiblesacrifices.Any(r => r.intensity == 3));

        var SacrificeDebuffHighestIntensity = possiblesacrifices.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && possiblesacrifices.Any(r => r.intensity == 4));

        var SacrificeDebuffGalaxyIntensity = possiblesacrifices.Any
            (des => des.description.Equals("debuff", System.StringComparison.InvariantCultureIgnoreCase)
            && possiblesacrifices.Any(r => r.intensity == 5));


        List<Item> commonitemlowitensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 3);

        List<Item> rareitemlowintensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> commonitemmedintensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Common Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 6);

        List<Item> rareitemmedintensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 3);

        List<Item> rareitemhighintensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Rare Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && des.collection >= 7);

        List<Item> legendaryitemlowintensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> legendaryitemhighintensity = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Legendary Loot", System.StringComparison.InvariantCultureIgnoreCase)
            && PlayerStats.Loot.GroupBy(x => x.tier).Any(y => y.Count() > 1));

        List<Item> ancientitem = PlayerStats.Loot.FindAll
            (des => des.tier.Equals("Ancient Loot", System.StringComparison.InvariantCultureIgnoreCase));

        List<Item> legendaryancientitem = ancientitem.Concat(legendaryitemlowintensity).ToList();


        if (PlayerCommonLowIntensity && !SacrificeCommonLowIntensity)
        {
            GetPossibleSacrifice(loot, 0, level);
            if (commonsac != commonitemlowitensity)
            {
                commonsac.AddRange(commonitemlowitensity);
            }
        }

        if (PlayerCommonMedIntensity && !SacrificeCommonLowIntensity)
        {
            GetPossibleSacrifice(loot, 1, level);
            if (commonitemmedintensity != commonsac)
            {
                commonsac.AddRange(commonitemmedintensity);
            }
        }

        if (PlayerRareLowIntensity && !SacrificeRare)
        {
            GetPossibleSacrifice(loot, 2, level);
            if (rareitemlowintensity != raresac)
            {
                raresac.AddRange(rareitemlowintensity);
            }
        }

        if (PlayerRareMedIntensity && !SacrificeRare)
        {
            GetPossibleSacrifice(loot, 3, level);
            if (rareitemmedintensity != raresac)
            {
                commonsac.AddRange(commonitemmedintensity);
            }
        }

        if (PlayerRareHighIntensity && !SacrificeRare)
        {
            GetPossibleSacrifice(loot, 4, level);
            if (rareitemhighintensity != raresac)
            {
                raresac.AddRange(rareitemhighintensity);
            }
        }

        if (PlayerWhite && !SacrificeWhite)
        {
            GetPossibleSacrifice(loot, 5, level);
        }

        if (PlayerViolet && !SacrificeViolet)
        {
            GetPossibleSacrifice(loot, 6, level);
        }

        if (PlayerLegendaryLowIntensity && !SacrificeLegendaryLowIntensity)
        {
            GetPossibleSacrifice(loot, 7, level);
            if (legendaryitemlowintensity != legendarysac)
            {
                legendarysac.AddRange(legendaryitemlowintensity);
            }
        }

        if (PlayerLegendayMedIntensity && !SacrificeLegendaryMedIntensity)
        {
            GetPossibleSacrifice(loot, 8, level);
            if (legendaryitemhighintensity != legendarysac)
            {
                legendarysac.AddRange(legendaryitemhighintensity);
            }
        }

        if (PlayerAncient && !SacrificeAncient)
        {
            GetPossibleSacrifice(loot, 9, level);
            if (ancientitem != ancientsac)
            {
                ancientsac.AddRange(ancientitem);
            }
        }

        if (PlayerLegendaryAncient && !SacrificeLegendaryAncient)
        {
            GetPossibleSacrifice(loot, 10, level);
            if (legendaryancientitem != legendaryancientsac)
            {
                legendaryancientsac.AddRange(legendaryancientitem);
            }
        }

        if (player.potion >= 1 && !SacrificePotion)
        {
            GetPossibleSacrifice(consumable, 0, level);
        }

        if (player.potion >= 1 && !SacrificePotion)
        {
            GetPossibleSacrifice(consumable, 1, level);
        }

        if (PlayerStats.LessHP.Value == 0 && !SacrificeDebuffLowIntensity)
        {
            GetPossibleSacrifice(debuff, 0, level);
        }

        if (PlayerStats.LessMS.Value == 0 && !SacrificeDebuffMedIntensity)
        {
            GetPossibleSacrifice(debuff, 1, level);
        }

        if (PlayerStats.LessStr.Value == 0 && !SacrificeDebuffHighIntensity)
        {
            GetPossibleSacrifice(debuff, 2, level);
        }

        if (PlayerStats.LessDex.Value == 0 && !SacrificeDebuffHigherIntensity)
        {
            GetPossibleSacrifice(debuff, 3, level);
        }

        if (PlayerStats.FrailtyCurse.Value == 0 && !SacrificeDebuffHighestIntensity)
        {
            GetPossibleSacrifice(debuff, 4, level);
        }

        if (PlayerStats.WeaknessCurse.Value == 0 && !SacrificeDebuffGalaxyIntensity)
        {
            GetPossibleSacrifice(debuff, 5, level);
        }

    }
    public void GetChosenSacrifice(SacrificeType type, int intensity, int level)
    {
        Sacrifice sacrificeToAdd = SacrificeDatabase.GetSacrifice(type, intensity, level);
        if (sacrificeToAdd != null)
        {
            chosensacrifice.Add(sacrificeToAdd);
        }
        else return;
    }
    public void GetPossibleSacrifice(SacrificeType type, int intensity, int level)
    {
        Sacrifice sacrificeToAdd = SacrificeDatabase.GetSacrifice(type, intensity, level);
        if (sacrificeToAdd != null)
        {
           possiblesacrifices.Add(sacrificeToAdd);
        }
        else return;
    }
    public void RemoveSacrifices()
    {
        possiblesacrifices.RemoveRange(0, possiblesacrifices.Count);
        chosensacrifice.RemoveRange(0, chosensacrifice.Count);
        commonsac.RemoveRange(0, commonsac.Count);
        raresac.RemoveRange(0, raresac.Count);
        legendarysac.RemoveRange(0, legendarysac.Count);
        ancientsac.RemoveRange(0, ancientsac.Count);
        legendaryancientsac.RemoveRange(0, legendaryancientsac.Count);
    }
    public Sacrifice CheckforSacrifice(SacrificeType type, int intensity)
    {
        return possiblesacrifices.Find(item => (item.type == type) && (item.intensity == intensity));
    }
}
