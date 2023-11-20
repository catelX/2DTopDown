using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Inverter : Node
    {
        private Node childNode;

        public Inverter(Node child)
        {
            childNode = child;
        }

        public override NodeState Evaluate()
        {
            switch (childNode.Evaluate())
            {
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    nodeState = NodeState.FAILURE;
                    break;
                case NodeState.FAILURE:
                    nodeState = NodeState.SUCCESS;
                    break;
                default:
                    break;
            }
            return nodeState;
        }
    }
}
