using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehavior : MonoBehaviour
{
    public NodeState State;
    public int LevelNumber;

    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite completeSprite;

    SpriteRenderer sr;
    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    public bool CompleteLevel()
    {
        switch (State)
        {
            case NodeState.Locked:
                //display locked message
                return false;
            case NodeState.Open:
                //if the node is open then complete the level
                State = NodeState.Complete;
                return true;
            case NodeState.Complete:
                //show completed effect again
                return false;
            default:
                return false;
        }
    }
    public void ChangeSpriteByState()
    {
        switch (State)
        {
            case NodeState.Locked:
                sr.sprite = lockedSprite;
                break;
            case NodeState.Open:
                sr.sprite = openSprite;
                break;
            case NodeState.Complete:
                sr.sprite = completeSprite;
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