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
<<<<<<< HEAD
        if (other.CompareTag("Trap") && GetComponent<Movement>().isClient) {
=======
        if (other.CompareTag("Player")) {
>>>>>>> 6afdacf8fd0ce20a61bd7c70b2f82724a5c2f296
            FindObjectOfType<ObjectInteractions>().triggeredTrap = true;
            Debug.Log("HITTED TRAP");
            if (instance!=null){ instance.timeManager.DeathCount++; }
            onTrapHitCall?.Invoke();
            OnTrapDeath?.Invoke();
        }
    }
}