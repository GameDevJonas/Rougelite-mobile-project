using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class DebuggingInBuild : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI sliderText;

    public CinemachineVirtualCamera vCam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderText.text = "Value: " + slider.value;
        vCam.m_Lens.OrthographicSize = slider.value;
    }
}
