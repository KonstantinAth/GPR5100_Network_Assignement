using UnityEngine;
using TMPro;
using Mirror;
using System;
public class TimeManager : NetworkBehaviour {
    [SerializeField] private float mapTimeInMinutes;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject GameHUDCanvas;
    [SyncVar (hook = nameof(InitializeTimeCanvas))] public float timeRemaining;
    bool clientAndServerActive => NetworkServer.connections.Count >= 2;
    float seconds;
    float minutes;
    bool isInitialized = false;
    public void InitializeTimeCanvas(float mapTimeRemaining, float timeRemaining) {
        if (isInitialized) return;
        Debug.Log("INITIALIZING...");
        GameHUDCanvas.SetActive(true);
        isInitialized = true;
        if (isServer) SetTime();
    }
    private void Start() {
        ObjectInit();
        DisplayTime();
    }
    private void Update() {
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
    }
    
    void AddExtraTime(int hourglasses)
    {
        timeRemaining += timeRemaining * hourglasses / 100;
    }
}