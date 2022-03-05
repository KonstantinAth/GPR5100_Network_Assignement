using UnityEngine;
public class GameManager : MonoBehaviour {
    #region Singleton

    public static GameManager _instance;
    private void Awake() { _instance = this; }
    #endregion
    public ObjectInteractions objectInteractions;
    public Movement player;
    public CameraFollow cameraFollow;
    public Portal ActivePortal;
    public UIManager UIManager;
    public bool GameFinished;
    public TimeManager timeManager;
    public int DeathCount;
    public GameObject entryCamera;
    public void SetCursorState(bool cursorState) {
        Cursor.visible = cursorState;
        if(Cursor.visible) { Cursor.lockState = CursorLockMode.None; }
        else { Cursor.lockState = CursorLockMode.Locked; }
    }
}