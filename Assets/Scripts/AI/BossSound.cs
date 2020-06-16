using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    JBoss myAi;

    public AudioSource introLoop, mainLoop;

    public FootstepSounds footsteps;
    public VoiceSounds voice;
    public DamageSounds damage;
    public EnemyAttackSounds attack;
    public OtherSFX otherSounds;

    float stepTimer;
    float voiceSoundTimerSet = 3f;
    float voiceSoundTimer;

    private void Awake()
    {
        myAi = GetComponent<JBoss>();
    }

    private void Update()
    {
        StepSoundTimer();
        //VoiceSoundsRandom();
    }
    void StepSoundTimer()
    {
        if (stepTimer <= 0 && (myAi.myState == JBoss.BossState.walk))
        {
            AudioClip clip = footsteps.footstepSounds[Random.Range(0, footsteps.footstepSounds.Length)];
            footsteps.mySource.clip = clip;
            footsteps.mySource.pitch = Random.Range(.6f, .9f);
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
        if (voiceSoundTimer <= 0 && (myAi.myState != JBoss.BossState.dead && myAi.myState != JBoss.BossState.melee))
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
        //voice.mySource.pitch = Random.Range(-1.1f, -.8f);
        voice.mySource.Play();
    }

    public void CrumbleSound()
    {
        //otherSounds.mySource.clip = otherSounds.crumble;
        otherSounds.mySource.Play();
    }


    public void PlayMeleeScream()
    {
        voice.mySource.clip = voice.voiceSounds[0];
        voice.mySource.pitch = .6f;
        voice.mySource.Play();
    }

    public void PlayRangedScream()
    {
        voice.mySource.clip = voice.voiceSounds[2];
        voice.mySource.pitch = .4f;
        voice.mySource.Play();
    }

    public void PlayAttackSound()
    {
        int rand = Random.Range(0, attack.attackSounds.Length);
        attack.mySource.clip = attack.attackSounds[rand];
        //attack.mySource.pitch = Random.Range(-1.1f, -.8f);
        attack.mySource.Play();
    }

    public void PlayDamageSound()
    {
        int rand = Random.Range(0, damage.damageSounds.Length);
        damage.mySource.clip = damage.damageSounds[rand];
        //damage.mySource.pitch = Random.Range(-1.1f, -.8f);
        damage.mySource.Play();
    }

    public void PlayDeathSound()
    {
        damage.mySource.clip = damage.deathSound;
        damage.mySource.pitch = .6f;
        damage.mySource.Play();
    }
}

[System.Serializable]
public class OtherSFX
{
    public AudioClip crumble;
    public AudioClip deathSound;
    public AudioSource mySource;
}
