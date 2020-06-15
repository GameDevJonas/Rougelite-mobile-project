using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromBossToDungeon : MonoBehaviour
{
    public int currentScene;
    public int nextScene;

    public MenuManager manager;

    void Start()
    {
    }

    void Update()
    {
        manager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 10)
        {
            nextScene = currentScene - 4;
        }
        else
        {
            manager.ToStartScreen();
            //Load cutscene scene
        }

    }

    void LoadNextScene()
    {
        manager.LoadNextLevel(nextScene);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Debug.Log("collision");
            LoadNextScene();
        }
    }
}
