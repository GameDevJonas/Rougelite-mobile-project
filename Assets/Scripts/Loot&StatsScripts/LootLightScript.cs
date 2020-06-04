using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LootLightScript : MonoBehaviour
{
    public Light2D light2d;
    // Start is called before the first frame update
    void Awake()
    {
        light2d = gameObject.GetComponent<Light2D>();
        StartCoroutine("FadeIn");
    }
    IEnumerator FadeIn()
    {
        float duration = 2.5f;//time you want it to run
        float interval = 0.2f;//interval time between iterations of while loop
        light2d.intensity = 0.0f;
        while (duration >= 0.0f)
        {
            light2d.intensity += 0.03f;

            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }
    }
}
