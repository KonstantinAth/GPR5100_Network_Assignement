using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NetworkMenu : NetworkBehaviour
{
   
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    GameObject MenuCanvas;

    NetworkManager manager;
    private void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    private void Update()
    {
        ConnectStatus();
    }

    public void StartHost()
    {
        
        manager.StartHost();
    }

    public void StartClient()
    {
        manager.networkAddress = inputField.text;
        manager.StartClient();
    }
  
    private void ConnectStatus()
    {
        if(NetworkClient.isConnected && NetworkServer.active)
        {
            MenuCanvas.SetActive(false);
        }
        else
        {
            MenuCanvas.SetActive(true);
        }    
    }
}
