using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Player : MonoBehaviour
{
    public enum WeaponState { sword, bow };
    public WeaponState weaponInUse = WeaponState.sword;
    public TextMeshProUGUI debugWeaponState;

    private Rigidbody2D rb;

    public Animator anim;

    public FixedJoystick joystick;

    public GameObject sword;

    public Button swordAttack;

    public float maxHealth;
    public float currentHealth;
    public float speed;
    public float attackspeed;
    public int potion = 0;
    public float potionPotency;
    public float rotationSpeed = 5f;
    public float xInput;
    public float yInput;
    public int weaponState;

    public HealthSystem HealthSystem = new HealthSystem(50);
    public Healthbar healthbar;

    public bool useTouch;
    public bool attack;
    public bool canMove = true;
    public bool canAttack = true;
    public bool canHeal = true;

    public MenuManager menuManager;

    public string dir;
    public GameObject arrow;
    public float shootSpeed;
    public float shootForce;
    public Transform shootPoint;

    public GameObject currentRoom;

    public AstarStarter aStarManager;

    void Awake()
    {
        debugWeaponState.text = "sword";

        anim = GetComponentInChildren<Animator>();

        shootPoint = GameObject.FindGameObjectWithTag("ShootPoint").transform;

        swordAttack = GameObject.FindGameObjectWithTag("SwordButton").GetComponent<Button>();

        joystick = FindObjectOfType<FixedJoystick>();

        PlayerStats playerstats = GetComponent<PlayerStats>();

        rb = GetComponent<Rigidbody2D>();
        menuManager = FindObjectOfType<MenuManager>();
        UpdateStats();
        dir = "U";
#if UNITY_EDITOR
        useTouch = false;
#else
        useTouch = true;
#endif
    }

    public void StartPosition(Vector3 startPos)
    {
        aStarManager = GameObject.FindGameObjectWithTag("RoomRoot").GetComponent<AstarStarter>();
        transform.position = startPos;
    }

    void Update()
    {
        CheckInput();
        ApplyRotation();
        //SwordAttack();
        Anims();
        currentHealth = HealthSystem.GetHealth();
        healthbar.SetHealth(currentHealth);


        if (canMove == false)
        {
            Invoke("MovementLock", 0.2f);
        }
        if (currentHealth <= 0)
        {
            if (menuManager != null)
            {
                menuManager.ToAlphaLevel();
            }
            //RestartScene();
        }

    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void UpdateStats()
    {
        PlayerStats playerstats = GetComponent<PlayerStats>();
        speed = playerstats.MovementSpeed.Value * 5;
        attackspeed = (100f - playerstats.Dexterity.Value) / 100f;
        shootSpeed = (100f - playerstats.Dexterity.Value) / 100f;
        HealthSystem.ModMaxHealth(playerstats.Health.Value);
        maxHealth = playerstats.Health.Value;
        healthbar.SetMaxHealth(maxHealth);
        potionPotency = playerstats.PotionPotency.Value;
    }

    public void UsePotion()
    {
        if (potion > 0 && canHeal == true)
        {
            canMove = false;
            canHeal = false;
            Invoke("MovementLock", 0.2f);
            Invoke("CanHeal", 0.3f);
            potion -= 1;
            HealthSystem.Heal((maxHealth / 2.5f) + potionPotency);
        }
        else
        {
            return;
        }
    }

    void CheckInput()
    {
        if (useTouch)
        {
            xInput = joystick.Horizontal;
            yInput = joystick.Vertical;
        }
        else
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetKey(KeyCode.Space) && !useTouch)
        {
            DoAnAttack();
        }
    }

    public void Anims()
    {
        //Anim set
        anim.SetInteger("X_Input", Mathf.RoundToInt(xInput));
        anim.SetInteger("Y_Input", Mathf.RoundToInt(yInput));
        if (xInput == 0 && yInput == 0)
        {
            anim.SetBool("IsIdle", true);
        }
        else anim.SetBool("IsIdle", false);

        if (weaponInUse == WeaponState.sword)
        {
            weaponState = 1;
        }
        else if (weaponInUse == WeaponState.bow)
        {
            weaponState = 2;
        }

        Animator switchAnim = GameObject.FindGameObjectWithTag("WeaponSwitchButton").GetComponent<Animator>();
        switchAnim.SetInteger("WeaponState", weaponState);
    }

    void ApplyMovement()
    {
        if (xInput != 0 && (canMove == true) || yInput != 0 && (canMove == true))
        {
            //transform.position += new Vector3(xInput * speed * Time.deltaTime, yInput * speed * Time.deltaTime, transform.position.z);

            Vector3 move = new Vector3(xInput * speed, yInput * speed, transform.position.z);

            transform.position += Vector3.ClampMagnitude(move, speed) * Time.deltaTime;
        }
    }

    void ApplyRotation()
    {
        if (xInput == 1 && (canMove == true)) //Right
        {
            dir = "R";
        }
        else if (xInput == -1 && (canMove == true)) //Left
        {
            dir = "L";
        }
        if (yInput == 1 && (canMove == true)) //Up
        {
            dir = "U";
        }
        else if (yInput == -1 && (canMove == true)) //Down
        {
            dir = "D";
        }
        if ((xInput == 1 && yInput == 1) && (canMove == true)) //Up right
        {
            dir = "UR";
        }
        else if ((xInput == -1 && yInput == -1) && (canMove == true)) //Down left
        {
            dir = "DL";
        }
        if ((xInput == 1 && yInput == -1) && (canMove == true)) //Down right
        {
            dir = "DR";
        }
        if ((xInput == -1 && yInput == 1) && (canMove == true)) //Up left
        {
            dir = "UL";
        }
    }

    public void SwitchWeapon()
    {
        if (weaponInUse == WeaponState.sword)
        {
            debugWeaponState.text = "bow";
            weaponInUse = WeaponState.bow;
        }
        else if (weaponInUse == WeaponState.bow)
        {
            debugWeaponState.text = "sword";
            weaponInUse = WeaponState.sword;
        }
    }

    public void DoAnAttack()
    {
        switch (weaponInUse)
        {
            case WeaponState.sword:
                SwordAttack();
                break;
            case WeaponState.bow:
                BowAttack();
                break;
        }
    }

    public void SwordAttack()
    {
        if ((canAttack == true))

        {
            GameObject clonedObject = Instantiate(sword, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z), transform);
            canMove = false;
            canAttack = false;
            Destroy(clonedObject, 0.2f);
            swordAttack.interactable = false;
            if (canAttack == false)
            {
                Invoke("AttackLock", attackspeed);
                Invoke("MovementLock", attackspeed);
            }
        }
    }

    public void BowAttack()
    {
        if (canAttack)
        {
            GameObject arrowClone = Instantiate(arrow, shootPoint.position, Quaternion.identity);
            arrowClone.GetComponent<Arrow>().ShootyShoot(dir);
            canMove = false;
            canAttack = false;
            //Destroy(arrowClone, 5f);
            if (canAttack == false)
            {
                Invoke("AttackLock", shootSpeed);
                Invoke("MovementLock", attackspeed);
            }
        }
    }

    void MovementLock()
    {
        canMove = true;
    }
    void AttackLock()
    {
        canAttack = true;
        swordAttack.interactable = true;
    }
    void CanHeal()
    {
        canHeal = true;
    }
    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RoomRoot" && collision.GetComponent<RoomInstance>())
        {
            currentRoom = collision.gameObject;
            aStarManager.DoTheScan(currentRoom);
        }
    }
}
