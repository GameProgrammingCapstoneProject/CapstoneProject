using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : MonoBehaviour
{
    public List<Node> children = new List<Node>();
}
