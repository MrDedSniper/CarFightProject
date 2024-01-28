using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_Text _buttonText;

    private const string AuthGuidKey = "auth_guid_key";
        
        public UnityEvent OnSuccessEvent;
        public UnityEvent OnErrorEvent;
    
        private bool isLogged;
    
        private void Start()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            {
                PlayFabSettings.staticSettings.TitleId = "131B5";
            }                        
        }      
    
        // Нажатие на кнопку входа
        public void OnTryToLogin()
        {
            var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
            var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());
            
            if (!isLogged)
            {
                var request = new LoginWithCustomIDRequest
                {
                    CustomId = id,
                    CreateAccount = !needCreation
                };
    
                PlayFabClientAPI.LoginWithCustomID(request,
                    result =>
                    {
                        PlayerPrefs.SetString(AuthGuidKey, id);
                        OnLoginSuccess(result);
                    }, OnLoginError);
            }
            
            PhotonNetwork.NickName = _usernameInput.text;
            _buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    
        // Ивенты различных сценариев
        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Complete Login");
            OnSuccessEvent.Invoke();
            SetUserData(result.PlayFabId);
        }
        
        private void SetUserData(string PlayFabId)
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string>
                    {
                        {"time_recieve_daily_reward", DateTime.UtcNow.ToString()}
                    }
                },
                result =>
                {
                    Debug.Log("SetUserData");
                    GetUserData(PlayFabId, "time_recieve_daily_reward");
                }, OnLoginError);
        }

        private void GetUserData(string playFabId, string keyData)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = playFabId
            }, result =>
            {
                if (result.Data.ContainsKey(keyData))
                    Debug.Log($"{keyData}: {result.Data[keyData].Value}");
            }, OnLoginError);
        }
            
        private void OnLoginError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError(errorMessage);
            OnErrorEvent.Invoke();
        }
        
        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene("Lobby");
        }
        
    
    
    /*[SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_Text _buttonText;

    private const string AuthGuidKey = "CarFightAuthGuidKey";

    public UnityEvent OnSuccessEvent;
    public UnityEvent OnFailureEvent;

    private void Start()
    {
        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());*/

        ////////
        
        
        /*var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
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
            error => Debug.LogError(error));*/
        
        //////
        
      /*  
}
    
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("OnLoginSuccess");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("OnLoginFailure: " + error.GenerateErrorReport());
    }
    
    
    public void ConnectedToServer()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMasterPlayfab");
        OnSuccessEvent.Invoke();

        if (_usernameInput.text.Length >= 1)
        {
            var request = new LoginWithPlayFabRequest
            {
                Username = _usernameInput.text,
                Password = _passwordInput.text
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
            
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
