using UnityEngine;
using Mirror;
[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(NetworkTransform))]
public class Movement : NetworkBehaviour {
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
    public bool IsRunning;
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
    private void Start() {
        ObjectInit();
    }
    private void OnEnable() { startingPosition = transform.position; }
    private void Update() {
        if (isServer) {
            if (!objectInteractionInstance_.triggeredTrap && !objectInteractionInstance_.teleporting) {
                Move();
                ApplyGravity();
                MovementAnimation();
            }
        }
        GroundtypeCheck();
    }
    void ObjectInit() {
        objectInteractionInstance_ = ObjectInteractions.objectInteractionsInstance;
        playerController = GetComponent<CharacterController>();
        startingSpeed = moveSpeed;
        characterAnimator = GetComponent<Animator>();
        soundFx = GetComponent<PlayerSoundFX>();
        #region Might need
        //if(isClient) {
        //    for (int i = 0; i < rend.Length; i++) {
        //        rend[i].enabled = false;
        //    }
        //}
        #endregion
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
        else {
            velocity.y = -0.2f;
        }
    }
    #endregion
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkSphereTransform.position, checkSphereRadius);
        Gizmos.DrawRay(transform.position, Vector3.down *3f);
    }
}