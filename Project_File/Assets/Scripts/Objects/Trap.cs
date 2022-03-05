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
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<ObjectInteractions>().triggeredTrap = true;
            Debug.Log("HITTED TRAP");
            if (instance!=null){ instance.DeathCount++; }
            onTrapHitCall?.Invoke();
            OnTrapDeath?.Invoke();
        }
    }
}