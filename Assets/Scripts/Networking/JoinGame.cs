﻿using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class JoinGame : MonoBehaviourPunCallbacks
{

    const string GameSceneName = "Level";

    public TextMeshProUGUI LoadingText;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        LoadingText.text = "Looking for a Battleield..";

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LoadingText.text = "Making my own battlefield...";

        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 20 }, null);
    }

    public override void OnCreatedRoom()
    {
        LoadingText.text = "Finally made my own room...";

        SceneManager.LoadScene(GameSceneName);
    }

    public override void OnJoinedRoom()
    {
        LoadingText.text = "Entered a battlefield..";

        SceneManager.LoadScene(GameSceneName);
    }
}
