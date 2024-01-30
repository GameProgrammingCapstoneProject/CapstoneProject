using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : ScriptableObject
{
    Node rootNode;
    public Node.State treeState = Node.State.RUNNING;

    public Node.State Update()
    {
        if(rootNode.state == Node.State.RUNNING)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }
}
