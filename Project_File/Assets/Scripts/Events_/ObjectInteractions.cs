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
    public static event Action OnEnteredQuicksand;
    public static event Action OnExitedQuicksand;
    public static event Action OnEnteredPortal;
    public World worldToGoNext;
    public bool triggeredTrap = false;
    public bool enteredQuicksand = false;
    public bool teleporting = false;
    private void Update() { EventInvoker(); }
    void EventInvoker() {
        if (triggeredTrap) {OnTrapTriggered?.Invoke(); }
        if(enteredQuicksand) { OnEnteredQuicksand?.Invoke(); }
        else if(!enteredQuicksand) { OnExitedQuicksand?.Invoke(); }
        if (teleporting) { OnEnteredPortal?.Invoke(); }
    }
}