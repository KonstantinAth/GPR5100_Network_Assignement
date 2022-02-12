using UnityEngine;
using System.Collections;
public class Portal : MonoBehaviour {
    [SerializeField] World worldToGoNext;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //StartCoroutine(SetTeleport());
            FindObjectOfType<ObjectInteractions>().teleporting = true;
            FindObjectOfType<ObjectInteractions>().worldToGoNext = worldToGoNext;
            worldToGoNext.ThisWorldPlayer.enabled = true; 
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            worldToGoNext.previousPlayer.gameObject.SetActive(false);
            FindObjectOfType<ObjectInteractions>().teleporting = false;
        }
    }
    IEnumerator SetTeleport()
    {
        FindObjectOfType<ObjectInteractions>().teleporting = true;
        yield return new WaitForEndOfFrame();
        FindObjectOfType<ObjectInteractions>().teleporting = false;
    }
}