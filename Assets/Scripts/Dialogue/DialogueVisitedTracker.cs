using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Dialogue
{
    public class DialogueVisitedTracker : MonoBehaviour
    {
        private DialogueRunner _dialogueRunner;
        private readonly Dictionary<string, int> _visitedNodes = new Dictionary<string, int>();

        private void Start()
        {
            _dialogueRunner = GetComponent<DialogueRunner>();
            _dialogueRunner.AddFunction("visited", 1, parameters => Visited(parameters[0].AsString));
        }

        /// <summary> Called by the Dialogue Runner to notify us that a node finished running. </summary>
        /// <param name="nodeName"> Name of the node that has completed running. </param>
        public void NodeComplete(string nodeName)
        {
            if (_visitedNodes.ContainsKey(nodeName))
                _visitedNodes[nodeName] += 1;
            else
                _visitedNodes[nodeName] = 1;
        }

        public int NodeNumberOfVisits(string nodeName)
        {
            _visitedNodes.TryGetValue(nodeName, out int numberOfVisits);
            return numberOfVisits;
        }

        public bool Visited(string nodeName)
        {
            return _visitedNodes.ContainsKey(nodeName);
        }

    }
}
