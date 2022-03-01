using UnityEngine;
using Mirror;
using System;
public class Trap : MonoBehaviour {
    public static event Action onTrapHitCall;
    ObjectInteractions objectInteractions;
    private void Start() {
        InitializeObjectInteractions();
    }
    private void Update() {
        InitializeObjectInteractions();
    }
    void InitializeObjectInteractions() {
        if (NetworkServer.active) {
            objectInteractions = ObjectInteractions.objectInteractionsInstance;
        }
        if (objectInteractions != null) {
            return;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            onTrapHitCall?.Invoke();
            FindObjectOfType<ObjectInteractions>().triggeredTrap = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<ObjectInteractions>().triggeredTrap = false;
        }
    }
}