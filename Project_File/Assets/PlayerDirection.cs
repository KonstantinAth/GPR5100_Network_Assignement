using UnityEngine;
public class PlayerDirection : MonoBehaviour {
    [SerializeField] Color facingTheRightWayColor;
    [SerializeField] float frequency;
    [Range(0, 0.90f)] [SerializeField] float directionMultiplier = 0.5f; 
    GameManager gameManagerInstance;
    UIManager uiManagerInstance;
    private void Start() {
        gameManagerInstance = GameManager._instance;
        uiManagerInstance = UIManager.uiManagerInstance_;
    }
    private void Update() {
        if(FacingTarget()) { uiManagerInstance.PingPongValue(frequency, facingTheRightWayColor); }
        else uiManagerInstance.closerToDirectionIndicator.color = uiManagerInstance.startingColor;
    }
    bool FacingTarget () {
        if (gameManagerInstance.player.gameObject.activeInHierarchy) {
            float dotProduct = Vector3.Dot(gameManagerInstance.player.transform.forward,
                (gameManagerInstance.ActivePlayer.transform.position - gameManagerInstance.player.transform.position).normalized);
            if (dotProduct > directionMultiplier) { return true; }
            return false;
        }
        return false;
    }
}