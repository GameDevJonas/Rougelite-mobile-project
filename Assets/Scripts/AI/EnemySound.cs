using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    JEnemy myAi;

    public FootstepSounds footsteps;
    float stepTimer;
    public VoiceSounds voice;
    public DamageSounds damage;
    public EnemyAttackSounds attack;

    float voiceSoundTimerSet = 3f;
    float voiceSoundTimer;

    private void Awake()
    {
        myAi = GetComponent<JEnemy>();
    }

    private void Update()
    {
        StepSoundTimer();
        VoiceSoundsRandom();
    }
    void StepSoundTimer()
    {
        if (stepTimer <= 0 && (myAi.myState == JEnemy.EnemyState.walking))
        {
            AudioClip clip = footsteps.footstepSounds[Random.Range(0, footsteps.footstepSounds.Length)];
            footsteps.mySource.clip = clip;
            footsteps.mySource.pitch = Random.Range(.8f, 1.1f);
            footsteps.mySource.Play();
            stepTimer = footsteps.myStepRatio;
        }
        else
        {
            stepTimer -= Time.deltaTime;
        }
    }

    void VoiceSoundsRandom()
    {
        if (voiceSoundTimer <= 0 && (myAi.myState != JEnemy.EnemyState.non && myAi.myState != JEnemy.EnemyState.dead && myAi.myState != JEnemy.EnemyState.damage))
        {
            int randVoice = Random.Range(0, 101);
            if (randVoice >= 85)
            {
                PlayVoiceSound();
            }
            voiceSoundTimer = voiceSoundTimerSet;
        }
        else
        {
            voiceSoundTimer -= Time.deltaTime;
        }
    }

    public void PlayVoiceSound()
    {
        int rand = Random.Range(0, voice.voiceSounds.Length);
        voice.mySource.clip = voice.voiceSounds[rand];
        voice.mySource.pitch = Random.Range(.8f, 1.1f);
        voice.mySource.Play();
    }

    public void PlayAttackSound()
    {
        int rand = Random.Range(0, attack.attackSounds.Length);
        attack.mySource.clip = attack.attackSounds[rand];
        attack.mySource.pitch = Random.Range(.8f, 1.1f);
        attack.mySource.Play();
    }

    public void PlayDamageSound()
    {
        int rand = Random.Range(0, damage.damageSounds.Length);
        damage.mySource.clip = damage.damageSounds[rand];
        damage.mySource.pitch = Random.Range(.8f, 1.1f);
        damage.mySource.Play();
    }

    public void PlayDeathSound()
    {
        damage.mySource.clip = damage.deathSound;
        damage.mySource.pitch = 1;
        damage.mySource.Play();
    }
}

#region Classes for sorting sounds
[System.Serializable]
public class FootstepSounds
{
    public AudioClip[] footstepSounds;
    public float myStepRatio;
    public AudioSource mySource;
}

[System.Serializable]
public class VoiceSounds
{
    public AudioClip[] voiceSounds;
    public AudioSource mySource;
}

[System.Serializable]
public class EnemyAttackSounds
{
    public AudioClip[] attackSounds;
    public AudioSource mySource;
}
#endregion
