using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Selector : Node
    {
        protected List<Node> childNodes = new();

        public Selector(List<Node> children)
        {
            childNodes = children;
        }

        public override NodeState Evaluate()
        {
            foreach (var node in childNodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        return NodeState.RUNNING;
                    case NodeState.SUCCESS:
                        return NodeState.SUCCESS;
                    case NodeState.FAILURE:
                    default:
                        break;
                }
            }
            return NodeState.FAILURE;
        }
    }
}