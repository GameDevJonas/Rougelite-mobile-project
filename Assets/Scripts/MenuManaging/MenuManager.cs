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
    //public GameObject mainMenuPause;
    public GameObject pauseButton;
    public GameObject otherCanvas;
    public GameObject menuLogo;
    public GameObject deathscreen;

    GameObject playerCanvas;

    #region Main Menu loadingBar
    public GameObject loadingThing;
    public Slider loadingSlider;
    public TextMeshProUGUI progressText;
    #endregion

    public bool isPaused;
    public bool fromStartMenu;
    public bool toBoss;
    public bool fromBoss;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        isPaused = false;
        toBoss = false;
        //mainMenuPause.SetActive(false);
        menuLogo.SetActive(true);
        otherCanvas.SetActive(true);
        pauseButton.SetActive(true);
        DontDestroyOnLoad(this);
        loadingThing.SetActive(false);
        //DontDestroyOnLoad(alphaAudio);
        startButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void StartGame()
    {
        menuLogo.SetActive(false);
        fromStartMenu = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(1));
    }

    public void TestButton()
    {
        Debug.Log("Hei");
    }
    public void PauseMenu()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            if (playerCanvas != null)
            {
                playerCanvas.SetActive(true);
            }
            otherCanvas.SetActive(true);
            pauseMenu.SetActive(false);
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
            }
            else
            {
                //mainMenuPause.SetActive(false);
            }
        }
        else if (!isPaused)
        {
            if (playerCanvas != null)
            {
                playerCanvas.SetActive(false);
            }
            pauseMenu.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
            }
            else
            {
                //mainMenuPause.SetActive(true);
            }
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
        int currenctScene = SceneManager.GetActiveScene().buildIndex;
        if (currenctScene == 5)
        {
            currenctScene = 0;
        }
        toBoss = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(currenctScene + 1));
        Debug.Log(SceneManager.sceneCount);
    }

    public void ToAlphaLevel()
    {
        //wait for rue animations
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        //fromStartMenu = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(0));
    }

    public void ReloadLevel(int buildIndex)
    {
        playerCanvas.SetActive(true);
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(buildIndex));
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
            loadingSlider.value = Mathf.RoundToInt(progress);
            progressText.text = "Progress: " + progress * 100 + "%";

            if ((buildIndex - 1) != 0 && buildIndex != 0)
            {
                //Debug.Log(buildIndex - 1);
                //Debug.Log("Startet unload");
                SceneManager.UnloadSceneAsync(buildIndex - 1);
                //destroyPrev.priority = 10;
            }

            // Loading completed
            if (loadStart.progress == 0.9f)
            {
                Resources.UnloadUnusedAssets();
                loadStart.allowSceneActivation = true;
                if (fromStartMenu)
                {
                    GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                    playerCanvas = player.GetComponentInChildren<Canvas>().gameObject;
                    DontDestroyOnLoad(player);
                    fromStartMenu = false;
                }
                else if (toBoss)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = new Vector3(0, 0, 0);
                    toBoss = false;
                }
                else if (fromBoss)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = new Vector3(0, 0, 0);
                    fromBoss = false;
                }
                loadingThing.SetActive(false);
                pauseButton.SetActive(true);
            }
            yield return null;
        }
    }
}
