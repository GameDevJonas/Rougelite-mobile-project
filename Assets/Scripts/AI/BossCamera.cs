using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossCamera : MonoBehaviour
{
    CinemachineVirtualCamera myCam;

    [SerializeField]
    Transform target;

    void Start()
    {
        myCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        myCam.Follow = target;
        //.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = -target.GetComponent<BetweenBossPlayer>().distance / 10;

        if (target.GetComponent<BetweenBossPlayer>().distance >= 60 && target.GetComponent<BetweenBossPlayer>().distance <= 130)
        {
            myCam.m_Lens.OrthographicSize = target.GetComponent<BetweenBossPlayer>().distance;
        }
        //else
        //{
        //    myCam.m_Lens.OrthographicSize = 60;
        //}
    }
}
