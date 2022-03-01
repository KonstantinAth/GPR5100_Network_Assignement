using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    #region Singleton
    public static UIManager uiManagerInstance_;
    private void Awake() { if (uiManagerInstance_ == null) uiManagerInstance_ = this; }
    #endregion
    public Image closerToDirectionIndicator;
    public Color startingColor;
    private void Start() {
        startingColor = closerToDirectionIndicator.color;
    }
    public void PingPongValue(float frequency, Color newColor) {
        closerToDirectionIndicator.color = Color.Lerp(startingColor, newColor, Mathf.PingPong(Time.time, frequency));
    }
}
