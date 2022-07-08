using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSub : StackController
{
    public override void Process()
    {
        base.Process();
        PlayerMovement.instance.RemoveStack();
        PlayerMovement.instance.stackCount--;
        gameObject.SetActive(false);
    }
}
