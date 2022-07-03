using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestOpen;
    public float openChestTime;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TopStack")
        {
            Debug.Log("Win");
            StartCoroutine(OpenChest());
        }
    }

    private IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(openChestTime);
        chestOpen.SetActive(true);
        gameObject.SetActive(false);
    }
}
