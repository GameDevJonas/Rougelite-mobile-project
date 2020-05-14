using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationScript : MonoBehaviour
{
    public GameObject textbox;
    public GameObject choice01;
    public GameObject choice02;
    public int choiceMade;

    public void Option01()
    {
        choiceMade = 1;
    }
    public void Option02()
    {
        choiceMade = 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (choiceMade >= 1)
        {
            choice01.SetActive (false);
            choice02.SetActive(false);
        }
    }
}
