using UnityEngine;
using Mirror;
using System;
public class Trap : MonoBehaviour {
    public static event Action onTrapHitCall;
    public static event Action OnTrapDeath; 
    ObjectInteractions objectInteractions;
    GameManager instance;
    private void Start() {
        InitializeObjectInteractions();
    }
    private void Update() {
        InitializeObjectInteractions();
    }
    void InitializeObjectInteractions() {
        if (NetworkServer.active ) {
            objectInteractions = ObjectInteractions.objectInteractionsInstance;
            instance = GameManager._instance;
        }
        //if (objectInteractions != null && instance!=null) {
        //    return;
        //}
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (instance!=null)
            {
                instance.DeathCount++;
            }
            onTrapHitCall?.Invoke();
            OnTrapDeath?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<ObjectInteractions>().triggeredTrap = false;
        }
    }
}