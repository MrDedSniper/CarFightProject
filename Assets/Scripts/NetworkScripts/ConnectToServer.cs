using System;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_Text _buttonText;

    private const string AuthGuidKey = "CarFightAuthGuidKey";

    public UnityEvent OnSuccessEvent;
    public UnityEvent OnFailureEvent;

    private void Start()
    {
        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());
        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = !needCreation
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                Debug.Log(result.PlayFabId);
                PhotonNetwork.AuthValues = new AuthenticationValues(result.PlayFabId);
                PhotonNetwork.NickName = result.PlayFabId;
            },
            error => Debug.LogError(error));
    }
    public void ConnectedToServer()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMasterPlayfab");
        OnSuccessEvent.Invoke();

        if (_usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = _usernameInput.text;
            _buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        OnFailureEvent.Invoke();
        Debug.Log("Disconnect");
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }

    /*public void OnClickConnect()
    {
       
        var request = new LoginWithCustomIDRequest
        {
            CustomId = _usernameInput.text,
            CreateAccount = true
        };
        
        if (_usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = _usernameInput.text;
            _buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }*/
}
