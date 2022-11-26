using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPosition;

    void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, Quaternion.identity, 0);
        player.transform.Rotate(0, 180, 0);
        player.GetComponent<PlayerController>().RandomizeAvatar();
        player.GetComponent<PlayerController>().SetName();
    }
}
