using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables & References")]
    [SerializeField] [Range(0, 50)] float moveSpeed;
    [SerializeField] [Range(0, 10)] float rotationSpeed = 5.0f;
    Vector3 movement;
    Vector3 direction;
    Vector3 fixedRotation;
    float horizontalInput;
    float verticalInput;
    CharacterController playerController;
    private void Start() {
        playerController = GetComponent<CharacterController>();
    }
    private void Update() {
        Move();
        RotateRelativeToInput();
    }
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
        playerController.Move(fixedRotation* moveSpeed * Time.deltaTime);
    }
}
//[Header("Movement Variables & References")]
//[SerializeField] [Range(0, 200)] float moveSpeed;
//[SerializeField] [Range(0, 100)] float rotationSpeed;
//Vector3 movement;
//Vector3 direction;
//float horizontalInput;
//float verticalInput;
//Rigidbody playerRB;
//private void Start() { Initialization(); }
//private void Update() { Move(); }
//void Initialization()
//{
//playerRB = GetComponent<Rigidbody>();
//}
//void Inputs()
//{
//horizontalInput = Input.GetAxisRaw("Horizontal");
//verticalInput = Input.GetAxisRaw("Vertical");
//movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
//direction = transform.TransformDirection(movement);
//}
//void Move()
//{
//Inputs();
//playerRB.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
//}