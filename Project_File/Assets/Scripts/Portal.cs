using UnityEngine;
public class Portal : MonoBehaviour {

    [SerializeField] World worldToGoNext;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<ObjectInteractions>().teleporting = true;
            FindObjectOfType<ObjectInteractions>().worldToGoNext = worldToGoNext;
            worldToGoNext.ThisWorldPlayer.enabled = true;
            FindObjectOfType<EventManager>().portalIndex++;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            worldToGoNext.previousPlayer.gameObject.SetActive(false);
            FindObjectOfType<ObjectInteractions>().teleporting = false;
        }
    }
}