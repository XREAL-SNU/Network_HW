using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    LauncherScene, SampleScene, OtherScene
}

public class PlayerManager : MonoBehaviourPunCallbacks
{
    // singleton, dont destroy on load
    public static PlayerManager Instance = null;

    private void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(0.75f * Screen.width, 0.85f * Screen.height, 200, 50), "Leave"))
            PhotonNetwork.LeaveRoom();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("PlayerManager/OnSceneLoaded: " + scene.name);

        if(PhotonNetwork.OfflineMode || PhotonNetwork.InRoom)
        {
            InitializePlayer();
        }
    }


    public string SceneNameToLoad = "SampleScene";

    public override void OnJoinedRoom()
    {
        Debug.Log("PlayerManager/JoinedRoom as " + PhotonNetwork.LocalPlayer.NickName);
        Debug.Log("PlayerManager/Room properties :" + PhotonNetwork.CurrentRoom.ToString());
        // do not call this in createRoom.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            // Must load level with PhotonNetwork.LoadLevel, not SceneManager.LoadScene
            PhotonNetwork.LoadLevel(SceneNameToLoad);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"PlayerManager/Player {newPlayer.NickName} joined"); 
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("PlayerManager/CreatedRoom");
    }

    // Leave Room Callbacks
    public Action JoinedLobbyActions = null;

    public override void OnLeftRoom()
    {
        Debug.Log("PlayerManager/LeftRoom");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PlayerManager/Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("PlayerManager/Joined Lobby");
        // if actions are specified, invoke, else, return to main.
        if (JoinedLobbyActions != null) JoinedLobbyActions();
    }


    void InitializePlayer()
    {
        Debug.Log("INITIALIZE PLAYER");
        // instantiate camera, locally
        var spawnPoint = GameObject.Find("SpawnPoint").transform;

        var prefab = (GameObject)Resources.Load("PhotonPrefab/PlayerFollowCamera");
        var cam = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cam.name = "PlayerFollowCamera";

        Vector3 spawnPosition = Vector3.zero;
        // instantiate player and link
        if(spawnPoint != null)
        {
            spawnPosition = spawnPoint.position;
        }
        var player = PhotonNetwork.Instantiate("PhotonPrefab/CharacterPrefab", spawnPosition, Quaternion.identity);

        if (cam != null && player != null) cam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform.Find("FollowTarget");
    }


}
