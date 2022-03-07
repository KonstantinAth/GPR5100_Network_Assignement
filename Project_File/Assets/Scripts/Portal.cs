using UnityEngine;
using System;
<<<<<<< HEAD
using System.Collections;
public class Portal : MonoBehaviour {
    GameManager manager;
    public static event Action<World> OnEnteredPortal;
    public int playerIndex = 0;
    private void Start() { manager = GameManager._instance; }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Portal")) {
            Debug.Log("TRIGGERED");
            //manager.timeManager.triggeredPortal = true;
            //OnEnteredPortal?.Invoke(GetComponent<Movement>().worldToGoNext);
            StartCoroutine(TriggerAndExit(GetComponent<Movement>().worldToGoNext));
            if (GetComponent<Movement>().isFinalPlayer) {
                manager.GameFinished = true;
            }
        }
    }
    IEnumerator TriggerAndExit(World worldToGoNext) {
        yield return new WaitForSeconds(0.5f);
        manager.PreviousPlayer.GetComponent<CharacterController>().enabled = false;
        manager.PreviousPlayer.GetComponent<Animator>().enabled = false;
        FindObjectOfType<ObjectInteractions>().teleporting = true;
        FindObjectOfType<ObjectInteractions>().worldToGoNext = worldToGoNext;
        manager.PreviousPlayer.GetComponent<AudioSource>().enabled = false;
        worldToGoNext.ThisWorldPlayer.enabled = true;
        worldToGoNext.thisWorldCamera.GetComponent<Camera>().enabled = true;
        yield return new WaitForEndOfFrame();
        manager.player = manager.objectInteractions.worldToGoNext.ThisWorldPlayer;
        manager.PreviousPlayer = transform.parent.GetChild(playerIndex).gameObject; /*players.GetChild(playerIndex).gameObject;*/
        manager.ActivePortal = GetComponent<Movement>().nextPortal.gameObject;
        worldToGoNext.previousWorldCamera.GetComponent<Camera>().enabled = false;
        worldToGoNext.previousWorldCamera.SetActive(false);
        worldToGoNext.previousPlayer.gameObject.SetActive(false);
        FindObjectOfType<ObjectInteractions>().teleporting = false;
        manager.timeManager.triggeredPortal = false;
=======
public class Portal : MonoBehaviour {
    GameManager manager;
    private void Start() { manager = GameManager._instance; }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Portal")) { Debug.Log("TRIGGERED"); manager.timeManager.triggeredPortal = true; }
>>>>>>> 6afdacf8fd0ce20a61bd7c70b2f82724a5c2f296
    }
}