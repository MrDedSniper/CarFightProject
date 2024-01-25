using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomCreationInput;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private TMP_Text _roomName;

    public RoomItem roomItemPrefab;
    private List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    private float _nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;
    public GameObject closeRoomButton;

    [SerializeField] private Toggle _isPrivateRoom;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    //Создание комнаты

    public void OnClickCreate()
    {
        if (_roomCreationInput.text.Length >= 1)
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = 4,
                BroadcastPropsChangeToAll = true,
                IsVisible = true,
                IsOpen = true
            };

            if (_isPrivateRoom.isOn)
            {
                roomOptions.IsOpen = false;
                roomOptions.IsVisible = false;
            }

            PhotonNetwork.CreateRoom(_roomCreationInput.text, roomOptions);
            
        }
    }

    public override void OnJoinedRoom()
    {
        _lobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= _nextUpdateTime)
        {
            UpdateRoomList(roomList);
            _nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }

        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
                
            if (room.IsOpen)
            {
                newRoom.GetComponent<Button>().interactable = true;
            }
            else
            {
                newRoom.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        _roomPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    private void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }

        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient /*&& PhotonNetwork.CurrentRoom.PlayerCount >= 2*/)
        {
            playButton.SetActive(true);
            closeRoomButton.SetActive(true);
        }

        else
        {
            playButton.SetActive(false);
            closeRoomButton.SetActive(false);
        }
    }

    public void OnClickCloseButton()
    {
        if (!PhotonNetwork.CurrentRoom.IsOpen)
        {
            return;
        }   
        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        playButton.GetComponent<Button>().interactable = true;
        closeRoomButton.GetComponent<Button>().interactable = false;
    }

    public void OnClickPlayButton()
    {
        if (!PhotonNetwork.CurrentRoom.IsOpen)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}