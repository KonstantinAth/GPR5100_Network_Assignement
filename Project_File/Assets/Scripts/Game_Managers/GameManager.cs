using UnityEngine;
public class GameManager : MonoBehaviour {
    #region Singleton

    public static GameManager _instance;
    private void Awake() { _instance = this; }
    #endregion
    public ObjectInteractions objectInteractions;
    public Movement player;
    public CameraFollow cameraFollow;
    public GameObject ActivePlayer;
    public UIManager UIManager;
    public bool GameFinished;
    public TimeManager timeManager;
    public GameObject entryCamera;
    public void SetCursorState(bool cursosVisible) {
        Cursor.visible = cursosVisible;
        if(Cursor.visible) { Cursor.lockState = CursorLockMode.None; }
        else { Cursor.lockState = CursorLockMode.Locked; }
    }
}