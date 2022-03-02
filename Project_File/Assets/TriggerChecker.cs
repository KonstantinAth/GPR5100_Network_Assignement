using UnityEngine;
public class TriggerChecker : MonoBehaviour {
    public bool triggered;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Default")) {
            Debug.Log($"DETECTING OTHER {other.gameObject.name}");
            other.GetComponent<Renderer>().enabled = false;
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Default")) {
            other.GetComponent<Renderer>().enabled = true;
            triggered = true;
        }
    }
}
