using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackStop : StackController
{
    public bool lockUp;
    public bool lockDown;
    public bool lockLeft;
    public bool lockRight;
    public override void Process()
    {
        PlayerMovement.instance.lockUp = lockUp;
        PlayerMovement.instance.lockDown = lockDown;
        PlayerMovement.instance.lockLeft = lockLeft;
        PlayerMovement.instance.lockRight = lockRight;

        MobileInput.instance.swipeLeft = false;
        MobileInput.instance.swipeRight = false;
        MobileInput.instance.swipeUp = false;
        MobileInput.instance.swipeDown = false;

        for (int i = 0; i < PlayerMovement.instance.stopPoints.Length; i++)
        {
            PlayerMovement.instance.stopPoints[i].SetActive(true);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(StringManager.TopStack))
        {
            Process();

            Vector3 newPos = gameObject.transform.position;
            newPos.y = PlayerMovement.instance.transform.position.y;
            PlayerMovement.instance.transform.position = newPos;

            if(gameObject.CompareTag(StringManager.WinPoint))
            {
                StartCoroutine(PlayerMovement.instance.ChangeWinAnim());
                GameManager.instance.SetGameState(GameState.Win);
            }

            gameObject.SetActive(false);
        }
    }
}
