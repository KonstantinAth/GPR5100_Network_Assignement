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
            Debug.Log("OBJECT INTERACTIONS NOT NULL ANYMORE");
            return;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("TRIGGERED");
            FindObjectOfType<ObjectInteractions>().triggeredTrap = true;    
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("EXITED TRIGGER");
            //objectInteractions.triggeredTrap = false;
            FindObjectOfType<ObjectInteractions>().triggeredTrap = false;
        }
    }
}