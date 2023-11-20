using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        private List<Node> childNodes = new();

        public Sequence(List<Node> children)
        {
            childNodes = children;
        }

        public override NodeState Evaluate()
        {
            bool isChildNodeRunning = false;
            foreach (var node in childNodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        isChildNodeRunning = true;
                        break;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.FAILURE:
                        return NodeState.FAILURE;
                    default:
                        break;
                }
            }
            nodeState = isChildNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return nodeState;
        }
    }
}