using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel; //indicates the current level the player is at
    public NodeBehavior[] LevelNodes;
    private void Start()
    {
        UpdateNodeStates();
    }
    public void UpdateNodeStates()
    {
        foreach (var node in LevelNodes)
        {
            if(node.LevelNumber == CurrentLevel)
            {
                node.State = NodeState.Open;
            }
            else if(node.LevelNumber < CurrentLevel)
            {
                node.State = NodeState.Complete;
            }
            else if(node.LevelNumber > CurrentLevel)
            {
                node.State = NodeState.Locked;
            }
            node.ChangeSpriteByState();
        }
    }
}
