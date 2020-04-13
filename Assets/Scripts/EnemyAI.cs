using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public GameObject aggroRange;
    public GameObject player;
    public GameObject sword;
    public GameObject attack;
    public bool closeEnough = false;
    public bool canAttack = false;
    public HealthSystem HealthSystem;
    public float Health;
    Vector2 direction;
    Vector2 walk;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 3f;
        HealthSystem = new HealthSystem(50);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Health = HealthSystem.GetHealth();
        
        if (closeEnough == false)
        {
            walk = (player.transform.position - transform.position).normalized * speed;
            rb2d.velocity = new Vector2(walk.x, walk.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag == "Sword")
        {
            Inventory inventory = player.GetComponent<Inventory>();
            HealthSystem.Damage(inventory.Strength.Value);
        }
    }
   
    public void Attack()
    {
        canAttack = false;
        direction = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        GameObject clonedObject = Instantiate(attack, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Destroy(clonedObject, 0.3f);
        Invoke("Check", 0.5f);
    }
    public void Check()
    {
        if (closeEnough == true && canAttack == true)
        {
            Invoke("Attack", 0.5f);
            print("1");
        }
        if (closeEnough == false || canAttack == false)
        {
            Invoke("Wait", 0.5f);
            print("2");
        }
    }
    public void Wait()
    {
        canAttack = true;
        print("3");
        Check();
    }
}
