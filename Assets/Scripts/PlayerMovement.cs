using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float xInput;
    public float yInput;

    public float speed = 5f;
    public float rotationSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
        ApplyMovement();
        ApplyRotation();
    }

    void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    void ApplyMovement()
    {
        if(xInput != 0 || yInput != 0)
        {
            transform.position += new Vector3(xInput * speed * Time.deltaTime, yInput * speed * Time.deltaTime, transform.position.z);
        }
    }

    void ApplyRotation()
    {
       if(xInput == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -90);
        }
       else if(xInput == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, 90);
        }
       if(yInput == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, 0);
        }
       else if(yInput == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -180);
        }
    }
}
