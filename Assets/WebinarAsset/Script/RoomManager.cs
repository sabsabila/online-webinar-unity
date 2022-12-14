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
        player.transform.GetChild(0).GetComponent<PlayerController>().RandomizeAvatar();
        player.transform.GetChild(0).GetComponent<PlayerController>().SetName();
    }
}
