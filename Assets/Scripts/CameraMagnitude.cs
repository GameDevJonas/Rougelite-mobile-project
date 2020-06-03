using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMagnitude : MonoBehaviour
{
    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 ogPos = transform.localPosition;

        float currentTime = 0f;
        while (currentTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(ogPos.x + x, ogPos.y + y, ogPos.z);

            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = ogPos;
    }
}
