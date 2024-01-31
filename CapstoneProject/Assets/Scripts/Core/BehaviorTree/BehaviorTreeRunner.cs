using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviorTree tree;
    // Start is called before the first frame update
    void Start()
    {
        //tree = tree.Clone(); //Open this later

        //tree = ScriptableObject.CreateInstance<BehaviorTree>();

        //// Action nodes
        //DebugLogNode log = ScriptableObject.CreateInstance<DebugLogNode>();
        //log.message = "HELLLLOOO WORLLLD";
        //DebugLogNode log2 = ScriptableObject.CreateInstance<DebugLogNode>();
        //log2.message = "How are you doing?";
        //DebugLogNode log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        //log3.message = "I'll see you later after a short time...";

        //WaitNode pause = ScriptableObject.CreateInstance<WaitNode>();
        //WaitNode pause2 = ScriptableObject.CreateInstance<WaitNode>();
        //WaitNode pause3 = ScriptableObject.CreateInstance<WaitNode>();

        ////Composite node
        //SequencerNode sequence = ScriptableObject.CreateInstance<SequencerNode>();
        //sequence.children.Add(log);
        //sequence.children.Add(pause);
        //sequence.children.Add(log2);
        //sequence.children.Add(pause2);
        //sequence.children.Add(log3);
        //sequence.children.Add(pause3);

        //// Decorator Node
        //RepeatNode loop = ScriptableObject.CreateInstance<RepeatNode>();
        //loop.child = sequence;

        //tree.rootNode = loop;
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
