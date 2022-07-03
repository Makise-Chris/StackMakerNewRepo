using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAdd : StackController
{
    public override void Process()
    {
        base.Process();
        gameObject.tag = "TopStack";
        base.Process();
        PlayerMovement.instance.AddStack(gameObject);
    }
}
