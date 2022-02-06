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
}