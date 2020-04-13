using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public GameObject player;
    public GameObject attack;
    Vector2 direction;
    public bool closeEnough = false;
    Vector2 walk;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 3f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (closeEnough == false)
        {
            walk = (player.transform.position - transform.position).normalized * speed;
            rb2d.velocity = new Vector2(walk.x, walk.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            Invoke("Check", 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D Attackrange)
    {
        if (Attackrange.gameObject.tag == "Player")
        {
            closeEnough = true;
        }
    }
    private void OnTriggerExit2D(Collider2D Attackrange)
    {
        if (Attackrange.gameObject.tag == "Player")
        {
            closeEnough = false;
        }
    }
    private void Attack()
    {
        direction = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        GameObject clonedObject = Instantiate(attack, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Check();
        Destroy(clonedObject, 0.3f);
    }
    private void Check()
    {
        if (closeEnough == true)
        {
            Attack();
        }
        else return;
    }
}
