using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform upStartDir, leftStartDir, rightStartDir, downStartDir;

    public float length;

    public LayerMask whatLayer;
    void Start()
    {
        length = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D upRay = Physics2D.Raycast(upStartDir.position, Vector2.right, length, whatLayer);
        RaycastHit2D leftRay = Physics2D.Raycast(leftStartDir.position, Vector2.up, length, whatLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(rightStartDir.position, Vector2.down, length, whatLayer);
        RaycastHit2D downRay = Physics2D.Raycast(downStartDir.position, Vector2.left, length, whatLayer);

        if (upRay)
        {
            //Debug.Log(upRay.collider.gameObject + " upRay", gameObject);
            if (upRay.collider.gameObject.CompareTag("Player"))
            {
                upRay.collider.gameObject.transform.position += new Vector3(0, 110, 0);
            }
            else
            {
                upRay.collider.gameObject.GetComponent<SwitchTilesAtDoor>().isThereDoor = true;
                upRay.collider.gameObject.GetComponent<SwitchTilesAtDoor>().SwitchTile();
            }
        }
        else if (leftRay)
        {
            //Debug.Log(leftRay.collider.gameObject + " leftRay", gameObject);
            if (leftRay.collider.gameObject.CompareTag("Player"))
            {
                leftRay.collider.gameObject.transform.position -= new Vector3(190, 0, 0);
            }
        }
        else if (rightRay)
        {
            //Debug.Log(rightRay.collider.gameObject + " rightRay", gameObject);
            if (rightRay.collider.gameObject.CompareTag("Player"))
            {
                rightRay.collider.gameObject.transform.position += new Vector3(190, 0, 0);
            }
        }
        else if (downRay)
        {
            //Debug.Log(downRay.collider.gameObject + " downRay", gameObject);
            if (downRay.collider.gameObject.CompareTag("Player"))
            {
                downRay.collider.gameObject.transform.position -= new Vector3(0, 110, 0);
            }
            else
            {
                downRay.collider.gameObject.GetComponent<SwitchTilesAtDoor>().isThereDoor = true;
                downRay.collider.gameObject.GetComponent<SwitchTilesAtDoor>().SwitchTile();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(upStartDir.position, Vector2.right * length, Color.red);
        Debug.DrawRay(leftStartDir.position, Vector2.up * length, Color.blue);
        Debug.DrawRay(rightStartDir.position, Vector2.down * length, Color.black);
        Debug.DrawRay(downStartDir.position, Vector2.left * length, Color.green);
    }
}
