using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
    int currentChildIndex = 0;

    protected override void OnStart()
    {
        currentChildIndex = 0;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        // var child = children[currentChildIndex];
        Node child = children[currentChildIndex];
        switch (child.Update())
        {
            case State.RUNNING:
                return State.RUNNING;
            case State.FAILURE:
                return State.FAILURE;
            case State.SUCCESS:
                currentChildIndex++;
                break;
        }

        return currentChildIndex == children.Count ? State.SUCCESS : State.RUNNING;
    }
}
