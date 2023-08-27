using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public NodeBehavior[] LevelNodes; // an  array that stores all level nodes refrences

    bool firstTimeUpdatingNodes = true; //indicates whether its the first time updating the node states.
    private void Start()
    {
        //initiate the node states for the first time (all levels exepet the starting level are locked).
        UpdateNodeStates();
    }
    public void UpdateNodeStates()
    {
        foreach (var node in LevelNodes)
        {
            if (node.ParentNode == null && !node.StartingNode)
            {
                //displays an error message incase one of the level nodes has no Parent node assigned.
                if (node.BonusLevel)
                    Debug.Log($"Error: bonus level node {node.LevelNumber} has no father node.");
                else
                    Debug.Log($"Error: level node {node.LevelNumber} has no father node.");
                return;
            }
            if (node.StartingNode)
            {
                //initializes the starting level node because it has no Parent node.
                if (firstTimeUpdatingNodes)
                {
                    node.State = NodeState.Open;
                    firstTimeUpdatingNodes = false;
                    continue;
                }
                else
                {
                    node.ChangeSpriteByState();
                    continue;
                }

            }
            switch (node.ParentNode.State)
            {
                //checks the current node's Parent state and changes the current node state accordingly.
                case NodeState.Locked:
                    node.State = NodeState.Locked;
                    break;
                case NodeState.Open:
                    node.State = NodeState.Locked;
                    break;
                case NodeState.Complete:
                    if (node.State == NodeState.Locked) node.State = NodeState.Open;
                    break;
                default:
                    break;
            }
            node.ChangeSpriteByState(); //update the sprites accordingly.
        }
    }
}
