using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject alphaAudio;
    public GameObject startButton;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject otherCanvas;

    #region Main Menu loadingBar
    public GameObject loadingThing;
    public Slider loadingSlider;
    public TextMeshProUGUI progressText;
    #endregion

    public bool isPaused;
    public bool fromStartMenu;
    public bool toBoss;

    void Awake()
    {
        isPaused = false;
        toBoss = false;
        otherCanvas.SetActive(true);
        pauseButton.SetActive(true);
        DontDestroyOnLoad(this);
        loadingThing.SetActive(false);
        DontDestroyOnLoad(alphaAudio);
        startButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void StartGame()
    {
        fromStartMenu = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(1));
    }

    public void PauseMenu()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            otherCanvas.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else if (!isPaused)
        {
            pauseMenu.SetActive(true);
            otherCanvas.SetActive(false);
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToAlphaBoss()
    {
        toBoss = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(2));
    }

    public IEnumerator StartLoad(int buildIndex)
    {
        pauseButton.SetActive(false);
        AsyncOperation loadStart = SceneManager.LoadSceneAsync(buildIndex);
        loadStart.allowSceneActivation = false;
        while (!loadStart.isDone)
        {
            Time.timeScale = 1;
            float progress = Mathf.Clamp01(loadStart.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            loadingSlider.value = progress;
            progressText.text = "Progress: " + progress * 100 + "%";

            // Loading completed
            if (loadStart.progress == 0.9f)
            {
                progressText.text = "Done! Touch anywhere to begin!";
                Debug.Log("Press a key to start");
                if (Input.anyKeyDown)
                {
                    loadStart.allowSceneActivation = true;
                    if (fromStartMenu)
                    {
                        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                        DontDestroyOnLoad(player);
                        fromStartMenu = false;
                    }
                    else if(toBoss)
                    {
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        player.transform.position = new Vector3(0, 0, 0);
                        toBoss = false;
                    }
                    loadingThing.SetActive(false);
                    pauseButton.SetActive(true);
                }
            }
            yield return null;
        }
    }
}
