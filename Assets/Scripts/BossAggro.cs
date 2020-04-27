using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAggro : MonoBehaviour
{
    private BossAI parent;

    void Start()
    {
        parent = transform.parent.GetComponent<BossAI>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            parent.closeEnough = true;
            if (parent.canAttack == false)
            {
                parent.Wait();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            parent.closeEnough = false;
        }
    }
}
