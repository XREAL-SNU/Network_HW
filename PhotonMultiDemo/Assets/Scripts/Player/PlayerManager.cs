using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XReal.XTown.UI;

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


    string SceneNameToLoad = "SampleScene";

    public override void OnJoinedRoom()
    {
        Debug.Log("PlayerManager/JoinedRoom as " + PhotonNetwork.LocalPlayer.NickName);
        Room currentRoom = PhotonNetwork.CurrentRoom;
        Debug.Log("Current Room : " + currentRoom.Name);
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

    public override void OnLeftRoom()
    {
        Debug.Log("PlayerManager/LeftRoom");
        SceneManager.LoadScene((int)SceneEnum.LauncherScene);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PlayerManager/Connected to master");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("PlayerManager/Joined Lobby");
        TypedLobby currentLobby = PhotonNetwork.CurrentLobby;
        Debug.Log("Current Lobby : " + currentLobby.Name);
        UIManager.UI.ShowSceneUI<RoomConnectionUIScript>("RoomConnectionUI");
    }


    void InitializePlayer()
    {
        Debug.Log("INITIALIZE PLAUYER");
        // instantiate camera, locally
        var prefab = (GameObject)Resources.Load("PhotonPrefab/PlayerFollowCamera");
        var cam = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cam.name = "PlayerFollowCamera";

        // instantiate player and link
        var player = PhotonNetwork.Instantiate("PhotonPrefab/CharacterPrefab", Vector3.zero, Quaternion.identity);
        if (cam != null && player != null) cam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform.Find("FollowTarget");
    }


}
