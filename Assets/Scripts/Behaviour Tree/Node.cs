using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Node
    {
        protected NodeState nodeState;

        public abstract NodeState Evaluate();
    }

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}