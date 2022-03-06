using UnityEngine;
using System;
public class Portal : MonoBehaviour {
    GameManager manager;
    [SerializeField] World worldToGoNext;
    [SerializeField] bool isFinalPlayer;
    public static event Action<World> OnEnteredPortal; 
    private void Start() { 
        manager = GameManager._instance;
        isFinalPlayer = this.transform == transform.parent.GetChild(transform.parent.childCount - 1);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Portal")) {
            Debug.Log("TRIGGERED");
            if (isFinalPlayer) { manager.GameFinished = true; }
            else { OnEnteredPortal?.Invoke(worldToGoNext); }
        }
    }
}