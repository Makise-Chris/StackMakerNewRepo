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
            if(PlayerMovement.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("Take 03"))
            {
                PlayerMovement.instance.animator.SetTrigger("IdleToState");
                PlayerMovement.instance.animator.SetTrigger("StateToMove");
            }
            Process();
        }
    }

    public virtual void Process()
    {
        Instantiate(newStack, transform.position, Quaternion.identity);
    }
}
