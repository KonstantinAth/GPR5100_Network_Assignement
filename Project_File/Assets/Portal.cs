using UnityEngine;
using System.Collections;
public class Portal : MonoBehaviour {
    [SerializeField] World worldToGoNext;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(SetTeleport());
            FindObjectOfType<ObjectInteractions>().worldToGoNext = worldToGoNext;
            worldToGoNext.previousPlayer.gameObject.SetActive(false);
            worldToGoNext.ThisWorldPlayer.enabled = true; 
        }
    }
    IEnumerator SetTeleport()
    {
        FindObjectOfType<ObjectInteractions>().teleporting = true;
        yield return new WaitForEndOfFrame(); 
        FindObjectOfType<ObjectInteractions>().teleporting = false;
    }
}