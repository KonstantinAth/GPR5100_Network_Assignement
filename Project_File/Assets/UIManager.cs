using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {
    #region Singleton
    public static UIManager uiManagerInstance_;
    private void Awake() { if (uiManagerInstance_ == null) uiManagerInstance_ = this; }
    #endregion
    public Image closerToDirectionIndicator;
    GameManager instance;
    public GameObject WinningCanvas;
    [SerializeField] TextMeshProUGUI deathCountText;
    [SerializeField] TextMeshProUGUI remainingTimeText;
    public Color startingColor;
    private void Start() {
        startingColor = closerToDirectionIndicator.color;
        instance = GameManager._instance;
    }
    public void PingPongValue(float frequency, Color newColor) {
        closerToDirectionIndicator.color = Color.Lerp(startingColor, newColor, Mathf.PingPong(Time.time, frequency));
    }
    public void UpdateTimeAndDeathCount(){
        float seconds = Mathf.Floor(instance.timeManager.timeRemaining % 60);
        float minutes = Mathf.Floor(instance.timeManager.timeRemaining / 60);
        deathCountText.text = $"Death Count : {instance.DeathCount}";
        remainingTimeText.text = $"Time Remained : {minutes}:{seconds}";
    }
    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
