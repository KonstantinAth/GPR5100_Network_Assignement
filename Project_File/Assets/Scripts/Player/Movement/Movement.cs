using UnityEngine;
using Mirror;
[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(NetworkTransform))]
public class Movement : NetworkBehaviour {
    public Vector3 startingPosition;
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
    private void Update() {
        if (isServer) {
            if (!objectInteractionInstance_.triggeredTrap && !objectInteractionInstance_.teleporting) {
                Move();
                ApplyGravity();
            }
        }
    }
    void ObjectInit() {
        objectInteractionInstance_ = ObjectInteractions.objectInteractionsInstance;
        startingPosition = transform.position;
        playerController = GetComponent<CharacterController>();
        startingSpeed = moveSpeed;
        #region Might need
        //if(isClient) {
        //    for (int i = 0; i < rend.Length; i++) {
        //        rend[i].enabled = false;
        //    }
        //}
        #endregion
    }
    #region Movement & Drag/Gravity Functions
    void RotateRelativeToInput() {
        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 45.0f, 0.0f));
        fixedRotation = matrix.MultiplyPoint3x4(movement);
        Debug.Log($"MATRIX : {matrix}");
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
    }
}