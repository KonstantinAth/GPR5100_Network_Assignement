using UnityEngine;
using System;
public class ObjectInteractions : MonoBehaviour {
    #region Singleton
    public static ObjectInteractions objectInteractionsInstance;
    private void Awake() {
        if (objectInteractionsInstance == null)
            objectInteractionsInstance = this;
    }
    #endregion
    public static event Action OnTrapTriggered;
    public bool triggeredTrap = false;
    private void Update() { EventInvoker(); }
    void EventInvoker() {
        if (triggeredTrap) {OnTrapTriggered?.Invoke(); }
        else { return; }
    }
}