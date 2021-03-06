using UnityEngine;
using Mirror;
using System;
public class Trap : MonoBehaviour {
    public static event Action onTrapHitCall;
    public static event Action OnTrapDeath; 
    GameManager instance;
    private void Start() { InitializeObjectInteractions(); }
    private void Update() { InitializeObjectInteractions(); }
    void InitializeObjectInteractions() { if (NetworkServer.active ) { instance = GameManager._instance; } }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Trap") && GetComponent<Movement>().isClient) {
            FindObjectOfType<ObjectInteractions>().triggeredTrap = true;
            Debug.Log("HITTED TRAP");
            if (instance!=null){ instance.timeManager.DeathCount++; }
            onTrapHitCall?.Invoke();
            OnTrapDeath?.Invoke();
        }
    }
}