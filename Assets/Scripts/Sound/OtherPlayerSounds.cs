using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerSounds : MonoBehaviour
{
    public BowSounds bow;
    public SwordSounds sword;
    public ShieldSounds shield;
    public DamageSounds damage;

    #region Weapon Sounds
    public void PlayAttackSound(int weaponState)
    {
        if(weaponState == 1) //Sword
        {

        }
        else if(weaponState == 2) //Bow
        {

        }
    }
    
    public void PlaySwitchSound(int weaponState)
    {
        if (weaponState == 1) //Sword
        {

        }
        else if (weaponState == 2) //Bow
        {

        }
    }
    #endregion

    #region Shield sounds
    public void PlayShieldSound()
    {

    }
    #endregion

    #region Damage sounds
    public void PlayDamageSound()
    {

    }

    public void PlayDeathSound()
    {

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
#endregion
