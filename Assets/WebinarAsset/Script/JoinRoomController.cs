using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomController : MonoBehaviour
{
    [SerializeField] private string versioName = "0.1";
    private static int playerno = 1;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versioName);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    private void SetUsername()
    {
        PhotonNetwork.playerName = "Player " + playerno;
        playerno++;
        Debug.Log(PhotonNetwork.playerName);
    }

    public void JoinRoom(string roomName)
    {
        SetUsername();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WebinarRoom");
        Debug.Log(PhotonNetwork.room);
    }
}
