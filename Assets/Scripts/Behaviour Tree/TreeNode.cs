using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static TreeNode;

// Base Node
public abstract class TreeNode
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    protected NodeState state;

    public TreeNode()
    {
        state = NodeState.RUNNING;
    }

    public abstract NodeState Evaluate();
}

// Base Composite Node
public class CompositeNode : TreeNode
{
    protected List<TreeNode> children;

    public CompositeNode()
    {
        children = new List<TreeNode>();
    }

    public void AddChild(TreeNode child)
    {
        children.Add(child);
    }

    public override NodeState Evaluate()
    {
        throw new NotImplementedException();
    }
}
