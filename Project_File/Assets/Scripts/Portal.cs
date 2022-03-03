using UnityEngine;
using System.Collections;
public class Portal : MonoBehaviour {
    GameManager manager;
    [SerializeField] World worldToGoNext;
    public bool isFinalDoor;
    private void Start() { 
        manager = GameManager._instance;
        isFinalDoor = this.transform == transform.parent.GetChild(transform.parent.childCount - 1);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (isFinalDoor) {
                Debug.Log("TRIGGERED");
                manager.GameFinished = true;
            }
            else { StartCoroutine(TriggerAndExit()); }
        }
    }
    IEnumerator TriggerAndExit() {
        manager.player.GetComponent<CharacterController>().enabled = false;
        manager.player.GetComponent<Animator>().enabled = false;
        FindObjectOfType<ObjectInteractions>().teleporting = true;
        FindObjectOfType<ObjectInteractions>().worldToGoNext = worldToGoNext;
        worldToGoNext.ThisWorldPlayer.enabled = true;
        FindObjectOfType<EventManager>().portalIndex++;
        yield return new WaitForEndOfFrame();
        worldToGoNext.previousPlayer.gameObject.SetActive(false);
        FindObjectOfType<ObjectInteractions>().teleporting = false;
    }
}