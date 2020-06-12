using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerSounds : MonoBehaviour
{
    public BowSounds bow;
    public SwordSounds sword;
    public ShieldSounds shield;
    public DamageSounds damage;
    public PotionSounds potion;

    #region Weapon Sounds
    public void PlayAttackSound(int weaponState)
    {
        if (weaponState == 1) //Sword
        {
            int rand = Random.Range(0, sword.attackSounds.Length);
            sword.mySource.clip = sword.attackSounds[rand];
            sword.mySource.pitch = Random.Range(.8f, 1.1f);
            sword.mySource.Play();
        }
        else if(weaponState == 2) //Bow
        {
            int rand = Random.Range(0, bow.attackSounds.Length);
            bow.mySource.clip = bow.attackSounds[rand];
            bow.mySource.pitch = Random.Range(.8f, 1.1f);
            bow.mySource.Play();
        }
    }
    
    public void PlaySwitchSound(int weaponState)
    {
        if (weaponState == 1) //Switch to bow
        {
            bow.mySource.clip = bow.swapSound;
            bow.mySource.pitch = Random.Range(.8f, 1.1f);
            bow.mySource.Play();
        }
        else if (weaponState == 2) //Switch to sword
        {
            sword.mySource.clip = sword.swapSound;
            sword.mySource.pitch = Random.Range(.8f, 1.1f);
            sword.mySource.Play();
        }
    }
    #endregion

    #region Shield sounds
    public void PlayShieldSound()
    {
        int rand = Random.Range(0, shield.blockSounds.Length);
        shield.mySource.clip = shield.blockSounds[rand];
        shield.mySource.pitch = Random.Range(.8f, 1.1f);
        shield.mySource.Play();
    }
    #endregion

    #region Damage sounds
    public void PlayDamageSound()
    {
        int rand = Random.Range(0, damage.damageSounds.Length);
        damage.mySource.clip = damage.damageSounds[rand];
        damage.mySource.pitch = 1f;
        damage.mySource.Play();
    }

    public void PlayDeathSound()
    {
        damage.mySource.clip = damage.deathSound;
        damage.mySource.pitch = 1f;
        damage.mySource.Play();
    }
    #endregion

    #region Potion sound
    public void PlayPotionSound()
    {
        potion.mySource.clip = potion.potionSound;
        potion.mySource.pitch = Random.Range(.8f, 1.1f);
        potion.mySource.Play();
    }
    #endregion
}

#region Classes for sorting sounds
[System.Serializable]
public class BowSounds
{
    public AudioClip[] attackSounds;
    public AudioClip swapSound;
    public AudioSource mySource;
}

[System.Serializable]
public class SwordSounds
{
    public AudioClip[] attackSounds;
    public AudioClip swapSound;
    public AudioSource mySource;
}

[System.Serializable]
public class ShieldSounds
{
    public AudioClip[] blockSounds;
    public AudioSource mySource;
}

[System.Serializable]
public class DamageSounds
{
    public AudioClip[] damageSounds;
    public AudioClip deathSound;
    public AudioSource mySource;
}

[System.Serializable]
public class PotionSounds
{
    public AudioClip potionSound;
    public AudioSource mySource;
}
#endregion
