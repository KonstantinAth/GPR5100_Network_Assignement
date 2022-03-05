using UnityEngine;
using System.Collections;
public class Portal : MonoBehaviour {
    GameManager manager;
    [SerializeField] World worldToGoNext;
    [SerializeField] bool isFinalDoor;
    private void Start() { 
        manager = GameManager._instance;
        isFinalDoor = this.transform == transform.parent.GetChild(transform.parent.childCount - 1);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (isFinalDoor) { manager.GameFinished = true; }
            else { StartCoroutine(TriggerAndExit()); }
        }
    }
    IEnumerator TriggerAndExit() {
        FindObjectOfType<ObjectInteractions>().teleporting = true;
        FindObjectOfType<ObjectInteractions>().worldToGoNext = worldToGoNext;
        manager.player.GetComponent<CharacterController>().enabled = false;
        manager.player.GetComponent<Animator>().enabled = false;
        manager.player.GetComponent<AudioSource>();
        worldToGoNext.ThisWorldPlayer.enabled = true;
        worldToGoNext.thisWorldCamera.GetComponent<Camera>().enabled = true;
        FindObjectOfType<EventManager>().portalIndex++;
        yield return new WaitForEndOfFrame();
        worldToGoNext.previousWorldCamera.GetComponent<Camera>().enabled = false;
        worldToGoNext.previousWorldCamera.SetActive(false);
        worldToGoNext.previousPlayer.gameObject.SetActive(false);
        FindObjectOfType<ObjectInteractions>().teleporting = false;
    }
}