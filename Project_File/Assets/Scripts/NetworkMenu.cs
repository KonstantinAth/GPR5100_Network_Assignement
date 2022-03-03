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
    private void Start() { 
        if(FindObjectsOfType<NetworkManager>().Length > 1) { Destroy(FindObjectsOfType<NetworkManager>()[1]); }
    }
    private void Update() {
        ConnectStatus();
        if (isServer) { playersConnected = NetworkServer.connections.Count; }
    }
    public void StartHost() {
        Time.timeScale = 1;
        manager.StartHost();
    }
    public void StartClient() {
        Time.timeScale = 1;
        if (inputField.text != "" && isAddressValid(inputField.text, inputField)) {
            Debug.Log("ded");
            manager.networkAddress = inputField.text;
            manager.StartClient();
        }
        else { manager.StartClient(); }
    }
    private void ConnectStatus() {
        if (NetworkClient.isConnected && NetworkServer.active) { MenuCanvas.SetActive(false); }
        else { MenuCanvas.SetActive(true); }
        if (playersConnected >= 2) { MenuCanvas.SetActive(false); }
    }
    bool isAddressValid(string input, TMP_InputField inputField) {
        var parts = input.Split('.');
        int correctCounter = 0;
        if (parts.Length == 4) {
            for (int i = 0; i < parts.Length; i++) {
                if (int.TryParse(parts[i], out int k)) {
                    if (k < 0 || k > 255) inputField.text = "The " + parts[i].ToString() + " of the address is not between 0-255";
                    else  correctCounter++;
                }
                else inputField.text = "The " + parts[i].ToString() + " of the address is not a number";
            }
        }
        else inputField.text = "You need 4 parts for the address";
        if (correctCounter == 4) { return true; }
        else { return false; }
    }
}