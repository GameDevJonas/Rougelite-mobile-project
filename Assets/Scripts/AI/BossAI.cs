using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    private int drop;
    public GameObject aggroRange;
    public GameObject player;
    public GameObject sword;
    public GameObject attack;
    public GameObject Loot;
    public bool closeEnough = false;
    public bool canAttack = false;
    public bool waiting;
    public bool BackOff = false;
    public HealthSystem HealthSystem;
    public float Health;
    Vector2 direction;
    Vector2 walk;
    Vector2 backOff;

    public MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 40f;
        HealthSystem = new HealthSystem(100);
        player = GameObject.FindGameObjectWithTag("Player");
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Health = HealthSystem.GetHealth();
        if (Health == 0)//If dead
        {
            menuManager.fromBoss = true;
            menuManager.ToStartScreen();
            drop = Random.Range(-1, 2);
        if (drop == 0)
            {
                Destroy(gameObject);
            }
        if (drop == 1)
            {
                GameObject lootDrop = Instantiate(Loot, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            }
        if (closeEnough == false && waiting == false)
        {
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            walk = (player.transform.position - transform.position).normalized * speed;
            rb2d.velocity = new Vector2(walk.x, walk.y);
        }
        if (closeEnough == true || waiting == true || BackOff == true && closeEnough == true)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (BackOff == true && closeEnough == false)
        {
            rb2d.velocity = new Vector2(backOff.x, backOff.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            backOff = (-collision.gameObject.transform.position + transform.position).normalized * speed;
            BackOff = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BackOff = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag == "Sword")
        {
            PlayerStats playerstats = player.GetComponent<PlayerStats>();
            HealthSystem.Damage(playerstats.Strength.Value);
        }
        
    }

    public void Attack()
    {
        waiting = true;
        canAttack = false;
        direction = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        GameObject clonedObject = Instantiate(attack, transform.position, Quaternion.AngleAxis(angle, Vector3.forward), transform);
        Destroy(clonedObject, 0.3f);
        Invoke("Check", 0.5f);
    }
    public void Check()
    {
        
        if (closeEnough == true && canAttack == true)
        {
            waiting = true;
            Invoke("Attack", 0.5f);
        }
        if (closeEnough == false || canAttack == false)
        {
            waiting = false;
            Invoke("Wait", 0.5f);
        }
    }
    public void Wait()
    {
        waiting = true;
        canAttack = true;
        Check();
    }

    public void Damage()
    {
        PlayerStats playerstats = player.GetComponent<PlayerStats>();
        HealthSystem.Damage(playerstats.Strength.Value);
    }
}
