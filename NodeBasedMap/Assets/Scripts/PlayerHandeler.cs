using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandeler : MonoBehaviour
{

    [SerializeField] Sprite shipRight;
    [SerializeField] Sprite shipUp;
    [SerializeField] Sprite shipDown;

    [SerializeField] LevelManager levelManager;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector2 movement;
    bool shipInsideNodeTriggerBox;
    Collider2D currentTriggerBox;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //get axis controls 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //change ship sprites
        ShipAnimation();
        //tries to enter a level by pressing [Space]
        EnterLevel();
    }
    private void FixedUpdate()
    {
        //update movement from axis controls
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Node"))
        {
            shipInsideNodeTriggerBox = true;
            currentTriggerBox = collision;
            Debug.Log("entered");
            //enlarge level node icon
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            shipInsideNodeTriggerBox = false;
        }
    }

    void ShipAnimation()
    {
        if (movement.x == 1)
        {
            sr.sprite = shipRight;
            sr.flipX = false;
        }
        else if (movement.x == -1)
        {
            sr.sprite = shipRight;
            sr.flipX = true;
        }

        if (movement.y == 1) sr.sprite = shipUp;
        else if (movement.y == -1) sr.sprite = shipDown;
    }
    void EnterLevel()
    {
        if (shipInsideNodeTriggerBox && Input.GetKeyDown(KeyCode.Space))
        {
            //enter level
            if(currentTriggerBox.GetComponent<NodeBehavior>().CompleteLevel())
            {
                levelManager.CurrentLevel++;
                levelManager.UpdateNodeStates();
            }
        }
    }
}
