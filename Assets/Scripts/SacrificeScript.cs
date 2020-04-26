using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeScript : MonoBehaviour
{
    public GameObject textbox;
    public GameObject choice01;
    public GameObject choice02;
    public int choiceMade;

    public void Option01()
    {
        textbox.GetComponent<Text>().text = "Your health is halved, \n we accept your sacrifice and you may enter.";
        choiceMade = 1;
    }
    public void Option02()
    {
        textbox.GetComponent<Text>().text = "Your strength is halved, \n we accept your sacrifice and you may enter.";
        choiceMade = 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (choiceMade >= 1)
        {
            choice01.SetActive(false);
            choice02.SetActive(false);
        }
    }
}
