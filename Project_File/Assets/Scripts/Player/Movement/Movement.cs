using UnityEngine;
using Mirror;
using System.Collections;
using System;
[RequireComponent(typeof(CharacterController))]
public class Movement : NetworkBehaviour {
    public World worldToGoNext;
    [SerializeField] bool isFinalPlayer;
    public static event Action<World> OnEnteredPortal;
    PlayerSoundFX soundFx;
    public Vector3 startingPosition;
    private RaycastHit hit;
    [Header("Movement Variables & References")]
    [Range(0, 50)] public float moveSpeed;
    [SerializeField] [Range(0, 10)] float rotationSpeed = 5.0f;
    [Header("Gravity Settings")]
    [SerializeField] float gravity;
    [SerializeField] float objectMass;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform checkSphereTransform;
    [SerializeField] float checkSphereRadius = 0.4f;
    [SerializeField] Renderer[] rend;
    [SyncVar] public bool IsRunning;
    [Header("Animation Settings")]
    Animator characterAnimator;
    Vector3 movement;
    Vector3 fixedRotation;
    Vector3 velocity;
    float horizontalInput;
    float verticalInput;
    CharacterController playerController;
    ObjectInteractions objectInteractionInstance_;
    public float startingSpeed;
    GameManager instance;
    public bool isThisServer;
    private void Start() { ObjectInit(); }
    private void OnEnable() {
        startingPosition = transform.position;
    }
    private void OnDisable() { }
    private void Update() {
        if (instance.GameFinished || instance.timeManager.GamePaused) return;
        if(instance.timeManager.triggeredPortal) {
            if (isFinalPlayer) { instance.GameFinished = true; }
            else { OnEnteredPortal?.Invoke(worldToGoNext); }
        }
        if (isServer) {
            if (!objectInteractionInstance_.triggeredTrap && !instance.timeManager.triggeredPortal) {
                Move();
                ApplyGravity();
                MovementAnimation();
            }
        }
        GroundtypeCheck();
    }
    void ObjectInit() {
        isThisServer = isServer ? true : false;
        isFinalPlayer = gameObject.name.Equals("Ghost") ? true : false;
        playerController = GetComponent<CharacterController>();
        startingSpeed = moveSpeed;
        characterAnimator = GetComponent<Animator>();
        soundFx = GetComponent<PlayerSoundFX>();
        instance = GameManager._instance;
        objectInteractionInstance_ = instance.objectInteractions;
    }
    public void GroundtypeCheck()
    {
        if(Physics.Raycast(transform.position+transform.up, Vector3.down, out hit, 3f, groundLayer))
        {
                if (hit.collider.tag == "Sand")
                {
                    soundFx.TagName = "Sand";
                }
                else if(hit.collider.tag == "Road")
                {
                    soundFx.TagName = "Road";
                }
                else if(hit.collider.tag == "Snow")
                {
                    soundFx.TagName = "Snow";
                }
                else if(hit.collider.tag=="Water")
                {
                    soundFx.TagName = "Water";
                }
                else if(hit.collider.tag=="Dirt")
                {
                soundFx.TagName = "Dirt"; 
                }
                else if (hit.collider.tag == "Bush")
                {
                soundFx.TagName = "Bush";
                }
        }
        
    }
    #region Animation Setter
    void MovementAnimation() {
        if(horizontalInput != 0 || verticalInput != 0) {
            IsRunning = true;
        }
        else { IsRunning = false; }
        characterAnimator.SetBool("IsRunning", IsRunning);
    }
    #endregion
    #region Movement & Drag/Gravity Functions
    void RotateRelativeToInput() {
        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 45.0f, 0.0f));
        fixedRotation = matrix.MultiplyPoint3x4(movement);
        if (movement != Vector3.zero) {
            Vector3 relative = (transform.position + fixedRotation) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
    void Inputs() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized; 
    }
    void Move() {
        Inputs();
        RotateRelativeToInput();
        playerController.Move(fixedRotation * moveSpeed * Time.deltaTime);
    }
    bool IsGrounded() {
        if(Physics.CheckSphere(checkSphereTransform.position, checkSphereRadius, groundLayer))
        { return true; }
        return false;
    }
    void ApplyGravity() {
        if (!IsGrounded()) {
            velocity.y += gravity * objectMass * Mathf.Pow(Time.deltaTime, 2);
            playerController.Move(velocity);
        }
        else { velocity.y = -0.2f; }
    }
    #endregion
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkSphereTransform.position, checkSphereRadius);
        Gizmos.DrawRay(transform.position, Vector3.down *3f);
    }
}