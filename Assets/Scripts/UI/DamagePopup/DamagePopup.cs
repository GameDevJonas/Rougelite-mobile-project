using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public Color normalDamage, critDamage;

    public TextMeshPro myText;

    private void Start()
    {
        myText = GetComponent<TextMeshPro>();
        Destroy(gameObject, 2f);
    }

    public void OnPopup(bool isCrit, float damage)
    {
        if (isCrit)
        {
            myText.color = critDamage;
            //myText.faceColor = normalDamage;
            FindObjectOfType<CameraMagnitude>().StartShake(4f, damage / 2);
        }
        else
        {
            myText.color = normalDamage;
            //myText.faceColor = critDamage;
            FindObjectOfType<CameraMagnitude>().StartShake(4f, damage / 2);
        }
        
        myText.text = Mathf.RoundToInt(damage).ToString();

    }

}
