using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;
    private LobbyManager _manager;

    private void Start()
    {
        _manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string RoomName)
    {
        _roomName.text = RoomName;
    }

    public void OnClickItem()
    {
        _manager.JoinRoom(_roomName.text);
    }
}
