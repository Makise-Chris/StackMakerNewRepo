using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public string stackTag;
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(StringManager.TopStack))
        {
            Process();
        }
    }

    public virtual void Process()
    {
        ObjectPooler.instance.SpawnFromPool(stackTag, transform.position, Quaternion.identity);
    }
}
