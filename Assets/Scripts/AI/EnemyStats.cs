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

    public EnemyStats(EnemyType enemyType, int level) //By using this public class, through inputting enemy type, stats are given to enemy scripts.
    {
        if (enemyType == EnemyType.trash)
        {
            if (level == 1 || level == 6)
            {
                health = 50;
                damage = 10;
                speed = 30;

                int[] table = { none, potiondropRange, commondropRange, raredropRange, legendarydropRange, ancientdropRange }; //available items to drop.
                Table = table; //chance to drop.
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
                
                int[] thisTable = { none, potiondropRange, commondropRange, raredropRange, legendarydropRange, ancientdropRange };
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
            if (level == 2 || level == 7)
            {
                health = 100;
                damage = 20;
                speed = 35;

                int[] table = { none, potiondropRange, commondropRange, raredropRange, legendarydropRange, ancientdropRange };
                Table = table;
                commondropRange = 20;
                raredropRange = 10;
                legendarydropRange = 0;
                ancientdropRange = 0;
                potiondropRange = 30;
                none = 40;

                blocksLight = false;
                hidesInDark = false;
                hidesInLight = false;

                this.health = 100;
                this.damage = 20;
                this.speed = 35;
                
                int[] thisTable = { none, potiondropRange, commondropRange, raredropRange, legendarydropRange, ancientdropRange };
                this.Table = thisTable;
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
            if (level == 3 || level == 8)
            {
                health = 200;
                damage = 50;
                speed = 30;

                int[] table = { potiondropRange, none, commondropRange, raredropRange, legendarydropRange, ancientdropRange };
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

                this.health = 200;
                this.damage = 50;
                this.speed = 30;

                int[] thisTable = { potiondropRange, none, commondropRange, raredropRange, legendarydropRange, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 20;
                this.raredropRange = 14;
                this.legendarydropRange = 1;
                this.ancientdropRange = 0;
                this.potiondropRange = 35;
                this.none = 30;

                this.blocksLight = false;
                this.hidesInDark = true;
                this.hidesInLight = false;
            }
            if (level == 4 || level == 9)
            {
                health = 400;
                damage = 100;
                speed = 40;

                int[] table = { potiondropRange, raredropRange, none, commondropRange, legendarydropRange, ancientdropRange };
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

                this.health = 400;
                this.damage = 100;
                this.speed = 40;

                int[] thisTable = { potiondropRange, raredropRange, none, commondropRange, legendarydropRange, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 13;
                this.raredropRange = 25;
                this.legendarydropRange = 2;
                this.ancientdropRange = 0;
                this.potiondropRange = 40;
                this.none = 20;

                this.blocksLight = true;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 5 || level == 10)
            {
                health = 600;
                damage = 200;
                speed = 35;

                int[] table = { potiondropRange, raredropRange, commondropRange, none, legendarydropRange, ancientdropRange };
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

                this.health = 600;
                this.damage = 200;
                this.speed = 35;

                int[] thisTable = { potiondropRange, raredropRange, commondropRange, none, legendarydropRange, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 12;
                this.raredropRange = 30;
                this.legendarydropRange = 3;
                this.ancientdropRange = 0;
                this.potiondropRange = 45;
                this.none = 10;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = true;
            }
        }
        if (enemyType == EnemyType.melee)
        {
            if (level == 1 || level == 6)
            {
                health = 100;
                damage = 20;
                speed = 30;

                int[] table = { potiondropRange, commondropRange, raredropRange, legendarydropRange, none, ancientdropRange };
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

                this.health = 100;
                this.damage = 20;
                this.speed = 30;

                int[] thisTable = { potiondropRange, commondropRange, raredropRange, legendarydropRange, none, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 30;
                this.raredropRange = 19;
                this.legendarydropRange = 1;
                this.ancientdropRange = 0;
                this.potiondropRange = 50;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 2 || level == 7)
            {
                health = 400;
                damage = 40;
                speed = 35;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
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

                this.health = 400;
                this.damage = 40;
                this.speed = 35;

                int[] thisTable = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 25;
                this.raredropRange = 28;
                this.legendarydropRange = 2;
                this.ancientdropRange = 0;
                this.potiondropRange = 45;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 3 || level == 8)
            {
                health = 600;
                damage = 80;
                speed = 40;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
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

                this.health = 600;
                this.damage = 80;
                this.speed = 40;

                int[] thisTable = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 20;
                this.raredropRange = 37;
                this.legendarydropRange = 3;
                this.ancientdropRange = 0;
                this.potiondropRange = 40;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = true;
                this.hidesInLight = false;
            }
            if (level == 4 || level == 9)
            {
                health = 1200;
                damage = 100;
                speed = 35;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
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

                this.health = 1200;
                this.damage = 100;
                this.speed = 35;

                int[] thisTable = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
                this.Table = thisTable;
                this.commondropRange = 15;
                this.raredropRange = 45;
                this.legendarydropRange = 4;
                this.ancientdropRange = 1;
                this.potiondropRange = 35;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 5 || level == 10)
            {
                health = 1500;
                damage = 150;
                speed = 40;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
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

                this.health = 1500;
                this.damage = 150;
                this.speed = 40;

                int[] thisTable = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
                this.Table = thisTable;
                this.commondropRange = 10;
                this.raredropRange = 54;
                this.legendarydropRange = 5;
                this.ancientdropRange = 1;
                this.potiondropRange = 30;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = true;
                this.hidesInLight = false;
            }
        }
        if (enemyType == EnemyType.ranged)
        {
            if (level == 1 || level == 6)
            {
                health = 75;
                damage = 25;
                speed = 30;

                int[] table = { potiondropRange, commondropRange, raredropRange, legendarydropRange, none, ancientdropRange };
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

                this.health = 75;
                this.damage = 25;
                this.speed = 30;

                int[] thisTable = { potiondropRange, commondropRange, raredropRange, legendarydropRange, none, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 30;
                this.raredropRange = 19;
                this.legendarydropRange = 1;
                this.ancientdropRange = 0;
                this.potiondropRange = 50;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 2 || level == 7)
            {
                health = 300;
                damage = 40;
                speed = 25;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
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

                this.health = 300;
                this.damage = 40;
                this.speed = 25;

                int[] thisTable = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 25;
                this.raredropRange = 28;
                this.legendarydropRange = 2;
                this.ancientdropRange = 0;
                this.potiondropRange = 45;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 3 || level == 8)
            {
                health = 400;
                damage = 90;
                speed = 30;

                int[] table = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
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

                this.health = 400;
                this.damage = 90;
                this.speed = 30;

                int[] thisTable = { potiondropRange, raredropRange, commondropRange, legendarydropRange, none, ancientdropRange };
                this.Table = thisTable;
                this.commondropRange = 20;
                this.raredropRange = 37;
                this.legendarydropRange = 3;
                this.ancientdropRange = 0;
                this.potiondropRange = 40;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = true;
                this.hidesInLight = false;
            }
            if (level == 4 || level == 9)
            {
                health = 1000;
                damage = 120;
                speed = 25;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
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

                this.health = 1000;
                this.damage = 120;
                this.speed = 25;

                int[] thisTable = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
                this.Table = thisTable;
                this.commondropRange = 15;
                this.raredropRange = 45;
                this.legendarydropRange = 4;
                this.ancientdropRange = 1;
                this.potiondropRange = 35;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 5 || level == 10)
            {
                health = 1300;
                damage = 180;
                speed = 30;

                int[] table = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
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

                this.health = 1300;
                this.damage = 180;
                this.speed = 30;

                int[] thisTable = { raredropRange, potiondropRange, commondropRange, legendarydropRange, ancientdropRange, none };
                this.Table = thisTable;
                this.commondropRange = 10;
                this.raredropRange = 54;
                this.legendarydropRange = 5;
                this.ancientdropRange = 1;
                this.potiondropRange = 30;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = true;
            }
        }
        if (enemyType == EnemyType.elite)
        {
            if (level == 1 || level == 6)
            {
                health = 125;
                damage = 30;
                speed = 30;

                int[] table = { raredropRange, legendarydropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 125;
                this.damage = 30;
                this.speed = 30;

                int[] thisTable = { raredropRange, legendarydropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 50;
                this.legendarydropRange = 44;
                this.ancientdropRange = 6;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 2 || level == 7)
            {
                health = 300;
                damage = 60;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 300;
                this.damage = 60;
                this.speed = 35;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 45;
                this.legendarydropRange = 48;
                this.ancientdropRange = 7;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 3 || level == 8)
            {
                health = 700;
                damage = 100;
                speed = 40;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 700;
                this.damage = 100;
                this.speed = 40;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 40;
                this.legendarydropRange = 52;
                this.ancientdropRange = 8;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = true;
                this.hidesInLight = false;
            }
            if (level == 4 || level == 9)
            {
                health = 1400;
                damage = 120;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 1400;
                this.damage = 120;
                this.speed = 35;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 35;
                this.legendarydropRange = 56;
                this.ancientdropRange = 9;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 5 || level == 10)
            {
                health = 1800;
                damage = 180;
                speed = 40;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 1800;
                this.damage = 180;
                this.speed = 40;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 30;
                this.legendarydropRange = 60;
                this.ancientdropRange = 10;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
        }
        if (enemyType == EnemyType.boss)
        {
            if (level == 1 || level == 6)
            {
                health = 250;
                damage = 50;
                speed = 30;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 250;
                this.damage = 50;
                this.speed = 30;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 35;
                this.legendarydropRange = 50;
                this.ancientdropRange = 15;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 2 || level == 7)
            {
                health = 1000;
                damage = 100;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 1000;
                this.damage = 100;
                this.speed = 35;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 35;
                this.legendarydropRange = 45;
                this.ancientdropRange = 20;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 3 || level == 8)
            {
                health = 2000;
                damage = 150;
                speed = 35;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 2000;
                this.damage = 150;
                this.speed = 35;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 30;
                this.legendarydropRange = 45;
                this.ancientdropRange = 25;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = false;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 4 || level == 9)
            {
                health = 3500;
                damage = 200;
                speed = 40;

                int[] table = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
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

                this.health = 3500;
                this.damage = 200;
                this.speed = 40;

                int[] thisTable = { legendarydropRange, raredropRange, ancientdropRange, none, commondropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 35;
                this.legendarydropRange = 40;
                this.ancientdropRange = 30;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = false;
                this.hidesInLight = false;
            }
            if (level == 5 || level == 10)
            {
                health = 7000;
                damage = 500;
                speed = 45;

                int[] table = { ancientdropRange, none, commondropRange, raredropRange, legendarydropRange, potiondropRange };
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

                this.health = 7000;
                this.damage = 500;
                this.speed = 45;

                int[] thisTable = { ancientdropRange, none, commondropRange, raredropRange, legendarydropRange, potiondropRange };
                this.Table = thisTable;
                this.commondropRange = 0;
                this.raredropRange = 0;
                this.legendarydropRange = 0;
                this.ancientdropRange = 100;
                this.potiondropRange = 0;
                this.none = 0;

                this.blocksLight = true;
                this.hidesInDark = true;
                this.hidesInLight = false;
            }
        }
    }
}
