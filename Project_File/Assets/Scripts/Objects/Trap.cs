using UnityEngine;
using Mirror;
public class Trap : MonoBehaviour {
    ObjectInteractions objectInteractions;
    private void Start() {
        InitializeObjectInteractions();
    }
    private void Update() {
        InitializeObjectInteractions();
    }
    void InitializeObjectInteractions() {
        if(NetworkServer.active) {
            objectInteractions = ObjectInteractions.objectInteractionsInstance;
        }
        if(objectInteractions != null) {
            return;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            FindObjectOfType<ObjectInteractions>().triggeredTrap = true;    
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<ObjectInteractions>().triggeredTrap = false;
        }
    }
}