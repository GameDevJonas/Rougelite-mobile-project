using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPicker : MonoBehaviour
{
    public CinemachineVirtualCamera myVCam;

    public GameObject player;

    void Start()
    {
        myVCam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");

        myVCam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
