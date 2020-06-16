using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Playables;
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
    public GameObject cutscene;
    public GameObject confirm;
    public GameObject pauseEnter;
    public GameObject pauseExit;

    public static List<GameObject> menuManagersInScene = new List<GameObject>();
    public List<GameObject> menuManagersCheck = new List<GameObject>();

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
    bool removeVideo;
    bool startButtonActive;

    void Awake()
    {
        startButtonActive = false;
        cutscene = FindObjectOfType<VideoPlayer>().gameObject;
        removeVideo = false;
        menuManagersInScene.Add(gameObject);
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
    private void Update()
    {
        menuManagersCheck = menuManagersInScene;
        if (menuManagersInScene.Count > 1)
        {
            Destroy(menuManagersInScene[1]);
            startButton.SetActive(true);
            menuLogo.SetActive(true);
            menuManagersInScene.Remove(menuManagersInScene[1]);
        }
        if (LastScene.returnToMainMenu && SceneManager.GetActiveScene().buildIndex == 0)
        {
            cutscene.SetActive(false);
            GetComponent<PlayableDirector>().enabled = false;
            LastScene.returnToMainMenu = false;
            removeVideo = false;
        }
        else if (cutscene == null && SceneManager.GetActiveScene().buildIndex == 0)
        {
            cutscene = GameObject.FindObjectOfType<VideoPlayer>().gameObject;
            if (removeVideo && SceneManager.GetActiveScene().buildIndex == 0)
            {
                startButtonActive = true;
                cutscene.SetActive(false);
                GetComponent<PlayableDirector>().enabled = false;
                removeVideo = false;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetMouseButtonDown(0))
        {
            if (cutscene != null && !startButtonActive)
            {
                startButton.SetActive(false);
                cutscene.SetActive(false);
                Invoke("StartButtonDelay", .1f);
                GetComponent<PlayableDirector>().enabled = false;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            VideoPlayer video = FindObjectOfType<VideoPlayer>();
            if (video.time >= video.clip.length || Input.GetMouseButtonDown(0))
            {
                ToStartScreen();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 0 && cutscene == null)
        {
            cutscene = FindObjectOfType<VideoPlayer>().gameObject;
        }
    }

    void StartButtonDelay()
    {
        if (!startButtonActive)
        {
            cutscene = null;
            startButton.SetActive(true);
            startButtonActive = true;
        }
    }

    public void StartGame()
    {
        removeVideo = true;
        confirm.GetComponent<AudioSource>().Play();
        menuLogo.SetActive(false);
        fromStartMenu = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(1));
        if (FindObjectOfType<VideoPlayer>().gameObject)
        {
            GameObject cutscene = GameObject.FindObjectOfType<VideoPlayer>().gameObject;
            cutscene.SetActive(false);
            GetComponent<PlayableDirector>().enabled = false;
        } 
    }

    public void TestButton()
    {
    }
    public void PauseMenu()
    {
        if (isPaused)
        {
            pauseEnter.GetComponent<AudioSource>().Play();
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
            pauseExit.GetComponent<AudioSource>().Play();
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

    public void ToBossLevel()
    {
        int currenctScene = SceneManager.GetActiveScene().buildIndex;
        //if (currenctScene == 5)
        //{
        //    currenctScene = 0;
        //}
        //toBoss = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(currenctScene + 5));
    }

    public void LoadNextLevel(int scene)
    {
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        StartCoroutine(StartLoad(scene));
    }

    public void ToStartScreen()
    {
        //wait for rue animations
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        //fromStartMenu = true;
        startButton.SetActive(false);
        loadingThing.SetActive(true);
        removeVideo = true;
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
            loadingSlider.value = Mathf.RoundToInt(progress);
            int prevScene = SceneManager.GetActiveScene().buildIndex;
            if (prevScene != 0)
            {
                SceneManager.UnloadSceneAsync(prevScene);
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
