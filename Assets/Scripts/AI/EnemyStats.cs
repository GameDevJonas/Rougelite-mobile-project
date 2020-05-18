using System;

public enum EnemyType { trash, melee, ranged, elite, boss }
public class EnemyStats
{
    public float health;
    public int damage;
    public int speed;

    public int[] Table;
    public int commondropRange;
    public int raredropRange;
    public int legendarydropRange;
    public int ancientdropRange;
    public int potiondropRange;
    public int none;
    public int lootTotal;

    public bool blocksLight;
    public bool hidesInDark;
    public bool hidesInLight;

    public int level;

    public EnemyType thisType;

    //test
    /*
    public int randomNumber;
    void Start()
    {
        foreach (var item in Table)
        {
            lootTotal += item;
        }

        randomNumber = Random.Range(0, lootTotal);

        foreach (var weight in Table)
        {
            if (randomNumber <= weight)
            {

            }
            else
            {
                randomNumber -= weight;
            }
        }
    }*/
    //test end
    public EnemyStats(EnemyType enemyType, int level)
    {
        if (enemyType == EnemyType.trash)
        {
            if (level == 1)
            {
                health = 50;
                damage = 10;
                speed = 30;

                int[] table = { none, potiondropRange, commondropRange, raredropRange };
                Table = table;
                commondropRange = 20;
                raredropRange = 5;
                legendarydropRange = 0;
                ancientdropRange = 0;
                potiondropRange = 25;
                none = 50;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;

                this.health = 50;
                this.damage = 10;
                this.speed = 30;
                
                int[] thisTable = { none, potiondropRange, commondropRange, raredropRange };
                this.Table = thisTable;
                this.commondropRange = 20;
                this.raredropRange = 5;
                this.legendarydropRange = 0;
                this.ancientdropRange = 0;
                this.potiondropRange = 25;
                this.none = 50;
                
                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 2)
            {
                this.health = 100;
                this.damage = 20;
                this.speed = 35;
                
                int[] table = { none, potiondropRange, commondropRange, raredropRange, legendarydropRange };
                this.Table = table;
                this.commondropRange = 20;
                this.raredropRange = 10;
                this.legendarydropRange = 0;
                this.ancientdropRange = 0;
                this.potiondropRange = 30;
                this.none = 40;
                
                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 3)
            {
                health = 200;
                damage = 50;
                speed = 30;

                int[] table = { potiondropRange, none, commondropRange, raredropRange, legendarydropRange };
                Table = table;
                commondropRange = 20;
                raredropRange = 14;
                legendarydropRange = 1;
                ancientdropRange = 0;
                potiondropRange = 35;
                none = 30;

                blocksLight = false;
                hidesInDark = true;
                hidesInLight = false;
            }
            if (level == 4)
            {
                health = 400;
                damage = 100;
                speed = 40;

                int[] table = { potiondropRange, raredropRange, none, commondropRange, legendarydropRange };
                Table = table;
                commondropRange = 13;
                raredropRange = 25;
                legendarydropRange = 2;
                ancientdropRange = 0;
                potiondropRange = 40;
                none = 20;

                blocksLight = true;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 5)
            {
                health = 600;
                damage = 200;
                speed = 35;

                int[] table = { potiondropRange, raredropRange, commondropRange, none, legendarydropRange };
                Table = table;
                commondropRange = 12;
                raredropRange = 30;
                legendarydropRange = 3;
                ancientdropRange = 0;
                potiondropRange = 45;
                none = 10;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = true;
            }
        }
        if (thisType == EnemyType.melee)
        {
            if (level == 1)
            {
                health = 100;
                damage = 20;
                speed = 30;

                int[] table = { potiondropRange, commondropRange, raredropRange, legendarydropRange };
                Table = table;
                commondropRange = 30;
                raredropRange = 19;
                legendarydropRange = 1;
                ancientdropRange = 0;
                potiondropRange = 50;
                none = 0;

                blocksLight = true;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 2)
            {
                health = 400;
                damage = 40;
                speed = 35;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange };
                Table = table;
                commondropRange = 25;
                raredropRange = 28;
                legendarydropRange = 2;
                ancientdropRange = 0;
                potiondropRange = 45;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 3)
            {
                health = 600;
                damage = 80;
                speed = 40;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange };
                Table = table;
                commondropRange = 20;
                raredropRange = 37;
                legendarydropRange = 3;
                ancientdropRange = 0;
                potiondropRange = 40;
                none = 0;

                blocksLight = false;
                hidesInDark = true;
                hidesInLight = false;
            }
            if (level == 4)
            {
                health = 1200;
                damage = 100;
                speed = 35;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange };
                Table = table;
                commondropRange = 15;
                raredropRange = 45;
                legendarydropRange = 4;
                ancientdropRange = 1;
                potiondropRange = 35;
                none = 0;

                blocksLight = true;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 5)
            {
                health = 1500;
                damage = 150;
                speed = 40;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange };
                Table = table;
                commondropRange = 10;
                raredropRange = 54;
                legendarydropRange = 5;
                ancientdropRange = 1;
                potiondropRange = 30;
                none = 0;

                blocksLight = true;
                hidesInDark = true;
                hidesInLight = false;
            }
        }
        if (thisType == EnemyType.ranged)
        {
            if (level == 1)
            {
                health = 75;
                damage = 25;
                speed = 30;

                int[] table = { potiondropRange, commondropRange, raredropRange, legendarydropRange };
                Table = table;
                commondropRange = 30;
                raredropRange = 19;
                legendarydropRange = 1;
                ancientdropRange = 0;
                potiondropRange = 50;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 2)
            {
                health = 300;
                damage = 40;
                speed = 25;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange };
                Table = table;
                commondropRange = 25;
                raredropRange = 28;
                legendarydropRange = 2;
                ancientdropRange = 0;
                potiondropRange = 45;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 3)
            {
                health = 400;
                damage = 90;
                speed = 30;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange };
                Table = table;
                commondropRange = 20;
                raredropRange = 37;
                legendarydropRange = 3;
                ancientdropRange = 0;
                potiondropRange = 40;
                none = 0;

                blocksLight = false;
                hidesInDark = true;
                hidesInLight = false;
            }
            if (level == 4)
            {
                health = 1000;
                damage = 120;
                speed = 25;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange };
                Table = table;
                commondropRange = 15;
                raredropRange = 45;
                legendarydropRange = 4;
                ancientdropRange = 1;
                potiondropRange = 35;
                none = 0;

                blocksLight = true;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 5)
            {
                health = 1300;
                damage = 180;
                speed = 30;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange };
                Table = table;
                commondropRange = 10;
                raredropRange = 54;
                legendarydropRange = 5;
                ancientdropRange = 1;
                potiondropRange = 30;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = true;
            }
        }
        if (thisType == EnemyType.elite)
        {
            if (level == 1)
            {
                health = 125;
                damage = 30;
                speed = 30;

                int[] table = { raredropRange, legendarydropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 50;
                legendarydropRange = 44;
                ancientdropRange = 6;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 2)
            {
                health = 300;
                damage = 60;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 45;
                legendarydropRange = 48;
                ancientdropRange = 7;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 3)
            {
                health = 700;
                damage = 100;
                speed = 40;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 40;
                legendarydropRange = 52;
                ancientdropRange = 8;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = true;
                hidesInLight = false;
            }
            if (level == 4)
            {
                health = 1400;
                damage = 120;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 35;
                legendarydropRange = 56;
                ancientdropRange = 9;
                potiondropRange = 0;
                none = 0;

                blocksLight = true;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 5)
            {
                health = 1800;
                damage = 180;
                speed = 40;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 30;
                legendarydropRange = 60;
                ancientdropRange = 10;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
        }
        if (thisType == EnemyType.boss)
        {
            if (level == 1)
            {
                health = 250;
                damage = 50;
                speed = 30;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 35;
                legendarydropRange = 50;
                ancientdropRange = 15;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 2)
            {
                health = 1000;
                damage = 100;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 35;
                legendarydropRange = 45;
                ancientdropRange = 20;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 3)
            {
                health = 2000;
                damage = 150;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 30;
                legendarydropRange = 45;
                ancientdropRange = 25;
                potiondropRange = 0;
                none = 0;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 4)
            {
                health = 3500;
                damage = 200;
                speed = 40;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 35;
                legendarydropRange = 40;
                ancientdropRange = 30;
                potiondropRange = 0;
                none = 0;

                blocksLight = true;
                hidesInDark = false;
                hidesInLight = false;
            }
            if (level == 5)
            {
                health = 7000;
                damage = 500;
                speed = 45;

                int[] table = { ancientdropRange };
                Table = table;
                commondropRange = 0;
                raredropRange = 0;
                legendarydropRange = 0;
                ancientdropRange = 100;
                potiondropRange = 0;
                none = 0;

                blocksLight = true;
                hidesInDark = true;
                hidesInLight = false;
            }
        }
    }
}
