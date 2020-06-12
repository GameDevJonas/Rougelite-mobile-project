using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFootsteps : MonoBehaviour
{
    AudioSource audio;

    public FootStepsInDungeon[] footStepSounds;

    int currentScene;

    float stepSoundTimerSet;
    float stepSoundTimer;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        audio = GetComponentInChildren<AudioSource>();
        //PlayStep();
    }

    // Update is called once per frame
    void Update()
    {
        //stepSoundTimerSet = 1 / (player.speed / 25);
        stepSoundTimerSet = .6f;
        CheckForScenes();
        StepSoundTimer();
    }

    void CheckForScenes()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            currentScene = 0;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            currentScene = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            currentScene = 2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 9)
        {
            currentScene = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 10)
        {
            currentScene = 4;
        }
    }

    void StepSoundTimer()
    {
        if (stepSoundTimer <= 0 && player.isMoving)
        {
            AudioClip clip = footStepSounds[currentScene].stepSounds[Random.Range(0, footStepSounds[currentScene].stepSounds.Length)];
            audio.clip = clip;
            audio.pitch = Random.Range(.8f, 1.1f);
            audio.Play();
            stepSoundTimer = stepSoundTimerSet;
        }
        else
        {
            stepSoundTimer -= Time.deltaTime;
        }
    }

    void PlayStep()
    {
        AudioClip clip = footStepSounds[currentScene].stepSounds[Random.Range(0, footStepSounds[currentScene].stepSounds.Length)];
        audio.clip = clip;
        audio.pitch = Random.Range(.8f, 1.1f);
        audio.Play();
    }
}

[System.Serializable]
public class FootStepsInDungeon
{
    public AudioClip[] stepSounds;
}
