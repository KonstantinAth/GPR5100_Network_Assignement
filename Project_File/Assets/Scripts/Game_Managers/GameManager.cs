using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager _instance;
    private void Awake() { _instance = this; }
    #endregion
    public Movement player;
    public void Start() {
        InitializePlayer();
    }
    void InitializePlayer() {
        if (NetworkServer.active) {
            player = FindObjectOfType<Movement>();
        }
        if (player != null) {
            Debug.Log("Player NOT NULL ANYMORE");
            return;
        }
    }
    private void Update() {
        InitializePlayer();
    }
}