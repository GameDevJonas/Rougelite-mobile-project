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
        if(weaponState == 1) //Sword
        {
            Debug.Log("Played sword attack sound");
        }
        else if(weaponState == 2) //Bow
        {
            Debug.Log("Played bow attack sound");
        }
    }
    
    public void PlaySwitchSound(int weaponState)
    {
        if (weaponState == 1) //Switch to bow
        {
            Debug.Log("Played switch to bow sound");
        }
        else if (weaponState == 2) //Switch to sword
        {
            Debug.Log("Played switch to sword sound");
        }
    }
    #endregion

    #region Shield sounds
    public void PlayShieldSound()
    {
        Debug.Log("Played shield block sound");
    }
    #endregion

    #region Damage sounds
    public void PlayDamageSound()
    {
        Debug.Log("Played damage sound");
    }

    public void PlayDeathSound()
    {
        Debug.Log("Played death sound");
    }
    #endregion

    #region Potion sound
    public void PlayPotionSound()
    {
        Debug.Log("Played potion sound");
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
