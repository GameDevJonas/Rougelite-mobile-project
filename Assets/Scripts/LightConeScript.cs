using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightConeScript : MonoBehaviour
{
    public string direction;

    public Light2D light2D; 

    // Update is called once per frame
    public void RotateMeBaby(string dir, float HasEye)
    {
        if (HasEye > 0)
        {
            light2D = gameObject.GetComponent<Light2D>();
            light2D.pointLightOuterAngle = 160;
            light2D.pointLightInnerRadius = 15;
        }
        if (dir == "UL" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (dir == "U" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir == "UR" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (dir == "R" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (dir == "DR" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (dir == "D" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (dir == "DL" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (dir == "L" && HasEye <= 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir == "UL" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 60);
        }
        else if (dir == "U" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 15);
        }
        else if (dir == "UR" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -60);
        }
        else if (dir == "R" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -75);
        }
        else if (dir == "DR" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -150);
        }
        else if (dir == "D" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 195);
        }
        else if (dir == "DL" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 150);
        }
        else if (dir == "L" && HasEye > 0)
        {
            direction = dir;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 105);
        }
    }
}
