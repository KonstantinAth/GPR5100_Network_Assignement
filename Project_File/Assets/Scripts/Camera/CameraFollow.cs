using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    GameManager gameManagerInstance_;
    delegate void Initialization();
    Initialization initialization;
    delegate void CameraFollowPlayer();
    CameraFollowPlayer followPlayer;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    [SerializeField] float lerpTime;
    // Start is called before the first frame update
    void Start() { 
        initialization += ReferenceInitialization;
        initialization += ObjectInitialization;
        followPlayer += FollowPlayer;
        initialization();
    }
    // Update is called once per frame
    void Update() { followPlayer(); }
    void ReferenceInitialization() { gameManagerInstance_ = GameManager._instance; }
    void ObjectInitialization() {
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
    void FollowPlayer() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(gameManagerInstance_.player.transform.position.x + xOffset,
            transform.position.y, gameManagerInstance_.player.transform.position.z + zOffset), lerpTime * Time.deltaTime);
    }
}
