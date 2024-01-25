using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_Text _buttonText;

    public void OnClickConnect()
    {
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
    }
}
