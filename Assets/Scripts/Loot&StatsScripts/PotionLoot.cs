using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLoot : MonoBehaviour
{
    private bool IsInRange;
    private bool pickup = false;
    public GameObject player;

    public GameObject potionSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange == true)
        {
            GiveItem();
        }
    }
    private void GiveItem()
    {
        player.GetComponent<Player>().potion += 1;
        
        Destroy(gameObject, 0);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !pickup)
        {
            pickup = true;
            IsInRange = true;
            GameObject sfx = Instantiate(potionSound, transform.position, Quaternion.identity);
            Destroy(sfx, .5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            pickup = false;
            IsInRange = false;
        }
    }
}
