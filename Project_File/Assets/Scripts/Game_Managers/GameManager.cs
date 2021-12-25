using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager _instance;
    private void Awake() { _instance = this; }
    #endregion
    public Movement player;
}
