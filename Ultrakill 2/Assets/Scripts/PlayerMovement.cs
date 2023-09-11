using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.Mouse2;
    public KeyCode slideKey = KeyCode.LeftControl;
    // Below may be temporary
    public KeyCode vaultKey = KeyCode.E;

    [Header("Speed")]
    private float moveSpeed;  // Final Speed
    public float walkSpeed;   // Walk
    public float slideSpeed;  // Slide
    public float wallrunSpeed;// Wallrun

    [Header("Velocity")]
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public int airJumps;
    private int currentAirJumps;
    private bool readyToJump;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;
    public float slideYScale;
    private float startYScale;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Dashing")]
    public const float dashDistance = 10f;
    public const float dashDuration = 0.1f;
    public const float dashCooldown = 1f;

    [Header("Dash Settings")]
    public bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector3 dashDirection;

    private Vector3 velocity;
    public LayerMask obstacleMask;
    

    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Wall Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Wall Exiting")]
    private bool exitingWall;
    public float exitWallNum;
    private float exitWallTimer;

    [Header("Wall Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float defaultFov;
    public float dashFov;
    public float wallrunFov;
    public float wallrunTilt;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded = true;

    [Header("References")]
    float horizontalInput;
    float verticalInput;
    public Transform orientation;
    public Transform playerObj;
    public Transform playerCam;
    Rigidbody rb;
    Vector3 moveDirection;

    [Header("Player State")]
    public bool freeze;
    public bool walking;
    public bool wallrunning;
    public bool sliding;
    public bool dashing;
    public bool jumping;

    private void StateHandler()
    {
        if (freeze)
        {
            rb.velocity = Vector3.zero;
            moveSpeed = 0f;
        }
        else if (wallrunning)
            moveSpeed = wallrunSpeed;

        else if (sliding)
            moveSpeed = slideSpeed;

        else
            moveSpeed = walkSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        currentAirJumps = airJumps;
        startYScale = playerObj.localScale.y;
        cam.DoFov(defaultFov);
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            // Get the input direction
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
            dashDirection = orientation.transform.TransformDirection(inputDirection);
            dashTimer = 0f;
            dashCooldownTimer = dashCooldown;
            velocity = dashDirection * dashDistance / dashDuration;
            print("asdas");
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Double Jump Out of Wallrun (to do normal remove the wallrunning part
        if (grounded || wallrunning)
            currentAirJumps = airJumps;

        MyInput();
        SpeedControl();
        StateHandler();
        CheckForWall();
        WallRunningStateMachine();
        
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Check if the dash has reached its duration
        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
            }
        }

        if (grounded && dashing == false)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        
        MovePlayer();

        if (isDashing)
        {
            rb.velocity = velocity;
        }

        if (sliding)
            SlidingMovement();
        if (wallrunning)
            WallRunningMovement();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jumping
        if (Input.GetKey(jumpKey) && readyToJump && (!jumping || grounded) && (grounded || currentAirJumps > 0))
        {
            readyToJump = false;
            currentAirJumps -= 1;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyUp(jumpKey))
            jumping = false;


        // Sliding
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
            StartSlide();

        if (Input.GetKeyUp(slideKey) && sliding)
            StopSlide();

        // Dashing 
        
    }

    // Jump Function
    private void Jump()
    {
        jumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // Delayed Reset
    private void ResetJump()
    {
        readyToJump = true;
    }

    // Starting Slide
    private void StartSlide()
    {
        sliding = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        slideTimer = maxSlideTime;
    }

    // Sliding Movement + Sliding on Slope
    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (!OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }

        else
            rb.AddForce(GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);

        if (slideTimer <= 0)
            StopSlide();
    }

    // Sliding On Slope
    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    // Slope Direction Checker
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    // End Slide
    private void StopSlide()
    {
        sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

    // Dashing
    /*
    private void Dash()
    {
        isDashing = true;
        // Get the input direction
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        dashDirection = transform.TransformDirection(inputDirection);
        dashTimer = 0f;
        dashCooldownTimer = dashCooldown;
        velocity = dashDirection * dashDistance / dashDuration;
    }
    
    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3();
    
        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;
        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;
        return direction.normalized;
        
    }
    */
    // Checking For Walls on Left and Right
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private void WallRunningStateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((wallLeft || wallRight) && verticalInput > 0 && !grounded && !exitingWall)
        {
            if (!wallrunning)
                StartWallRun();
            if (wallRunTimer > 0)
                wallRunTimer -= Time.deltaTime;
            if (wallRunTimer <= 0 && wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallNum;
            }
            if (Input.GetKeyDown(jumpKey))
                WallJump();
        }

        else if (exitingWall)
        {
            if (wallrunning)
                StopWallRun();
            if (exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;
            // grounded in this statment stop you from wallrunning on the same wall indefinetly but it makes it impossible to wallrunin on two things without touching the ground first, fix later
            if (exitWallTimer <= 0 && grounded)
                exitingWall = false;
        }

        else
        {
            if (wallrunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
    {
        wallrunning = true;
        wallRunTimer = maxWallRunTime;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        cam.DoFov(wallrunFov);
        if (wallLeft) cam.DoTilt(-wallrunTilt);
        if (wallRight) cam.DoTilt(wallrunTilt);
    }

    private void WallRunningMovement()
    {
        // Gravity While Wallrun
        rb.useGravity = useGravity;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        if (useGravity)
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
    }

    private void StopWallRun()
    {
        wallrunning = false;
        cam.DoFov(defaultFov);
        cam.DoTilt(0f);
        if (!useGravity)
        {
            rb.useGravity = true;
        }
    }

    private void WallJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallNum;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
