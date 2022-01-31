using UnityEngine;
public class EventManager : MonoBehaviour {
    GameManager instance;
    private void Start() {
        instance = GameManager._instance;
        InitializeEvents();
    }
    void InitializeEvents() {
        ObjectInteractions.OnTrapTriggered += ObjectInteractions_OnTrapTriggered;
    }
    private void ObjectInteractions_OnTrapTriggered() {
        instance.player.transform.position = instance.player.startingPosition;
    }
    private void OnDisable() {
        ObjectInteractions.OnTrapTriggered -= ObjectInteractions_OnTrapTriggered;
    }
}