using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public GameObject newStack;
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TopStack")
        {
            Process();
        }
    }

    public virtual void Process()
    {
        Instantiate(newStack, transform.position, Quaternion.identity);
    }
}
