using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightConeScript : MonoBehaviour
{
    public string direction;


    // Update is called once per frame
    public void RotateMeBaby(string dir, float HasEye)
    {
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
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (dir == "U" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, 15);
        }
        else if (dir == "UR" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, -20);
        }
        else if (dir == "R" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, -75);
        }
        else if (dir == "DR" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, -120);
        }
        else if (dir == "D" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, -165);
        }
        else if (dir == "DL" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, 150);
        }
        else if (dir == "L" && HasEye > 0)
        {
            direction = dir;
            transform.Rotate(0, 0, 105);
        }
    }
}
