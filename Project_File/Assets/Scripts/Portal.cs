using UnityEngine;
using System.Collections;
using System;
public class Portal : MonoBehaviour {
    GameManager manager;
    [SerializeField] World worldToGoNext;
    [SerializeField] bool isFinalDoor;
    public static event Action<World> OnEnteredPortal; 
    private void Start() { 
        manager = GameManager._instance;
        isFinalDoor = this.transform == transform.parent.GetChild(transform.parent.childCount - 1);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (isFinalDoor) { manager.GameFinished = true; }
            else { OnEnteredPortal?.Invoke(worldToGoNext); }
        }
    }
}