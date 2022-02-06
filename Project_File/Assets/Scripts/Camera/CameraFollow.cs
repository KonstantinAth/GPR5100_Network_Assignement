using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CameraFollow : NetworkBehaviour {
    GameManager gameManagerInstance_;
    delegate void Initialization();
    Initialization initialization;
    delegate void CameraFollowPlayer();
    CameraFollowPlayer followPlayer;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    [SerializeField] float lerpTime;
    [SerializeField] LayerMask layersToBeCulledIfHost;
    [SerializeField] LayerMask layersToBeCulledIfClient;
    [SerializeField] GameObject startingCamera;
    // Start is called before the first frame update
    void Start() { 
        initialization += ReferenceInitialization;
        initialization += ObjectInitialization;
        followPlayer += FollowPlayer;
        initialization();
        DetermineCullingMaskProperties();
    }
    // Update is called once per frame
    void Update() { 
        followPlayer();
        TurnEntryCameraOff();
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
    void ObjectInitialization() {
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
    void FollowPlayer() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(gameManagerInstance_.player.transform.position.x + xOffset,
            transform.position.y, gameManagerInstance_.player.transform.position.z + zOffset), lerpTime * Time.deltaTime);
    }
}