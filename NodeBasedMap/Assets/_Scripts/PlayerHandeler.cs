using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandeler : MonoBehaviour
{
    //this class handles the movement of the player ship and activating the level nodes.

    [SerializeField] LevelManager levelManager; // a refrence to the level manager.

    #region ShipSprites
    [Header("Sprites")]
    [SerializeField] Sprite shipRight;
    [SerializeField] Sprite shipUp;
    [SerializeField] Sprite shipDown;
    #endregion

    [Header("Movement")]
    [SerializeField] float moveSpeed;

    #region PrivateRefrences&Variables
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    private Vector2 movement;
    private bool shipInsideNodeTriggerBox;
    private Collider2D currentTriggerBox;
    #endregion
    private void Start()
    {
        //connecting gameobject refrences
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
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

    #region TriggerDetection
    //handles the detection of trigger boxes of the level nodes on the map.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Node"))
        {
            shipInsideNodeTriggerBox = true;
            currentTriggerBox = collision;
            //enlarge level node icon
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            shipInsideNodeTriggerBox = false;
            ////shrink level node icon
        }
    }
    #endregion
    void ShipAnimation()
    {
        //changes the sprites of the ship according to its movement direction.
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
        //if a trigger box is detected and the player presses space the function tries to complete the level.
        if (shipInsideNodeTriggerBox && Input.GetKeyDown(KeyCode.Space))
        {
            if(currentTriggerBox.GetComponent<NodeBehavior>().CompleteLevel())
            {
                levelManager.UpdateNodeStates();
                audioSource.Play();
            }
        }
    }
}
