using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;   // Assign the player prefab
    public Transform spawnPoint1;     // Assign SpawnPoint1
    public Transform spawnPoint2;     // Assign SpawnPoint2

    private static bool spawnPoint1Occupied = false; // To track if spawnPoint1 is occupied
    private static bool spawnPoint2Occupied = false; // To track if spawnPoint2 is occupied

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Transform selectedSpawnPoint;

        // Check if spawn points are occupied, and assign the free one
        if (!spawnPoint1Occupied)
        {
            //selectedSpawnPoint = spawnPoint1;
            transform.position = spawnPoint1.position;
            spawnPoint1Occupied = true; // Mark spawnPoint1 as occupied
        }
        else if (spawnPoint1Occupied)
        {
            //selectedSpawnPoint = spawnPoint2;
            transform.position = spawnPoint2.position;
            spawnPoint2Occupied = true; // Mark spawnPoint2 as occupied
        }
        else
        {
            Debug.LogError("Both spawn points are occupied!");
            return; // Do nothing if no spawn point is available
        }

        // Instantiate the player at the selected spawn point using PhotonNetwork.Instantiate
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, transform.rotation);
    }
}
