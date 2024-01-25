using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[randomNumber];
        
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerCar"))
        {
            int carIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerCar"];
            if (carIndex >= 0 && carIndex < _playerPrefabs.Length)
            {
                GameObject playerToSpawn = _playerPrefabs[carIndex];
                PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Invalid player car index: " + carIndex);
            }
        }
        else
        {
            Debug.LogError("Player car property not found");
        }
        
        
        
        
        
        
        
        
        
        
        
        //GameObject playerToSpawn = _playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerCar"]];
        //PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
}
