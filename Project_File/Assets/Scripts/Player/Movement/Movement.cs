using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement Variables & References")]
    [SerializeField] [Range(0, 50)] float moveSpeed;
    [SerializeField] [Range(0, 10)] float rotationSpeed = 5.0f;
    [Header("Gravity Settings")]
    [SerializeField] float gravity;
    [SerializeField] float objectMass;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform checkSphereTransform;
    [SerializeField] float checkSphereRadius = 0.4f;
    Vector3 movement;
    Vector3 fixedRotation;
    Vector3 velocity;
    float horizontalInput;
    float verticalInput;
    CharacterController playerController;
    private void Start() {
        playerController = GetComponent<CharacterController>();
    }
    private void Update() {
        Move();
        ApplyGravity();
    }
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
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkSphereTransform.position, checkSphereRadius);
    }
}