using UnityEngine;
using Mirror;
public class AudioManager : NetworkBehaviour {
    [SerializeField] int timeOfRecording = 999;
    [SerializeField] AudioSource Server;
    [SerializeField] AudioSource Client;
    AudioClip serverClip;
    AudioClip clientClip;
    private void Start() {
        InitializeAudioSource();
    }
    private void Update() {

    }
    private void OnDisable() { Microphone.End(Microphone.devices[0]); }
    private void OnConnectedToServer() {
        Server.gameObject.SetActive(true);
        Client.gameObject.SetActive(false);
        clientClip = Microphone.Start(Microphone.devices[0], true, timeOfRecording, 48000);
        Client.clip = clientClip;
        Server.Play();
    }
    void InitializeAudioSource() {
        Server.gameObject.SetActive(false);
        Client.gameObject.SetActive(true);
        serverClip = Microphone.Start(Microphone.devices[0], true, timeOfRecording, 48000);
        Server.clip = serverClip;
        Client.Play();
    }
}