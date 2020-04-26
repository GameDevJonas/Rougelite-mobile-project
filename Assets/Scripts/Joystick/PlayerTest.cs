using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private float inputX, inputY;
    public float speed = 4f;

    public FixedJoystick joystick;

    private Rigidbody2D rb;
    void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void GetInputs()
    {
        inputX = joystick.Horizontal;
        inputY = joystick.Vertical;
    }

    public void ApplyMovement()
    {
        if(inputX != 0 || inputY != 0)
        {
            rb.velocity = new Vector2(inputX * speed * Time.deltaTime, inputY * speed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
