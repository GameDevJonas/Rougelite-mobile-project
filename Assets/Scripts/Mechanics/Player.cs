using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[Serializable]
public class Player : MonoBehaviour
{

    public enum WeaponState { sword, bow };
    public WeaponState weaponInUse = WeaponState.sword;

    private Rigidbody2D rb;

    public Animator anim;

    public FixedJoystick joystick;

    public GameObject sword;

    GameObject playerCanvas;
    public GameObject deathScreen;
    GameObject deathscreenclone;
    public GameObject healthPopup;

    public Button swordAttack;
    public Button switchButton;
    public Button shieldButton;

    public float maxHealth;
    public float currentHealth;
    public float speed;
    public float maxspeed;
    public float attackspeed;
    public int potion = 3;
    public float potionPotency;
    public float rotationSpeed = 5f;
    public float xInput;
    public float yInput;
    public int weaponState;
    public float knockBack;
    public float shieldKnockBack;
    public int extraLives = 5;
    public int strengthPotionCount = 0;

    public HealthSystem HealthSystem = new HealthSystem(50);
    public Healthbar healthbar;
    public PlayerStats playerstats;

    public bool hasSword, hasBow, hasShield = true;

    public bool useTouch;
    public bool attack;
    public bool attacking;
    public bool attackcooldown = false;
    public bool canMove = true;
    public bool movementlock = false;
    public bool canAttack = true;
    public bool canHeal = true;
    public bool canTakeDamage = true;
    public bool shieldIsUp = false;
    public bool inKnockBack = false;
    public bool knockBackCooldown = false;
    public bool ignore = false;
    public bool potionsIncreaseStr = false;
    public bool subtractextralife = true;
    public bool isdead = false;
    public bool slowingdown = false;

    public MenuManager menuManager;

    public string dir;
    public GameObject arrow;
    public GameObject swordprojectile;
    public float shootSpeed;
    public float shootForce;
    public Transform shootPoint;
    public LightConeScript LightCone;

    public GameObject currentRoom;

    public AstarStarter aStarManager;

    public bool isMoving;

    OtherPlayerSounds otherSFX;

    void Awake()
    {
        otherSFX = GetComponentInChildren<OtherPlayerSounds>();
        isMoving = false;

        playerstats = GetComponent<PlayerStats>();

        anim = GetComponentInChildren<Animator>();

        shootPoint = GameObject.FindGameObjectWithTag("ShootPoint").transform;

        swordAttack = GameObject.FindGameObjectWithTag("SwordButton").GetComponent<Button>();

        switchButton = GetComponentInChildren<SwitchButton>().GetComponent<Button>();

        shieldButton = GetComponentInChildren<DefendButton>().GetComponent<Button>();

        joystick = FindObjectOfType<FixedJoystick>();

        playerstats = GetComponent<PlayerStats>();

        LightCone = GetComponentInChildren<LightConeScript>();

        rb = GetComponent<Rigidbody2D>();

        menuManager = FindObjectOfType<MenuManager>();

        playerCanvas = gameObject.GetComponentInChildren<Canvas>().gameObject;

        UpdateStats();


        dir = "D";

#if UNITY_ANDROID
        useTouch = true;
#endif
#if UNITY_EDITOR
        useTouch = false;
#endif

        
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 6)
        {
            GetComponentInChildren<CanvasGroup>().GetComponent<Animator>().enabled = true;
            transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        }
        else
        {
            GetComponentInChildren<CanvasGroup>().GetComponent<Animator>().enabled = false;
            GetComponentInChildren<CanvasGroup>().alpha = 1;
        }
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

        if (currentHealth <= 0 && !isdead)
        {
            //TRIGGER DEATH ANIM AND THEN LOAD PERHAPS USE COROUTINE
            anim.SetTrigger("Death");
            otherSFX.PlayDeathSound(); //DEATH SOUND
            canMove = false;
            canAttack = false;
            canHeal = false;
            menuManager.pauseButton.SetActive(false);
            Time.timeScale = 0.003f;
            otherSFX.PlayGameOverSound();
            DeathScreen();
            isdead = true;
        }
        if (currentHealth > 1 && isdead)
        {
            canMove = true;
            canAttack = true;
            canHeal = true;
            subtractextralife = true;
            Time.timeScale = 1;
            otherSFX.StopGameOverSound();
            isdead = false;
        }
        if (playerCanvas.activeSelf == true && isdead)
        {
            playerCanvas.SetActive(false);
            menuManager.pauseButton.SetActive(false);
        }
        if (playerCanvas.activeSelf == false && !isdead)
        {
            playerCanvas.SetActive(true);
            menuManager.pauseButton.SetActive(true);
        }
        if (dir != LightCone.direction)
        {
            LightCone.RotateMeBaby(dir, playerstats.HasEye.Value);
        }
        float minspeed = maxspeed / 2f;
        if (!slowingdown && playerstats.CanDodge.Value > 0)
        {
            print("slowdown");
            speed -= Time.deltaTime * 80;
            if (speed <= minspeed)
            {
                slowingdown = true;
            }
        }
        if (slowingdown && playerstats.CanDodge.Value > 0)
        {
            print("speedup");
            speed += Time.deltaTime * 80;
            if (speed >= maxspeed)
            {
                slowingdown = false;
            }
        }

        CheckForMovement();
    }


    public void HealthPopup(float health)
    {
        float fadetime = 0.5f;

        double hpprint = System.Math.Round(health, 2);
        GameObject healpopupclone = Instantiate(healthPopup, transform.position + transform.up * 18, Quaternion.identity);
        TextMeshProUGUI hpText = healpopupclone.GetComponentInChildren<TextMeshProUGUI>();
        healpopupclone.AddComponent<Rigidbody2D>();
        healpopupclone.GetComponent<Rigidbody2D>().velocity = RandomVector(-10f, 10f);
        hpText.color = Color.green;
        hpText.fontSize = 42;
        hpText.CrossFadeAlpha(0, fadetime, false);
        hpText.text = hpprint.ToString();
        healpopupclone.SetActive(true);
        Destroy(healpopupclone, fadetime);

    }

    Vector3 RandomVector(float min, float max)
    {
        float x = UnityEngine.Random.Range(min, max);
        float y = 10;
        float z = 0;
        return new Vector3(x, y, z);
    }

    void CheckForMovement()
    {
        if ((xInput != 0 || yInput != 0) && canMove)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    void DeathScreen()
    {
        if (subtractextralife == true)
        {
            deathscreenclone = Instantiate(deathScreen, transform.parent);
            subtractextralife = false;
            extraLives -= 1;
        }
    }

    public void TakeDamageAndKnockBack(float dmg, string dir)
    {
        //HURT ANIM


        if (canTakeDamage && playerstats.IgnoreKnockback.Value <= 0 || canTakeDamage && !knockBackCooldown)
        {
            HealthSystem.Damage(dmg);
            otherSFX.PlayDamageSound(); //DAMAGE SOUND
            if (dir == "U")
            {
                rb.AddForce(new Vector2(0, knockBack), ForceMode2D.Force);

                //ANIMATION
            }
            else if (dir == "D")
            {
                rb.AddForce(new Vector2(0, -knockBack), ForceMode2D.Force);

                //ANIMATION
            }
            else if (dir == "L")
            {
                rb.AddForce(new Vector2(-knockBack, 0), ForceMode2D.Force);

                //ANIMATION
            }
            else if (dir == "R")
            {
                rb.AddForce(new Vector2(knockBack, 0), ForceMode2D.Force);

                //ANIMATION
            }
            inKnockBack = true;
            knockBackCooldown = true;
            canMove = false;
            canAttack = false;
            canHeal = false;
            Invoke("CanHeal", .75f);
            if (!attackcooldown)
            {
                Invoke("AttackLock", .75f);
            }
            Invoke("MovementLock", .75f);
            Invoke("StopKnockBack", .75f);
        }
        else if (canTakeDamage && playerstats.IgnoreKnockback.Value > 0 || canTakeDamage && knockBackCooldown)
        {
            HealthSystem.Damage(dmg);
            otherSFX.PlayDamageSound(); //DAMAGE SOUND
        }
        else if (!canTakeDamage && playerstats.IgnoreKnockback.Value <= 0 || !canTakeDamage && !knockBackCooldown)
        {
            otherSFX.PlayShieldSound(); //SHIELD BLOCK SOUND
            if (dir == "U")
            {
                rb.AddForce(new Vector2(0, shieldKnockBack), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "D")
            {
                rb.AddForce(new Vector2(0, -shieldKnockBack), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "L")
            {
                rb.AddForce(new Vector2(-shieldKnockBack, 0), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "R")
            {
                rb.AddForce(new Vector2(shieldKnockBack, 0), ForceMode2D.Force);

                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }

            inKnockBack = true;
            knockBackCooldown = true;
            anim.SetBool("ShieldUp", true);
            canMove = false;
            canAttack = false;
            canHeal = false;
            Invoke("CanHeal", .5f);
            if (!attackcooldown)
            {
                Invoke("AttackLock", .5f);
            }
            Invoke("MovementLock", .5f);
            Invoke("StopKnockBack", .5f);
        }
        else if (!canTakeDamage && playerstats.IgnoreKnockback.Value > 0 || !canTakeDamage && knockBackCooldown)
        {
            //Knockback w/ shield
            otherSFX.PlayShieldSound(); //SHIELD BLOCK SOUND
            if (dir == "U")
            {
                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "D")
            {
                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "L")
            {
                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            else if (dir == "R")
            {
                //ANIMATION
                anim.SetTrigger("ShieldHit");
            }
            anim.SetBool("ShieldUp", true);
        }
    }

    void StopKnockBack()
    {
        rb.velocity = Vector2.zero;
        inKnockBack = false;
        Invoke("KnockbackCooldown", 1.5f);
    }

    void KnockbackCooldown()
    {
        knockBackCooldown = false;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        //Resources.UnloadUnusedAssets();
    }

    public void UpdateStats()
    {
        maxspeed = playerstats.MovementSpeed.Value * 5;
        speed = maxspeed;
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
        if (playerstats.PotsIncreaseStr.Value <= 0 && potionsIncreaseStr == true)
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
            Physics2D.IgnoreLayerCollision(9, 11, ignore: true);
        }
        if (playerstats.IgnoreUnitCollision.Value == 0 && ignore == true)
        {
            ignore = false;
            Physics2D.IgnoreLayerCollision(9, 11, ignore: false);
        }
    }

    public void UsePotion()
    {
        if (potion > 0 && canHeal == true)
        {
            otherSFX.PlayPotionSound(); //POTION SOUND
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
                strengthPotionCount += 1;
                playerstats.RemoveFlatModifier(playerstats.Strength);
                if (playerstats.Loot.Any((des => des.id.Equals(1))))
                {
                    playerstats.AddFlatModifier(playerstats.Strength,
                    (playerstats.Loot.Find(x => x.id == 1).statValue) * (playerstats.Loot.Find(x => x.id == 1).collection) + strengthPotionCount);
                }
                else
                {
                    playerstats.AddFlatModifier(playerstats.Strength, strengthPotionCount);
                }
                UpdateStats();
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
        if (GameObject.FindGameObjectWithTag("WeaponSwitchButton").GetComponent<Animator>())
        {
            Animator switchAnim = GameObject.FindGameObjectWithTag("WeaponSwitchButton").GetComponent<Animator>();
            switchAnim.SetInteger("WeaponState", weaponState);
        }

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
        if (xInput == 1) //Right
        {
            dir = "R";
            shootPoint.localPosition = new Vector2(0.25f, 0.9f);
        }
        else if (xInput == -1) //Left
        {
            dir = "L";
            shootPoint.localPosition = new Vector2(-0.5f, 0.9f);
        }
        if (yInput == 1) //Up
        {
            dir = "U";
            shootPoint.localPosition = new Vector2(-0.1f, 1f);
        }
        else if (yInput == -1) //Down
        {
            dir = "D";
            shootPoint.localPosition = new Vector2(-0.1f, 0.5f);
        }
        if (xInput == 1 && yInput == 1) //Up right
        {
            dir = "UR";
            //shootPoint.localPosition = new Vector2(0.1f, 0.9f);
            shootPoint.localPosition = new Vector2(-0.1f, 1f);
        }
        else if (xInput == -1 && yInput == -1) //Down left
        {
            dir = "DL";
            //shootPoint.localPosition = new Vector2(-0.3f, 0.6f);
            shootPoint.localPosition = new Vector2(-0.1f, 0.5f);
        }
        if (xInput == 1 && yInput == -1) //Down right
        {
            dir = "DR";
            //shootPoint.localPosition = new Vector2(0.1f, 0.6f);
            shootPoint.localPosition = new Vector2(-0.1f, 0.5f);
        }
        if (xInput == -1 && yInput == 1) //Up left
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
            otherSFX.PlaySwitchSound(weaponState); //SWITCH WEAPON SOUND
            if (weaponInUse == WeaponState.sword)
            {
                weaponInUse = WeaponState.bow;
            }
            else if (weaponInUse == WeaponState.bow)
            {
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

    public IEnumerator DoAnAttack()
    {
        while (attacking)
        {
            canMove = false;
            switch (weaponInUse)
            {
                case WeaponState.sword:
                    SwordAttack();
                    break;
                case WeaponState.bow:
                    BowAttack();
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (!movementlock)
        {
            canMove = true;
        }
        attacking = false;
        yield return null;
    }

    public void SwordAttack()
    {
        if (canAttack)
        {
            otherSFX.PlayAttackSound(weaponState); //ATTACK SOUND
            //SETS ANIMATOR TRIGGER
            anim.SetTrigger("DoAttack");
            anim.SetBool("InAttackAnim", true);
            //---------------------
            canHeal = false;
            if (playerstats.SwordRangeIncreased.Value <= 0 && playerstats.SwordArcIncreased.Value <= 0)
            {
                GameObject clonedObject = Instantiate(sword, shootPoint.position, Quaternion.identity, transform);
                clonedObject.GetComponent<Swordscript>().RotateMeBaby(dir, playerstats.HasEye.Value);
                Destroy(clonedObject, 0.2f);
            }
            if (playerstats.SwordRangeIncreased.Value > 0 && playerstats.SwordArcIncreased.Value <= 0)
            {
                GameObject clonedObject = Instantiate(sword, shootPoint.position, Quaternion.identity, transform);
                clonedObject.transform.localScale = new Vector3(15, 24, 15);
                clonedObject.GetComponent<Swordscript>().RotateMeBaby(dir, playerstats.HasEye.Value);
                Destroy(clonedObject, 0.2f);
            }
            if (playerstats.SwordRangeIncreased.Value <= 0 && playerstats.SwordArcIncreased.Value > 0)
            {
                GameObject clonedObject = Instantiate(sword, shootPoint.position, Quaternion.identity, transform);
                clonedObject.transform.localScale = new Vector3(24, 15, 24);
                clonedObject.GetComponent<Swordscript>().RotateMeBaby(dir, playerstats.HasEye.Value);
                Destroy(clonedObject, 0.2f);
            }
            if (playerstats.SwordRangeIncreased.Value > 0 && playerstats.SwordArcIncreased.Value > 0)
            {
                GameObject clonedObject = Instantiate(sword, shootPoint.position, Quaternion.identity, transform);
                clonedObject.transform.localScale = new Vector3(24, 24, 24);
                clonedObject.GetComponent<Swordscript>().RotateMeBaby(dir, playerstats.HasEye.Value);
                Destroy(clonedObject, 0.2f);
            }
            if (playerstats.SwordProjectile.Value > 0)
            {
                GameObject swordprojectileClone = Instantiate(swordprojectile, shootPoint.position, Quaternion.identity, transform);
                swordprojectileClone.GetComponent<SwordProjectile>().ShootyShoot(dir, playerstats.HasEye.Value);
            }
            canMove = false;
            canHeal = false;
            attackcooldown = true;

            //swordAttack.interactable = false;
            if (attackcooldown)
            {
                canAttack = false;
                Invoke("AttackLock", attackspeed);
                Invoke("MovementLock", attackspeed / 2);
                Invoke("CanHeal", attackspeed);
                movementlock = true;
            }
        }
    }

    public void BowAttack()
    {
        if (canAttack)
        {
            otherSFX.PlayAttackSound(weaponState); //ATTACK SOUND
            //SETS ANIMATOR TRIGGER
            anim.SetBool("InAttackAnim", true);
            anim.SetTrigger("DoFire");
            //---------------------
            GameObject arrowClone = Instantiate(arrow, shootPoint.position, Quaternion.identity);
            arrowClone.GetComponent<Arrow>().ShootyShoot(dir, playerstats.HasEye.Value);
            if (playerstats.TripleArrow.Value > 0)
            {
                GameObject arrowClone2 = Instantiate(arrow, shootPoint.position, Quaternion.identity);
                arrowClone2.GetComponent<Arrow>().ShootyShoot1(dir);
                GameObject arrowClone3 = Instantiate(arrow, shootPoint.position, Quaternion.identity);
                arrowClone3.GetComponent<Arrow>().ShootyShoot2(dir);
            }
            canMove = false;
            attackcooldown = true;
            canHeal = false;
            //Destroy(arrowClone, 5f);
            if (attackcooldown)
            {
                canAttack = false;
                attackcooldown = true;
                movementlock = true;
                Invoke("AttackLock", shootSpeed);
                Invoke("MovementLock", shootSpeed / 2);
                Invoke("CanHeal", shootSpeed);
            }
        }
    }

    void MovementLock()
    {
        movementlock = false;
        anim.ResetTrigger("DoAttack");
        anim.ResetTrigger("DoFire");
        anim.SetBool("InAttackAnim", false);
        anim.SetBool("DoHeal", false);
        if (!attacking)
        {
            canMove = true;
        }
    }
    void AttackLock()
    {
        canAttack = true;
        attackcooldown = false;
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
