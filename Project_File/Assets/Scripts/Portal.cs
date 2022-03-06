using UnityEngine;
using System;
public class Portal : MonoBehaviour {
    GameManager manager;

    private void Start() { 
        manager = GameManager._instance;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Portal")) {
            Debug.Log("TRIGGERED");
            manager.timeManager.triggeredPortal = true;
        }
    }
}