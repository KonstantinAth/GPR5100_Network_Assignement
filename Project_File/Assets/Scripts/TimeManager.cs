using UnityEngine;
using TMPro;
using Mirror;
using System;
public class TimeManager : NetworkBehaviour {
    [SerializeField] private float mapTimeInMinutes;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject GameHUDCanvas;
    GameManager instance;
    [SyncVar(hook = nameof(InitializeTimeCanvas))] public float timeRemaining;
    bool clientAndServerActive => NetworkServer.connections.Count >= 2;
    float seconds;
    float minutes;
    bool isInitialized = false;
    public void InitializeTimeCanvas(float mapTimeRemaining, float timeRemaining) {
        if (isInitialized) return;
        Debug.Log("INITIALIZING...");
        GameHUDCanvas.SetActive(true);
        isInitialized = true;
        instance = GameManager._instance;
        if (isServer) SetTime();
    }
    private void OnEnable() {
        Inventory.onRemoveCall += AddExtraTime;
    }
    private void OnDisable() {
        Inventory.onRemoveCall -= AddExtraTime;
    }
    private void Start() {
        ObjectInit();
        DisplayTime();
    }
    private void Update() {
        if (instance.GameFinished) return;
        if (isServer) SetTime();
        DisplayTime();
    }
    void ObjectInit() {
        timeRemaining = mapTimeInMinutes * 60;
    }
    void SetTime() {
        if (clientAndServerActive) {
            timeRemaining -= Time.deltaTime;
            seconds = Mathf.Floor(timeRemaining % 60);
            minutes = Mathf.Floor(timeRemaining / 60);
        }
    }
    void DisplayTime() {
        seconds = Mathf.Floor(timeRemaining % 60);
        minutes = Mathf.Floor(timeRemaining / 60);
        timeText.text = $"Time Remaining : {minutes}:{seconds}";
        if(timeRemaining <= 0) {
            timeRemaining = 0;
            instance.player.GetComponent<Movement>().enabled = false;
            instance.player.GetComponent<CharacterController>().enabled = false;
            instance.UIManager.SetLoseCanvasState(true);
            instance.SetCursorState(cursorState: false);
        }
    }
    void AddExtraTime(int hourglasses) {
        timeRemaining += timeRemaining * hourglasses / 100 * 10;
    }
}