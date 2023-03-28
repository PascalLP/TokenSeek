using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }


    public class Node
    {
        protected NodeState state;

        public Node _parent;
        protected List<Node> children = new List<Node>();

        public Node() 
        {
            _parent = null;
        }

        public Node(List<Node> children) 
        {
            foreach(Node child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node._parent= this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Dictionary containing the Node data
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            _data[key] = value;
        }

        public object GetData(string key)
        {
            object val = null;
            if (_data.TryGetValue(key, out val))
                return val;

            Node node = _parent;
            if (node != null)
                val = node.GetData(key);

            return val;
        }

        public bool ClearData(string key)
        {
            bool cleared = false;
            if(_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }

            Node node = _parent;
            if(node!= null)
                cleared = node.ClearData(key);

            return cleared;
        }
    }
}
