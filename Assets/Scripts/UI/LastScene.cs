using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScene : MonoBehaviour
{
    public static bool returnToMainMenu = false;
    private void Awake()
    {

    }

    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        MenuManager manager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        MenuManager.menuManagersInScene.Remove(manager.gameObject);
        Destroy(manager.gameObject);
        Invoke("RestartGame", 135f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        returnToMainMenu = true;
        SceneManager.LoadScene(0);
    }
}
