using UnityEngine;
using System.Collections;
using System.Linq;
public class EventManager : MonoBehaviour {
    GameManager instance;
    [SerializeField] ObjectInteractions objectInteractions;
    [SerializeField] Transform portals;
    public int portalIndex = 0;
    private void OnEnable() {
        instance = GameManager._instance;
        InitializeEvents();
    }
    void InitializeEvents() {
        Trap.OnTrapDeath += Trap_OnTrapDeath;
        ObjectInteractions.OnEnteredQuicksand += ObjectInteractions_OnEnteredQuicksand;
        ObjectInteractions.OnExitedQuicksand += ObjectInteractions_OnExitedQuicksand;
        ObjectInteractions.OnEnteredPortal += ObjectInteractions_OnEnteredPortal;
    }

    private void Trap_OnTrapDeath() { StartCoroutine(SeeDeathAndReset(3.0f)); }
    private void ObjectInteractions_OnEnteredPortal() {
        instance.player = objectInteractions.worldToGoNext.ThisWorldPlayer;
        instance.ActivePortal = portals.GetChild(portalIndex).GetComponent<Portal>();
    }
    private void ObjectInteractions_OnEnteredQuicksand() {
        instance.player.moveSpeed = 3;
    }
    private void ObjectInteractions_OnExitedQuicksand() {
        instance.player.moveSpeed = instance.player.startingSpeed;
    }
    IEnumerator SeeDeathAndReset(float time) {
        Camera[] cameras = FindObjectsOfType<Camera>();
        Camera activeCamera = null;
        foreach (var item in cameras) { if(item.GetComponent<Camera>().enabled == true && item.gameObject.activeInHierarchy) { activeCamera = item; } }
        activeCamera.cullingMask = instance.cameraFollow.layersToBeCulledIfClient; 
        instance.player.GetComponent<CharacterController>().enabled = false;
        instance.player.GetComponent<Movement>().enabled = false; 
        instance.player.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(time);
        instance.player.transform.position = instance.player.startingPosition;
        if (instance.player.isThisServer) activeCamera.cullingMask = instance.cameraFollow.layersToBeCulledIfHost;
        yield return new WaitForEndOfFrame();
        FindObjectOfType<ObjectInteractions>().triggeredTrap = false;
        instance.player.GetComponent<CharacterController>().enabled = true;
        instance.player.GetComponent<Movement>().enabled = true;
        instance.player.GetComponent<Animator>().enabled = true;
    }
    private void OnDisable() {
        ObjectInteractions.OnEnteredQuicksand -= ObjectInteractions_OnEnteredQuicksand;
        ObjectInteractions.OnExitedQuicksand -= ObjectInteractions_OnExitedQuicksand;
        ObjectInteractions.OnEnteredPortal -= ObjectInteractions_OnEnteredPortal;
    }
}