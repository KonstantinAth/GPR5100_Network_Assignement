using UnityEngine;
using Mirror;
using TMPro;
public class NetworkMenu : NetworkBehaviour {
    [SyncVar]
    public int playersConnected;
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    GameObject MenuCanvas;
    [SerializeField]
    NetworkManager manager;
    private void Update() {
        ConnectStatus();
        if (isServer) {
            playersConnected = NetworkServer.connections.Count;
        }
    }
    public void StartHost()  {
        NetworkManager.singleton.StartHost();
    }
    public void StartClient() {
        manager.StartClient();
        if (inputField.text!="") {
            Debug.Log("ded");
            manager.networkAddress = inputField.text;
        }
    }
    private void ConnectStatus() {
        if (NetworkClient.isConnected && NetworkServer.active) {
            MenuCanvas.SetActive(false);
        }
        else {
            MenuCanvas.SetActive(true);
        }
        if (playersConnected > 1) {
            MenuCanvas.SetActive(false);
        }
    }
    
}