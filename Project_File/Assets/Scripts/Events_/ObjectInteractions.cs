using UnityEngine;
using System;
using System.Collections;
public class ObjectInteractions : MonoBehaviour {
    public static event Action OnEnteredQuicksand;
    public static event Action OnExitedQuicksand;
    public static event Action OnEnteredPortal;
    public World worldToGoNext;
    public bool triggeredTrap = false;
    public bool enteredQuicksand = false;
    public bool teleporting = false;
    public GameObject currentTrap;
    [Header("Death Configs")]
    [SerializeField] float timeToResetPosition = 3.0f;
    private void Update() { EventInvoker(); }
    void EventInvoker() {
        if(enteredQuicksand) { OnEnteredQuicksand?.Invoke(); }
        else if(!enteredQuicksand) { OnExitedQuicksand?.Invoke(); }
        if (teleporting) { OnEnteredPortal?.Invoke(); }
    }
}