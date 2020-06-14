using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeOnIntro : MonoBehaviour
{
    CinemachineVirtualCamera vCam;

    public Vector2 shakeAmp, shakeFreq;

    BossSound sound;

    void Start()
    {
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        sound = GetComponent<BossSound>();
    }

    public void DoShake()
    {
        StartCoroutine(CamShake());
    }

    IEnumerator CamShake()
    {
        //Starts cam shake
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Random.Range(shakeAmp.x, shakeAmp.y);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Random.Range(shakeFreq.x, shakeFreq.y);
        yield return new WaitForSeconds(.3f);
        //Resets cam shake
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        StopAllCoroutines();
        yield return null;
    }
}
