using UnityEngine;
public class EventManager : MonoBehaviour {
    GameManager instance;
    [SerializeField] ObjectInteractions objectInteractions;
    [SerializeField] Transform portals;
    public int portalIndex = 0;
    private void Start() {
        instance = GameManager._instance;
        InitializeEvents();
    }
    void InitializeEvents() {
        ObjectInteractions.OnTrapTriggered += ObjectInteractions_OnTrapTriggered;
        ObjectInteractions.OnEnteredQuicksand += ObjectInteractions_OnEnteredQuicksand;
        ObjectInteractions.OnExitedQuicksand += ObjectInteractions_OnExitedQuicksand;
        ObjectInteractions.OnEnteredPortal += ObjectInteractions_OnEnteredPortal;
    }
    private void ObjectInteractions_OnEnteredPortal() {
        instance.player.transform.position -= transform.forward;
        instance.cameraFollow.SetPositionToOtherPlayer();
        instance.player = objectInteractions.worldToGoNext.ThisWorldPlayer;
        instance.activePortal = portals.GetChild(portalIndex).GetComponent<Portal>();
    }
    private void ObjectInteractions_OnEnteredQuicksand() {
        instance.player.moveSpeed = 3;
    }
    private void ObjectInteractions_OnExitedQuicksand() {
        instance.player.moveSpeed = instance.player.startingSpeed;
    }
    private void ObjectInteractions_OnTrapTriggered() {
        instance.player.transform.position = instance.player.startingPosition;
    }
    private void OnDisable() {
        ObjectInteractions.OnTrapTriggered -= ObjectInteractions_OnTrapTriggered;
        ObjectInteractions.OnEnteredQuicksand -= ObjectInteractions_OnEnteredQuicksand;
        ObjectInteractions.OnExitedQuicksand -= ObjectInteractions_OnExitedQuicksand;
        ObjectInteractions.OnEnteredPortal -= ObjectInteractions_OnEnteredPortal;
    }
}