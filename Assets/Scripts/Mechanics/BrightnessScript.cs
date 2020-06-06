using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BrightnessScript : MonoBehaviour
{
    public GameObject rue;
    public Light2D light2d;
    public PlayerStats PlayerStats;


    // Update is called once per frame
    void Update()
    {
        if (rue == null)
        {
            rue = GameObject.FindGameObjectWithTag("Player");
            PlayerStats = rue.GetComponent<PlayerStats>();
            light2d = gameObject.GetComponent<Light2D>();
        }
        if (PlayerStats.EnemiesVisibleOutsideLight.Value > 0)
        {
            light2d.intensity = 0.1f;
        }
    }
}
