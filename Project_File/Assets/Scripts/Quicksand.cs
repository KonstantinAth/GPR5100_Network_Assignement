using UnityEngine;
using Mirror;
public class Quicksand : MonoBehaviour {
    ObjectInteractions objectInteractions;
    private void Start() { InitializeObjectInteractions(); }
    void InitializeObjectInteractions() { if (objectInteractions != null) { return; } }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("On Trigger");
            FindObjectOfType<ObjectInteractions>().enteredQuicksand = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Exited Trigger");
            FindObjectOfType<ObjectInteractions>().enteredQuicksand = false;
        }
    }
}