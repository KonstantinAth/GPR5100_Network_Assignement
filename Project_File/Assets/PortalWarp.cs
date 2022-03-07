using UnityEngine;
public class PortalWarp : MonoBehaviour {
    GameManager instance;
    [SerializeField] float minDistance;
    private void Start() { instance = GameManager._instance; }
    private void Update() {
        float distance = Vector3.Distance(instance.player.transform.position, transform.position);
        if(distance < minDistance) {
            instance.player.transform.position = Vector3.Lerp(instance.player.transform.position, transform.position, 1.0f * Time.deltaTime);
        }
    }
}