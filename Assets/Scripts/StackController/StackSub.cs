using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSub : StackController
{
    public override void Process()
    {
        base.Process();
        PlayerMovement.instance.RemoveStack();
        Destroy(gameObject);
    }
}
