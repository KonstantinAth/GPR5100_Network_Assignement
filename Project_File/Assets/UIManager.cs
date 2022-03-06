using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Mirror;
public class UIManager : MonoBehaviour {
    #region Singleton
    public static UIManager uiManagerInstance_;
    private void Awake() { if (uiManagerInstance_ == null) uiManagerInstance_ = this; }
    #endregion
    public Image closerToDirectionIndicator;
    GameManager instance;
    [SerializeField] GameObject WinningCanvas;
    [SerializeField] TextMeshProUGUI deathCountText;
    [SerializeField] TextMeshProUGUI remainingTimeText;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject lobbyCanvas;
    private bool isPaused;
    public bool IsPaused {
        get { return isPaused; }
        set { isPaused = value; }
    }
    public Color startingColor;
    private void Start() {
        startingColor = closerToDirectionIndicator.color;
        instance = GameManager._instance;
    }
    private void Update() { 
        PauseGame();
        FinishGame();
        CheckClientState();
    }
    void PauseGame() {
        bool wantsToPause = Input.GetKeyDown(KeyCode.Escape);
        if (wantsToPause) {
            if (!isPaused) { Pause(); }
            else if (isPaused) { Resume(); }
        }
    }
    void Pause() {
        instance.SetCursorState(cursosVisible: true);
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    void Resume() {
        instance.SetCursorState(cursosVisible: false);
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    void CheckClientState() { if(!NetworkServer.localClientActive) { lobbyCanvas.SetActive(true); } }
    public void PingPongValue(float frequency, Color newColor) {
        closerToDirectionIndicator.color = Color.Lerp(startingColor, newColor, Mathf.PingPong(Time.time, frequency)); 
    }
    public void UpdateTimeAndDeathCount(){
        float seconds = Mathf.Floor(instance.timeManager.timeRemaining % 60);
        float minutes = Mathf.Floor(instance.timeManager.timeRemaining / 60);
        deathCountText.text = $"Death Count : {instance.timeManager.DeathCount}";
        remainingTimeText.text = $"Time Remained : {minutes}:{seconds}";
    }
    void FinishGame() {
        if(instance.GameFinished) {
            instance.entryCamera.SetActive(true);
            instance.player.GetComponent<CharacterController>().enabled = false;
            instance.player.GetComponent<Movement>().enabled = false;
            instance.player.GetComponent<AudioSource>().enabled = false;
            instance.SetCursorState(true);
            UpdateTimeAndDeathCount();
            WinningCanvas.SetActive(true);
        }
    }
    public void SetLoseCanvasState(bool enable) { loseCanvas.SetActive(enable); }
    public void ReloadScene() {
        if (instance.player.isThisServer) { NetworkManager.singleton.StopHost(); }
        else { NetworkManager.singleton.StopClient(); }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void DisconnectFromServer() {
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopServer();
        GameObject whoIsActive = pauseMenu.activeInHierarchy ? pauseMenu : loseCanvas;
        whoIsActive.SetActive(false);
        instance.entryCamera.SetActive(true);
        if(!lobbyCanvas.activeInHierarchy) lobbyCanvas.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitApplication() { Application.Quit(); }
    public void ResumeGame() { Resume(); }
}
