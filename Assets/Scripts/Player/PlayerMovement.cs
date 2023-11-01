using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    
    [Header("References")]
    [SerializeField] private Transform playerObj;
    public bool movementConstraint;
    Rigidbody rb;
    public bool camCon1;
    public bool camCon2;

    [Header("Movement")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float backwardsSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private bool stunMode;
    [SerializeField] private bool backwardsMove;
    private float moveSpeed;
    private float rotationSpeedStore;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public MovementState state;    

    [Header("Crouching")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    bool grounded;

    public enum MovementState{

        walking,
        sprinting,
        crouching
    }

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = transform.localScale.y;
        camCon2 = true;
        rotationSpeedStore = rotationSpeed;

        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movementConstraint = false;
    }

    // Update is called once per frame
    void Update(){
        
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if(!movementConstraint){
            PlayerInput();
            SpeedControl();
            StateHandler();
            
            if(stunMode){
                rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
                rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                stunMode = false;
            }

        }else{
            stunMode = true;            
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // Handle drag
        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }

    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate(){
        
        if(!movementConstraint)
            MovePlayer();        
    }

    private void PlayerInput(){
       
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Backwards Speed
        if(Input.GetKeyDown(KeyCode.S)){
            moveSpeed = backwardsSpeed;
            backwardsMove = true;
        }
        if(Input.GetKeyDown(KeyCode.W)){
            moveSpeed = walkSpeed;
            backwardsMove = false;
            moveSpeed = walkSpeed;
        }

        // Start crouch
        if(Input.GetKeyDown(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // Stop crouch
        if(Input.GetKeyUp(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        } 
    }

    private void StateHandler(){

        // Mode - Crouching
        if(Input.GetKey(crouchKey)){
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        if(grounded && Input.GetKey(sprintKey) && !backwardsMove){
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            /*
                Quando a personagem corre faz barulho - Fazer barulho de acordo com um timer(WaitForSeconds)
                
                HearingManager.Instance.OnSoundEmitted(gameObject, transform.position, EHeardSoundCategory.EFootstep, 1f);
            */
        }

        // Mode - Walking
        else if(grounded){
            state = MovementState.walking;
        }
    }

    private void MovePlayer(){
        
        //Rotate character
        playerObj.Rotate(Vector3.up * horizontalInput * rotationSpeed * (100f * Time.deltaTime));
        
        // Calculate movement direction
        moveDirection = playerObj.forward * verticalInput;
        
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl(){

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity if needed
        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }
    }
}










