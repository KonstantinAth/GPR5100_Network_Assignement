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
        manager.StartHost();
    }
    public void StartClient() {
        if (inputField.text != null) {
            manager.networkAddress = inputField.text;
        }
        else {
            inputField.text = manager.networkAddress;
            manager.networkAddress = inputField.text;
        }
        manager.StartClient();
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