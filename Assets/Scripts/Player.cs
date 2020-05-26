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
    public float knockBackDebug;

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
    public bool canTakeDamage = true;
    public bool shieldIsUp = false;

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
        dir = "L";

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
        DirectionManaging();
        //SwordAttack();
        Anims();
        currentHealth = HealthSystem.GetHealth();
        healthbar.SetHealth(currentHealth);


        if (canMove == false && !shieldIsUp)
        {
            Invoke("MovementLock", 0.2f);
        }
        if (currentHealth <= 0)
        {
            //TRIGGER DEATH ANIM AND THEN LOAD PERHAPS USE COROUTINE
            if (menuManager != null)
            {
                menuManager.ToAlphaLevel();
            }
        }

    }

    public void TakeDamage(float dmg, string dir)
    {
        //HURT ANIM
        if (canTakeDamage)
        {
            HealthSystem.Damage(dmg);
        }
        else
        {
            //Knockback w/ shield
            if (dir == "U")
            {
                rb.AddForce(new Vector2(0, knockBackDebug), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "D")
            {
                rb.AddForce(new Vector2(0, -knockBackDebug), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "L")
            {
                rb.AddForce(new Vector2(-knockBackDebug, 0), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "R")
            {
                rb.AddForce(new Vector2(knockBackDebug, 0), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
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

        if (currentHealth == maxHealth)
        {
            HealthSystem = new HealthSystem(playerstats.Health.Value);
        }
        else
        {
            HealthSystem.ModMaxHealth(playerstats.Health.Value);
        }

        HealthSystem.ModMaxHealth(playerstats.Health.Value);
        maxHealth = playerstats.Health.Value;
        healthbar.SetMaxHealth(maxHealth);
        potionPotency = playerstats.PotionPotency.Value;
    }

    public void UsePotion()
    {
        if (potion > 0 && canHeal == true)
        {
            //TRIGGER ANIMATION HERE
            anim.SetBool("DoHeal", true);
            canMove = false;
            canHeal = false;
            Invoke("MovementLock", 0.3f);
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

        #region Non-Touch Controls
        //Attack
        if (Input.GetKey(KeyCode.Space) && !useTouch)
        {
            DoAnAttack();
        }

        //Shield up
        if (Input.GetKey(KeyCode.Mouse1) && !useTouch)
        {
            shieldIsUp = true;
            StartCoroutine(ShieldUp());
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && !useTouch)
        {
            shieldIsUp = false;
        }

        //Potion
        if (Input.GetKeyDown(KeyCode.E) && !useTouch)
        {
            UsePotion();
        }

        //Switch weapons
        if (Input.GetKeyDown(KeyCode.Q) && !useTouch)
        {
            SwitchWeapon();
        }
        #endregion
    }

    public void Anims()
    {
        #region Walking directions
        anim.SetInteger("X_Input", Mathf.RoundToInt(xInput));
        anim.SetInteger("Y_Input", Mathf.RoundToInt(yInput));
        if (xInput == 0 && yInput == 0)
        {
            anim.SetBool("IsIdle", true);
        }
        else anim.SetBool("IsIdle", false);
        #endregion

        #region States based on what weapon player is using
        if (weaponInUse == WeaponState.sword)
        {
            weaponState = 1;
        }
        else if (weaponInUse == WeaponState.bow)
        {
            weaponState = 2;
        }

        //Weapon switch button
        Animator switchAnim = GameObject.FindGameObjectWithTag("WeaponSwitchButton").GetComponent<Animator>();
        switchAnim.SetInteger("WeaponState", weaponState);

        //Player animation states
        anim.SetInteger("WeaponState", weaponState);
        #endregion

        //Shield
        anim.SetBool("ShieldUp", shieldIsUp);
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

    public IEnumerator ShieldUp()
    {
        shieldIsUp = true;

        while (shieldIsUp)
        {
            yield return new WaitForSeconds(0.1f);
            canTakeDamage = false;
            canMove = false;
            canAttack = false;
            canHeal = false;
        }

        shieldIsUp = false;
        canMove = true;
        canAttack = true;
        canHeal = true;
        canTakeDamage = true;

        yield return null;
    }

    void DirectionManaging()
    {
        if (xInput == 1 && (canMove == true)) //Right
        {
            dir = "R";
            shootPoint.localPosition = new Vector2(0.25f, 0.9f);
        }
        else if (xInput == -1 && (canMove == true)) //Left
        {
            dir = "L";
            shootPoint.localPosition = new Vector2(-0.5f, 0.9f);
        }
        if (yInput == 1 && (canMove == true)) //Up
        {
            dir = "U";
            shootPoint.localPosition = new Vector2(-0.1f, 1f);
        }
        else if (yInput == -1 && (canMove == true)) //Down
        {
            dir = "D";
            shootPoint.localPosition = new Vector2(-0.1f, 0.5f);
        }
        if ((xInput == 1 && yInput == 1) && (canMove == true)) //Up right
        {
            dir = "UR";
            shootPoint.localPosition = new Vector2(0.1f, 0.9f);
        }
        else if ((xInput == -1 && yInput == -1) && (canMove == true)) //Down left
        {
            dir = "DL";
            shootPoint.localPosition = new Vector2(-0.3f, 0.6f);
        }
        if ((xInput == 1 && yInput == -1) && (canMove == true)) //Down right
        {
            dir = "DR";
            shootPoint.localPosition = new Vector2(0.1f, 0.6f);
        }
        if ((xInput == -1 && yInput == 1) && (canMove == true)) //Up left
        {
            dir = "UL";
            shootPoint.localPosition = new Vector2(-0.5f, 0.9f);
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
        anim.SetTrigger("SwitchWeapons");
        //anim.ResetTrigger("SwitchWeapons");
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
            //SETS ANIMATOR TRIGGER
            anim.SetTrigger("DoAttack");
            anim.SetBool("InAttackAnim", true);
            //---------------------
            GameObject clonedObject = Instantiate(sword, shootPoint.position, Quaternion.identity, transform);
            clonedObject.GetComponent<Swordscript>().RotateMeBaby(dir);
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
            //SETS ANIMATOR TRIGGER
            anim.SetTrigger("DoFire");
            anim.SetBool("InAttackAnim", true);
            //---------------------
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
        anim.ResetTrigger("DoAttack");
        anim.ResetTrigger("DoFire");
        anim.SetBool("InAttackAnim", false);
        anim.SetBool("DoHeal", false);
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
