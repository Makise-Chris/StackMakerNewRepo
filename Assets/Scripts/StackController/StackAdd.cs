using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAdd : StackController
{
    public override void Process()
    {
        base.Process();
        gameObject.tag = StringManager.TopStack;
        PlayerMovement.instance.animator.SetInteger("renwu", 1);
        PlayerMovement.instance.AddStack(gameObject);
        PlayerMovement.instance.stackCount++;
    }
}