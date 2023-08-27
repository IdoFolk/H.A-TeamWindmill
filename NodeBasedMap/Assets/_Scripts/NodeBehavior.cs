using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeBehavior : MonoBehaviour
{
    //this class determines the behavior of the level nodes in the game.

    [HideInInspector] public NodeState State; //the current state of the node, can be either locked, open or complete.
    public NodeBehavior ParentNode; //a refrence to the previous level node that opens the current node.

    #region ImportantVariables
    [Space]
    public int LevelNumber;
    public bool BonusLevel;
    [HideInInspector] public bool StartingNode;
    [Space]
    #endregion

    #region sprites&Text
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite completeSprite;

    [Header("Level")]
    [SerializeField] TextMeshPro levelNumberText;
    [Header("Bonus Level")]
    [SerializeField] Sprite incompleteStarSprite;
    [SerializeField] Sprite completeStarSprite;

    [Space]
    [SerializeField] Animator nodeAnimator;
    [SerializeField] SpriteRenderer nodeSR;
    [SerializeField] SpriteRenderer StarSR;
    #endregion
    private void Awake()
    {
        //defining the starting node (that has no Parent node)
        if (LevelNumber == 1 && !BonusLevel)
            StartingNode = true;
    }

    private void Start()
    {
        //updating text and star sprites at the start
        if (BonusLevel)
            StarSR.sprite = incompleteStarSprite;
        else
            levelNumberText.text = LevelNumber.ToString();
    }
    public bool CompleteLevel()
    {
        //this function tries to complete the level according to its state. (it will only succeed if the level is open).
        switch (State)
        {
            case NodeState.Locked:
                //if the node is locked then display a message
                Debug.Log("Level is currently locked...");
                return false;
            case NodeState.Open:
                //if the node is open then complete the level
                State = NodeState.Complete;
                nodeAnimator.SetTrigger("Complete Level"); //show completed effect
                return true;
            case NodeState.Complete:
                //if the node is already completed then "complete" it again
                nodeAnimator.SetTrigger("Complete Level"); //show completed effect again
                return false;
            default:
                return false;
        }
    }
    public void ChangeSpriteByState()
    {
        //this function looks at the current state of the node and changes it sprites accordingly.
        switch (State)
        {
            case NodeState.Locked:
                nodeSR.sprite = lockedSprite;
                break;
            case NodeState.Open:
                nodeSR.sprite = openSprite;
                break;
            case NodeState.Complete:
                nodeSR.sprite = completeSprite;
                if (BonusLevel) StarSR.sprite = completeStarSprite;
                break;
        }
    }

}
public enum NodeState
{
    Locked,
    Open,
    Complete
}