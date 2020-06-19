using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class SacrificeScript : MonoBehaviour
{

    public TextMeshProUGUI textbox;
    public GameObject choice01;
    public TextMeshProUGUI option01text;
    public GameObject choice02;
    public TextMeshProUGUI option02text;
    public GameObject choice03;
    public SacrificeDatabase SacrificeDatabase;
    public BossdoorScript BossDoor;
    public GameObject rue;
    public Player player;
    public PlayerStats PlayerStats;
    public List<Sacrifice> sacrifice = new List<Sacrifice>();
    public Item commontosac;
    public Item raretosac;
    public Item white;
    public Item violet;
    public Item legendarytosac;
    public Item legendarytosac2;
    public Item ancienttosac;
    public Item legendaryancienttosac;
    public Item legendaryancienttosac2;

    public int choiceMade;
    public int level;
    public int sacsize;
    public int sacroll1;
    public int sacroll2;

    public bool option01Done = false;

    public void Option01()
    {
        //Sacrifice common, low intensity
        if (sacrifice[0].description == "common" && sacrifice[0].intensity == 0)
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
        if (sacrifice[0].description == "common" && sacrifice[0].intensity == 1)
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
        if (sacrifice[0].description == "rare" && sacrifice[0].intensity == 2)
        {
            print(raretosac.title);
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
        if (sacrifice[0].description == "rare" && sacrifice[0].intensity == 3)
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
        if (sacrifice[0].description == "rare" && sacrifice[0].intensity == 4)
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
        if (sacrifice[0].description == "white")
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
        if (sacrifice[0].description == "violet")
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
        if (sacrifice[0].description == "legendary" && sacrifice[0].intensity == 7)
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
        if (sacrifice[0].description == "legendary" && sacrifice[0].intensity == 8)
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
        if (sacrifice[0].description == "ancient")
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
        if (sacrifice[0].description == "legendary ancient")
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
        if (sacrifice[0].description == "potion" && sacrifice[0].intensity == 0)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost half of my potions.";
            player.potion /= 2;
        }

        //sacrifice potion, high intensity
        if (sacrifice[0].description == "potion" && sacrifice[0].intensity == 1)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost all my potions.";
            player.potion = 0;
        }

        //sacrifice mechanic, low intensity
        if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 0)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my eye...";
            PlayerStats.AddFlatModifier(PlayerStats.HasEye, 1);
        }

        //sacrifice mechanic, med intensity
        if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 1)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost a tendon...";
            PlayerStats.AddFlatModifier(PlayerStats.CanDodge, 1);
        }

        //sacrifice mechanic, high intensity
        if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 2)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my arm...";
            PlayerStats.AddFlatModifier(PlayerStats.ShieldArm, 1);
        }

        //sacrifice mechanic, higher intensity
        if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 3)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my sword...";
            PlayerStats.AddFlatModifier(PlayerStats.HasSword, 1);
        }

        //sacrifice mechanic, highest intensity
        if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 4)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my crossbow...";
            PlayerStats.AddFlatModifier(PlayerStats.HasCrossbow, 1);
        }

        //sacrifice debuff, low intensity
        if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 0)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% health.";
            PlayerStats.AddFlatModifier(PlayerStats.LessHP, 1);
            PlayerStats.AddPercentModifier(PlayerStats.Health, -0.25f);
        }

        //sacrifice debuff, med intensity
        if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 1)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% Movement Speed.";
            PlayerStats.AddFlatModifier(PlayerStats.LessMS, 1);
            PlayerStats.AddPercentModifier(PlayerStats.MovementSpeed, -0.25f);
        }

        //sacrifice debuff, high intensity
        if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 2)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% Strenght.";
            PlayerStats.AddFlatModifier(PlayerStats.LessStr, 1);
            PlayerStats.AddPercentModifier(PlayerStats.Strength, -0.25f);
        }

        //sacrifice debuff, higher intensity
        if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 3)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% Dexterity.";
            PlayerStats.AddFlatModifier(PlayerStats.LessDex, 1);
            PlayerStats.AddPercentModifier(PlayerStats.Dexterity, -0.25f);
        }

        //sacrifice debuff, highest intensity
        if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 4)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Cursed with frailty, my health is forever reduced.";
            PlayerStats.AddFlatModifier(PlayerStats.FrailtyCurse, 1);
            PlayerStats.AddPercentMultModifier(PlayerStats.Health, -0.25f);
        }

        //sacrifice debuff, galaxy intensity
        if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 5)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Cursed with weakness, my strength is forever reduced.";
            PlayerStats.AddFlatModifier(PlayerStats.WeaknessCurse, 1);
            PlayerStats.AddPercentMultModifier(PlayerStats.Strength, -0.25f);
        }

        choiceMade = 1;
    }
    public void Option02()
    {
        //sacrifice common, low intensity
        if (sacrifice[1].description == "common" && sacrifice[1].intensity == 0)
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
        if (sacrifice[1].description == "common" && sacrifice[1].intensity == 1)
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
        if (sacrifice[1].description == "rare" && sacrifice[1].intensity == 2)
        {
            print(raretosac.title);
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
        if (sacrifice[1].description == "rare" && sacrifice[1].intensity == 3)
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
        if (sacrifice[1].description == "rare" && sacrifice[1].intensity == 4)
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
        if (sacrifice[1].description == "white")
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
        if (sacrifice[1].description == "violet")
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
        if (sacrifice[1].description == "legendary" && sacrifice[1].intensity == 7)
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
        if (sacrifice[1].description == "legendary" && sacrifice[1].intensity == 8)
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
        if (sacrifice[1].description == "ancient")
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
        if (sacrifice[1].description == "legendary ancient")
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
        if (sacrifice[1].description == "potion" && sacrifice[1].intensity == 0)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost half of my potions.";
            player.potion /= 2;
        }

        //sacrifice potion, high intensity
        if (sacrifice[1].description == "potion" && sacrifice[1].intensity == 1)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost all my potions.";
            player.potion = 0;
        }

        //sacrifice mechanic, low intensity
        if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 0)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my eye...";
            PlayerStats.AddFlatModifier(PlayerStats.HasEye, 1);
        }

        //sacrifice mechanic, med intensity
        if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 1)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost a tendon...";
            PlayerStats.AddFlatModifier(PlayerStats.CanDodge, 1);
        }

        //sacrifice mechanic, high intensity
        if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 2)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my arm...";
            PlayerStats.AddFlatModifier(PlayerStats.ShieldArm, 1);
        }

        //sacrifice mechanic, higher intensity
        if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 3)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my sword...";
            PlayerStats.AddFlatModifier(PlayerStats.HasSword, 1);
        }

        //sacrifice mechanic, highest intensity
        if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 4)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost my crossbow...";
            PlayerStats.AddFlatModifier(PlayerStats.HasCrossbow, 1);
        }

        //sacrifice debuff, low intensity
        if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 0)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% health.";
            PlayerStats.AddFlatModifier(PlayerStats.LessHP, 1);
            PlayerStats.AddPercentModifier(PlayerStats.Health, -0.25f);
        }

        //sacrifice debuff, med intensity
        if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 1)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% Movement Speed.";
            PlayerStats.AddFlatModifier(PlayerStats.LessMS, 1);
            PlayerStats.AddPercentModifier(PlayerStats.MovementSpeed, -0.25f);
        }

        //sacrifice debuff, high intensity
        if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 2)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% Strenght.";
            PlayerStats.AddFlatModifier(PlayerStats.LessStr, 1);
            PlayerStats.AddPercentModifier(PlayerStats.Strength, -0.25f);
        }

        //sacrifice debuff, higher intensity
        if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 3)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Lost 25% Dexterity.";
            PlayerStats.AddFlatModifier(PlayerStats.LessDex, 1);
            PlayerStats.AddPercentModifier(PlayerStats.Dexterity, -0.25f);
        }

        //sacrifice debuff, highest intensity
        if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 4)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Cursed with frailty, my health is forever reduced.";
            PlayerStats.AddFlatModifier(PlayerStats.FrailtyCurse, 1);
            PlayerStats.AddPercentMultModifier(PlayerStats.Health, -0.25f);
        }

        //sacrifice debuff, galaxy intensity
        if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 5)
        {
            textbox.text = "Sacrifice accepted... \n " +
            "Cursed with weakness, my strength is forever reduced.";
            PlayerStats.AddFlatModifier(PlayerStats.WeaknessCurse, 1);
            PlayerStats.AddPercentMultModifier(PlayerStats.Strength, -0.25f);
        }

        choiceMade = 2;
    }

    public void Option03()
    {
        choiceMade = 3;
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
    // Update is called once per frame
    void Update()
    {
        if (rue == null)
        {
            rue = GameObject.FindGameObjectWithTag("Player");
            SacrificeDatabase = rue.GetComponentInChildren<SacrificeDatabase>();
            PlayerStats = rue.GetComponent<PlayerStats>();
            player = rue.GetComponent<Player>();
            BossDoor = GetComponentInParent<BossdoorScript>();
            sacsize = BossDoor.sacrifice.Count;
            level = SceneManager.GetActiveScene().buildIndex;
            if (level > 5)
            {
                level -= 5;
            }
            
            return;
        }
        if (PlayerStats.NoSacrifice.Value > 0 && choice03.activeSelf == false)
        {
            choice03.SetActive(true);
            textbox.text = "You have enough guilt and regret...";
        }
        if (PlayerStats.NoSacrifice.Value <= 0 && choice01.activeSelf == false)
        {
            choice01.SetActive(true);
            choice02.SetActive(true);
        }

        //option01
        if (sacsize == BossDoor.sacrifice.Count && option01Done == false)
        {
            sacroll1 = Random.Range(0, sacsize);
            GetSacrifice(BossDoor.sacrifice[sacroll1].type, BossDoor.sacrifice[sacroll1].intensity, level);
            BossDoor.sacrifice.Remove(BossDoor.sacrifice[sacroll1]);
            option01Done = true;
            //Sacrifice common, low intensity
            if (sacrifice[0].description == "common" && sacrifice[0].intensity == 0)
            {
                int commontosacRoll = Random.Range(0, (BossDoor.commonsac.Count));
                commontosac = BossDoor.commonsac[commontosacRoll];
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
            if (sacrifice[0].description == "common" && sacrifice[0].intensity == 1)
            {
                int commontosacRoll = Random.Range(0, (BossDoor.commonsac.Count));
                commontosac = BossDoor.commonsac[commontosacRoll];
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
            if (sacrifice[0].description == "rare" && sacrifice[0].intensity == 2)
            {
                int raretosacRoll = Random.Range(0, (BossDoor.raresac.Count));
                raretosac = BossDoor.raresac[raretosacRoll];
                option01text.text = "Sacrifice your " + raretosac.title;
                return;
            }

            //Sacrifice rare, med intensity
            if (sacrifice[0].description == "rare" && sacrifice[0].intensity == 3)
            {
                int raretosacRoll = Random.Range(0, (BossDoor.raresac.Count));
                raretosac = BossDoor.raresac[raretosacRoll];
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
            if (sacrifice[0].description == "rare" && sacrifice[0].intensity == 4)
            {
                int raretosacRoll = Random.Range(0, (BossDoor.raresac.Count));
                raretosac = BossDoor.raresac[raretosacRoll];
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
            if (sacrifice[0].description == "white")
            {
                white = PlayerStats.Loot.Find(des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option01text.text = "Sacrifice your White light particle";
                return;
            }

            //Sacrifice violet light particle
            if (sacrifice[0].description == "violet")
            {
                violet = PlayerStats.Loot.Find(des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option01text.text = "Sacrifice your Violet light particle";
                return;
            }

            //Sacrifice legendary, low intensity
            if (sacrifice[0].description == "legendary" && sacrifice[0].intensity == 7)
            {
                int legendarytosacRoll = Random.Range(0, (BossDoor.legendarysac.Count));
                legendarytosac = BossDoor.legendarysac[legendarytosacRoll];
                option01text.text = "Sacrifice your " + legendarytosac.title;
                return;
            }

            //Sacrifice legendary, med intensity
            if (sacrifice[0].description == "legendary" && sacrifice[0].intensity == 8)
            {
                int legendarytosacRoll = Random.Range(0, (BossDoor.legendarysac.Count));
                legendarytosac = BossDoor.legendarysac[legendarytosacRoll];
                BossDoor.legendarysac.Remove(legendarytosac);
                int legendarytosac2Roll = Random.Range(0, (BossDoor.legendarysac.Count));
                legendarytosac2 = BossDoor.legendarysac[legendarytosac2Roll];
                option01text.text = "Sacrifice" + legendarytosac.title + " and " + legendarytosac2.title;
                return;
            }

            //Sacrifice ancient
            if (sacrifice[0].description == "ancient")
            {
                int ancienttosacRoll = Random.Range(0, (BossDoor.ancientsac.Count));
                ancienttosac = BossDoor.ancientsac[ancienttosacRoll];
                option01text.text = "Sacrifice your " + ancienttosac.title;
                return;
            }

            //Sacrifice ancient and legendary
            if (sacrifice[0].description == "legendary ancient")
            {
                int legendaryancienttosacRoll = Random.Range(0, (BossDoor.legendaryancientsac.Count));
                legendaryancienttosac = BossDoor.legendaryancientsac[legendaryancienttosacRoll];
                if (legendaryancienttosac.tier != "Legendary Loot")
                {
                    BossDoor.legendaryancientsac.RemoveAll(x => x.tier == "Ancient Loot");
                    legendaryancienttosacRoll = Random.Range(0, (BossDoor.legendaryancientsac.Count));
                    legendaryancienttosac2 = BossDoor.legendaryancientsac[legendaryancienttosacRoll];
                    option01text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }

                if (legendaryancienttosac.title != "Ancient Loot")
                {
                    BossDoor.legendaryancientsac.RemoveAll(x => x.tier == "Legendary Loot");
                    legendaryancienttosacRoll = Random.Range(0, (BossDoor.legendaryancientsac.Count));
                    legendaryancienttosac2 = BossDoor.legendaryancientsac[legendaryancienttosacRoll];
                    option01text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }
                return;
            }

            //Sacrifice potions, low intensity
            if (sacrifice[0].description == "potion" && sacrifice[0].intensity == 0)
            {
                option01text.text = "Sacrifice half of your potions";
                BossDoor.sacrifice.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice potions, med intensity
            if (sacrifice[0].description == "potion" && sacrifice[0].intensity == 1)
            {
                option01text.text = "Sacrifice all your potions";
                BossDoor.sacrifice.RemoveAll(sac => sac.description.Equals("potion"));
                return;
            }

            //Sacrifice mechanic, low intensity
            if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 0)
            {
                option01text.text = "Sacrifice your eye.";
                return;
            }

            //Sacrifice mechanic, med intensity
            if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 1)
            {
                option01text.text = "Sacrifice a tendon.";
                return;
            }

            //Sacrifice mechanic, high intensity
            if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 2)
            {
                option01text.text = "Sacrifice your arm.";
                return;
            }

            //Sacrifice mechanic, higher intensity
            if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 3)
            {
                option01text.text = "Sacrifice your sword.";
                return;
            }

            //Sacrifice mechanic, highest intensity
            if (sacrifice[0].description == "mechanic" && sacrifice[0].intensity == 4)
            {
                option01text.text = "Sacrifice your crossbow.";
                return;
            }

            //Sacrifice debuff, low intensity
            if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 0)
            {
                option01text.text = "Sacrifice your health.";
                return;
            }

            //Sacrifice debuff, med intensity
            if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 1)
            {
                option01text.text = "Sacrifice your swiftness.";
                return;
            }

            //Sacrifice debuff, high intensity
            if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 2)
            {
                option01text.text = "Sacrifice your strength.";
                return;
            }

            //Sacrifice debuff, higher intensity
            if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 3)
            {
                option01text.text = "Sacrifice your dexterity.";
                return;
            }

            //Sacrifice debuff, highest intensity
            if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 4)
            {
                option01text.text = "Suffer the Curse of Frailty. \n Health permanently reduced by 50%";
                return;
            }

            //Sacrifice debuff, galaxy intensity
            if (sacrifice[0].description == "debuff" && sacrifice[0].intensity == 5)
            {
                option01text.text = "Suffer the Curse of Weakness. \n Strength permanently reduced by 25%";
                return;
            }
            
            return;
        }
        //option02
        if (sacsize == BossDoor.sacrifice.Count + 1)
        {
            sacsize = BossDoor.sacrifice.Count;
            GetSacrifice(BossDoor.sacrifice[sacroll2].type, BossDoor.sacrifice[sacroll2].intensity, level);
            sacroll2 = Random.Range(0, sacsize);
            if (sacrifice[1].description == "common" && sacrifice[1].intensity == 0)
            {
                int commontosacRoll = Random.Range(0, (BossDoor.commonsac.Count));
                commontosac = BossDoor.commonsac[commontosacRoll];
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
            if (sacrifice[1].description == "common" && sacrifice[1].intensity == 1)
            {
                int commontosacRoll = Random.Range(0, (BossDoor.commonsac.Count));
                commontosac = BossDoor.commonsac[commontosacRoll];
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
            if (sacrifice[1].description == "rare" && sacrifice[1].intensity == 2)
            {
                int raretosacRoll = Random.Range(0, (BossDoor.raresac.Count));
                raretosac = BossDoor.raresac[raretosacRoll];
                option02text.text = "Sacrifice your " + raretosac.title;
                return;
            }

            //Sacrifice rare, med intensity
            if (sacrifice[1].description == "rare" && sacrifice[1].intensity == 3)
            {
                int raretosacRoll = Random.Range(0, (BossDoor.raresac.Count));
                raretosac = BossDoor.raresac[raretosacRoll];
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
            if (sacrifice[1].description == "rare" && sacrifice[1].intensity == 4)
            {
                int raretosacRoll = Random.Range(0, (BossDoor.raresac.Count));
                raretosac = BossDoor.raresac[raretosacRoll];
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
            if (sacrifice[1].description == "white")
            {
                white = PlayerStats.Loot.Find(des => des.title.Equals("White light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option02text.text = "Sacrifice your White light particle";
                return;
            }

            //Sacrifice violet light particle
            if (sacrifice[1].description == "violet")
            {
                violet = PlayerStats.Loot.Find(des => des.title.Equals("Violet light particle", System.StringComparison.InvariantCultureIgnoreCase));
                option02text.text = "Sacrifice your Violet fruit";
                return;
            }

            //Sacrifice legendary, low intensity
            if (sacrifice[1].description == "legendary" && sacrifice[1].intensity == 7)
            {
                int legendarytosacRoll = Random.Range(0, (BossDoor.legendarysac.Count));
                legendarytosac = BossDoor.legendarysac[legendarytosacRoll];
                option02text.text = "Sacrifice your " + legendarytosac.title;
                return;
            }

            //Sacrifice legendary, med intensity
            if (sacrifice[1].description == "legendary" && sacrifice[1].intensity == 8)
            {
                int legendarytosacRoll = Random.Range(0, (BossDoor.legendarysac.Count));
                legendarytosac = BossDoor.legendarysac[legendarytosacRoll];
                BossDoor.legendarysac.Remove(legendarytosac);
                int legendarytosac2Roll = Random.Range(0, (BossDoor.legendarysac.Count));
                legendarytosac2 = BossDoor.legendarysac[legendarytosac2Roll];
                option02text.text = "Sacrifice" + legendarytosac.title + " and " + legendarytosac2.title;
                return;
            }

            //Sacrifice ancient
            if (sacrifice[1].description == "ancient")
            {
                int ancienttosacRoll = Random.Range(0, (BossDoor.ancientsac.Count));
                ancienttosac = BossDoor.ancientsac[ancienttosacRoll];
                option02text.text = "Sacrifice your " + ancienttosac.title;
                return;
            }

            //Sacrifice legendary and ancient
            if (sacrifice[1].description == "legendary ancient")
            {
                int legendaryancienttosacRoll = Random.Range(0, (BossDoor.legendaryancientsac.Count));
                legendaryancienttosac = BossDoor.legendaryancientsac[legendaryancienttosacRoll];
                print(legendaryancienttosac.tier);
                if (legendaryancienttosac.tier != "Legendary Loot")
                {
                    print("ancient");
                    BossDoor.legendaryancientsac.RemoveAll(x => x.tier == "Ancient Loot");
                    legendaryancienttosacRoll = Random.Range(0, (BossDoor.legendaryancientsac.Count));
                    legendaryancienttosac2 = BossDoor.legendaryancientsac[legendaryancienttosacRoll];
                    option02text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;
                }

                if (legendaryancienttosac.title != "Ancient Loot")
                {
                    print("legendary");
                    BossDoor.legendaryancientsac.RemoveAll(x => x.tier == "Legendary Loot");
                    legendaryancienttosacRoll = Random.Range(0, (BossDoor.legendaryancientsac.Count));
                    legendaryancienttosac2 = BossDoor.legendaryancientsac[legendaryancienttosacRoll];
                    option02text.text = "Sacrifice " + legendaryancienttosac.title + " and " + legendaryancienttosac2.title;
                    return;

                }

            }

            //Sacrifice potions, low intensity
            if (sacrifice[1].description == "potion" && sacrifice[1].intensity == 0)
            {
                option02text.text = "Sacrifice half of your potions";
                return;
            }

            //Sacrifice potions, med intensity
            if (sacrifice[1].description == "potion" && sacrifice[1].intensity == 1)
            {
                option02text.text = "Sacrifice all your potions";
                return;
            }

            //Sacrifice mechanic, low intensity
            if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 0)
            {
                option02text.text = "Sacrifice your eye.";
                return;
            }

            //Sacrifice mechanic, med intensity
            if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 1)
            {
                option02text.text = "Sacrifice a tendon.";
                return;
            }

            //Sacrifice mechanic, high intensity
            if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 2)
            {
                option02text.text = "Sacrifice your arm.";
                return;
            }

            //Sacrifice mechanic, higher intensity
            if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 3)
            {
                option02text.text = "Sacrifice your sword.";
                return;
            }

            //Sacrifice mechanic, highest intensity
            if (sacrifice[1].description == "mechanic" && sacrifice[1].intensity == 4)
            {
                option02text.text = "Sacrifice your crossbow.";
                return;
            }

            //Sacrifice debuff, low intensity
            if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 0)
            {
                option02text.text = "Sacrifice your health.";
                return;
            }

            //Sacrifice debuff, med intensity
            if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 1)
            {
                option02text.text = "Sacrifice your swiftness.";
                return;
            }

            //Sacrifice debuff, high intensity
            if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 2)
            {
                option02text.text = "Sacrifice your strength.";
                return;
            }

            //Sacrifice debuff, higher intensity
            if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 3)
            {
                option02text.text = "Sacrifice your dexterity.";
                return;
            }

            //Sacrifice debuff, highest intensity
            if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 4)
            {
                option02text.text = "Suffer the Curse of Frailty. \n Health permanently reduced by 50%";
                return;
            }

            //Sacrifice debuff, galaxy intensity
            if (sacrifice[1].description == "debuff" && sacrifice[1].intensity == 5)
            {
                option02text.text = "Suffer the Curse of Weakness. \n Strength permanently reduced by 25%";
                return;
            }
            return;
        }

        if (choiceMade >= 1)
        {
            choice01.SetActive(false);
            choice02.SetActive(false);
            if (choice03 != null)
            {
                choice03.SetActive(false);
            }
            
        }
    }
}