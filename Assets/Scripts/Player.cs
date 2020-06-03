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
    //public TextMeshProUGUI debugWeaponState;

    private Rigidbody2D rb;

    public Animator anim;

    public FixedJoystick joystick;

    public GameObject sword;

    public Button swordAttack;
    public Button switchButton;
    public Button shieldButton;

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
    public PlayerStats playerstats;

    public bool hasSword, hasBow, hasShield = true;

    public bool useTouch;
    public bool attack;
    public bool canMove = true;
    public bool canAttack = true;
    public bool canHeal = true;
    public bool canTakeDamage = true;
    public bool shieldIsUp = false;
    public bool inKnockBack = false;
    public bool ignore = false;
    public bool potionsIncreaseStr = false;

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
        playerstats = GetComponent<PlayerStats>();
        //debugWeaponState.text = "sword";

        anim = GetComponentInChildren<Animator>();

        shootPoint = GameObject.FindGameObjectWithTag("ShootPoint").transform;

        swordAttack = GameObject.FindGameObjectWithTag("SwordButton").GetComponent<Button>();

        switchButton = GetComponentInChildren<SwitchButton>().GetComponent<Button>();

        shieldButton = GetComponentInChildren<DefendButton>().GetComponent<Button>();

        joystick = FindObjectOfType<FixedJoystick>();

        playerstats = GetComponent<PlayerStats>();

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
        if (currentHealth == maxHealth && playerstats.PotsIncreaseStr.Value == 0)
        {
            canHeal = false;
        }
        else
        {
            canHeal = true;
        }

        /*if (canMove == false && !shieldIsUp)
        {
            Invoke("MovementLock", 0.2f);
        }*/

        #region Button Managing with interactables based on what the player has
        if (hasSword && !hasBow) // Maybe call a function when sacrificed one of these
        {
            switchButton.interactable = false;
            weaponInUse = WeaponState.sword;
        }
        else if (!hasSword && hasBow)
        {
            switchButton.interactable = false;
            weaponInUse = WeaponState.bow;
        }
        else
        {
            switchButton.interactable = true;
        }

        shieldButton.interactable = hasShield;

        #endregion

        if (currentHealth <= 0)
        {
            //TRIGGER DEATH ANIM AND THEN LOAD PERHAPS USE COROUTINE
            anim.SetTrigger("Death");
            canMove = false;
            canAttack = false;
            canHeal = false;
            if (menuManager != null)
            {
                menuManager.ToAlphaLevel();
            }
        }

    }

    public void TakeDamageAndKnockBack(float dmg, string dir)
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
            inKnockBack = true;
            anim.SetBool("ShieldUp", true);
            canMove = false;
            canAttack = false;
            canHeal = false;
            Invoke("CanHeal", .5f);
            Invoke("AttackLock", .5f);
            Invoke("MovementLock", .5f);
            Invoke("StopKnockBack", 1f);
        }
    }

    void StopKnockBack()
    {
        anim.SetBool("ShieldUp", false);
        rb.velocity = Vector2.zero;
        inKnockBack = false;
        shieldIsUp = false;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        //Resources.UnloadUnusedAssets();
    }
    public void UpdateStats()
    {
        speed = playerstats.MovementSpeed.Value * 5;
        attackspeed = (100f - playerstats.Dexterity.Value) / 100f;
        if (playerstats.RapidFire.Value > 0)
        {
            shootSpeed = 0.25f;
        }
        else
        {
            shootSpeed = (100f - playerstats.Dexterity.Value) / 100f;
        }

        if (HealthSystem.GetHealth() >= maxHealth)
        {
            HealthSystem = new HealthSystem(playerstats.Health.Value);
            currentHealth = HealthSystem.GetHealth();
        }
        else
        {
            HealthSystem.ModMaxHealth(playerstats.Health.Value);
            currentHealth = HealthSystem.GetHealth();
        }
        maxHealth = playerstats.Health.Value;
        healthbar.SetMaxHealth(maxHealth);
        potionPotency = playerstats.PotionPotency.Value;

        if (playerstats.PotsIncreaseStr.Value > 0 && potionsIncreaseStr == false)
        {
            potionsIncreaseStr = true;
        }
        else
        {
            potionsIncreaseStr = false;
        }
        if (playerstats.HasSword.Value > 0 && hasSword == true)
        {
            hasSword = false;
        }
        if (playerstats.HasCrossbow.Value > 0 && hasBow == true)
        {
            hasBow = false;
        }
        if (playerstats.IgnoreUnitCollision.Value > 0 && ignore == false)
        {
            ignore = true;
            Physics2D.IgnoreLayerCollision(9, 12, ignore: true);
        }
        if (playerstats.IgnoreUnitCollision.Value == 0 && ignore == true)
        {
            ignore = false;
            Physics2D.IgnoreLayerCollision(9, 12, ignore: false);
        }
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
            if (potionsIncreaseStr == true)
            {
                playerstats.AddFlatModifier(playerstats.Strength, 5);
            }
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
        if (Input.GetKey(KeyCode.Mouse1) && !useTouch && hasShield)
        {
            shieldIsUp = true;
            StartCoroutine(ShieldUp());
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && !useTouch && hasShield && !inKnockBack)
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
        anim.SetBool("HasSword", hasSword);
        anim.SetBool("HasBow", hasBow);
        if (playerstats.ShieldArm.Value > 0)
        {
            anim.SetBool("HasShield", !hasShield);
        }
        else
        {
            anim.SetBool("HasShield", hasShield);
        }

        //Shield
        anim.SetBool("ShieldUp", shieldIsUp);
        #endregion

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

        while (shieldIsUp || (shieldIsUp && inKnockBack) || inKnockBack)
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
            //shootPoint.localPosition = new Vector2(0.1f, 0.9f);
            shootPoint.localPosition = new Vector2(-0.1f, 1f);
        }
        else if ((xInput == -1 && yInput == -1) && (canMove == true)) //Down left
        {
            dir = "DL";
            //shootPoint.localPosition = new Vector2(-0.3f, 0.6f);
            shootPoint.localPosition = new Vector2(-0.1f, 0.5f);
        }
        if ((xInput == 1 && yInput == -1) && (canMove == true)) //Down right
        {
            dir = "DR";
            //shootPoint.localPosition = new Vector2(0.1f, 0.6f);
            shootPoint.localPosition = new Vector2(-0.1f, 0.5f);
        }
        if ((xInput == -1 && yInput == 1) && (canMove == true)) //Up left
        {
            dir = "UL";
            //shootPoint.localPosition = new Vector2(-0.5f, 0.9f);
            shootPoint.localPosition = new Vector2(-0.1f, 1f);
        }
    }

    public void SwitchWeapon()
    {
        if (hasSword && hasBow)
        {
            if (weaponInUse == WeaponState.sword)
            {
                //debugWeaponState.text = "bow";
                weaponInUse = WeaponState.bow;
            }
            else if (weaponInUse == WeaponState.bow)
            {
                //debugWeaponState.text = "sword";
                weaponInUse = WeaponState.sword;
            }
            anim.SetTrigger("SwitchWeapons");
            Invoke("ResetWeaponSwitchTrigger", .1f);
        }
    }

    void ResetWeaponSwitchTrigger()
    {
        anim.ResetTrigger("SwitchWeapons");
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
            canHeal = false;
            GameObject clonedObject = Instantiate(sword, shootPoint.position, Quaternion.identity, transform);
            clonedObject.GetComponent<Swordscript>().RotateMeBaby(dir);
            canMove = false;
            canAttack = false;
            Destroy(clonedObject, 0.2f);
            swordAttack.interactable = false;
            if (canAttack == false)
            {
                Invoke("AttackLock", attackspeed);
                Invoke("MovementLock", attackspeed / 2);
                Invoke("CanHeal", attackspeed / 2);
            }
        }
    }

    public void BowAttack()
    {
        if (canAttack)
        {
            //SETS ANIMATOR TRIGGER
            anim.SetBool("InAttackAnim", true);
            anim.SetTrigger("DoFire");
            //---------------------
            GameObject arrowClone = Instantiate(arrow, shootPoint.position, Quaternion.identity);
            arrowClone.GetComponent<Arrow>().ShootyShoot(dir);
            if (playerstats.TripleArrow.Value > 0)
            {
                GameObject arrowClone2 = Instantiate(arrow, shootPoint.position, Quaternion.identity);
                arrowClone2.GetComponent<Arrow>().ShootyShoot1(dir);
                GameObject arrowClone3 = Instantiate(arrow, shootPoint.position, Quaternion.identity);
                arrowClone3.GetComponent<Arrow>().ShootyShoot2(dir);
            }
            canMove = false;
            canAttack = false;
            canHeal = false;
            //Destroy(arrowClone, 5f);
            if (canAttack == false)
            {
                Invoke("AttackLock", shootSpeed);
                Invoke("MovementLock", shootSpeed / 2);
                Invoke("CanHeal", shootSpeed / 2);
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
