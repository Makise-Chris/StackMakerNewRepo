using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;
    private int finalAngle;

    private IEnumerator Spin()
    {
        randomValue = Random.Range(30, 50);
        timeInterval = 0.05f;

        for(int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, -22.5f);

            if (i > Mathf.RoundToInt(randomValue * 0.5f))
            {
                timeInterval = 0.15f;
            }

            if(i>Mathf.RoundToInt(randomValue * 0.85f))
            {
                timeInterval = 0.35f;
            }

            yield return new WaitForSeconds(timeInterval);
        }

        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
        {
            transform.Rotate(0, 0, -22.5f);
        }

        finalAngle = (Mathf.RoundToInt(transform.eulerAngles.z) + 90) % 360;

        switch (finalAngle)
        {
            case 0:
                Debug.Log("Win 10");
                break;
            case 45:
                Debug.Log("Win 20");
                break;
            case 90:
                Debug.Log("Win 50");
                break;
            case 135:
                Debug.Log("Win 80");
                break;
            case 180:
                Debug.Log("Win 100");
                break;
            case 225:
                Debug.Log("Win 120");
                break;
            case 270:
                Debug.Log("Win 150");
                break;
            case 315:
                Debug.Log("Win 200");
                break;
        }
    }

    public void Gacha()
    {
        StartCoroutine(Spin());
    }
}
