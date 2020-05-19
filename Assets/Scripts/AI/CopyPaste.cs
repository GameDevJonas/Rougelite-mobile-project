/*Copy/Paste Stats after Class : MonoBehavior

public EnemyStats EnemyStats;

public GameObject commonLoot;
public GameObject rareLoot;
public GameObject legendaryLoot;
public GameObject ancientLoot;
public GameObject potion;

public HealthSystem HealthSystem;

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
public int randomNumber;
public int lootTotal;

public bool blocksLight;
public bool hidesInDark;
public bool hidesInLight;

public int level;

public EnemyType thisType;

public bool dropping = false;




     //Copy/Paste stats inside Start()

        thisType = EnemyType.trash; //Important, set type to EnemyType.trash, EnemyType.melee, EnemyType.ranged, EnemyType.elite or EnemyType.boss.

        level = SceneManager.GetActiveScene().buildIndex;

        EnemyStats = new EnemyStats(thisType, level);


        health = EnemyStats.health;
        damage = EnemyStats.damage;
        speed = EnemyStats.speed;
        HealthSystem = new HealthSystem(health);


        Table = EnemyStats.Table;
        commondropRange = EnemyStats.commondropRange;
        raredropRange = EnemyStats.raredropRange;
        legendarydropRange = EnemyStats.legendarydropRange;
        ancientdropRange = EnemyStats.ancientdropRange;
        potiondropRange = EnemyStats.potiondropRange;
        none = EnemyStats.none;
        lootTotal = EnemyStats.lootTotal;

        blocksLight = EnemyStats.blocksLight;
        hidesInDark = EnemyStats.hidesInDark;
        hidesInLight = EnemyStats.hidesInLight;




        
   Copy/Paste method in other scripts and use DropLootAndDie() after hp reaches zero or death animations are done.    
    
    private void DropLootAndDie()
    {


        foreach (var item in Table)
        {
            lootTotal += item;
            dropping = true;
        }
        randomNumber = Random.Range(0, (lootTotal + 1));


        foreach (var weight in Table)
        {
            if (randomNumber <= weight)
            {


                if (weight == commondropRange)
                {
                    Instantiate<GameObject>(commonLoot, transform);
                    Destroy(this.gameObject);
                    return;

                }

                if (weight == raredropRange)
                {
                    Instantiate<GameObject>(rareLoot, transform);
                    Destroy(this.gameObject);
                    return;
                }

                if (weight == legendarydropRange)
                {
                    Instantiate<GameObject>(legendaryLoot, transform);
                    Destroy(this.gameObject);
                    return;
                }

                if (weight == ancientdropRange)
                {
                    Instantiate<GameObject>(ancientLoot, transform);
                    Destroy(this.gameObject);
                    return;
                }

                if (weight == potiondropRange)
                {
                    Instantiate<GameObject>(potion, transform);
                    Destroy(this.gameObject);
                    return;
                }

                if (weight == none)
                {
                    Destroy(this.gameObject);
                    return;
                }
            }

            else

            {
                randomNumber -= weight;
            }
        }

    }

    
     */

