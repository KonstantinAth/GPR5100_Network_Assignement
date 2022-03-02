using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CameraFollow : NetworkBehaviour {
    GameManager gameManagerInstance_;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    [SerializeField] float lerpTime;
    public LayerMask layersToBeCulledIfHost;
    public LayerMask layersToBeCulledIfClient;
    [SerializeField] GameObject startingCamera;
    // Start is called before the first frame update
    void Start() { ObjectInit(); }
    // Update is called once per frame
    void Update() {
        FollowPlayer();
        TurnEntryCameraOff();
    }
    void ObjectInit() {
        ReferenceInitialization();
        ObjectInitialization();
        FollowPlayer();
        DetermineCullingMaskProperties();
    }
    void TurnEntryCameraOff() {
        if (NetworkServer.active) {
            startingCamera.SetActive(false);
            return;
        }
    }
    void DetermineCullingMaskProperties() {
        if(IsHost()) { Camera.main.cullingMask = layersToBeCulledIfHost; }
        else { Camera.main.cullingMask = layersToBeCulledIfClient; }
    }
    bool IsHost() {
        if(isServer) { return true; }
        else if(!isServer) { return false; }
        return false;
    }
    void ReferenceInitialization() { gameManagerInstance_ = GameManager._instance; }
    void ObjectInitialization() { transform.position = new Vector3(transform.position.x, yOffset, transform.position.z); }
    public void SetPositionToOtherPlayer() {
        transform.position = new Vector3(gameManagerInstance_.player.transform.localPosition.x + xOffset, transform.localPosition.y, gameManagerInstance_.player.transform.localPosition.z + zOffset);
    }
    void FollowPlayer() {
        transform.position = Vector3.Lerp(transform.localPosition, new Vector3(gameManagerInstance_.player.transform.localPosition.x + xOffset,
            transform.localPosition.y, gameManagerInstance_.player.transform.localPosition.z + zOffset), lerpTime * Time.deltaTime);
    }
}