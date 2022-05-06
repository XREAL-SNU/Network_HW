using Cinemachine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XReal.XTown.Persistance;

public enum SceneEnum
{
    LauncherScene, SampleScene, OtherScene
}

public class PlayerManager : MonoBehaviourPunCallbacks
{
    // singleton, dont destroy on load
    public static PlayerManager Instance = null;
    public PlayerData Data;
    protected const string _storageFileName = "player";
    protected string _dataPath;
    private void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
            Data = new PlayerData();

            InitializePlayerData();
            PlayerHierarchy = (GameObject)Resources.Load("PhotonPrefab/CharacterPrefab");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    void InitializePlayerData()
    {
        _dataPath = Application.dataPath;
        Storage storage = new Storage();
        object obj = storage.LoadData(_storageFileName);
        if (obj is null)
        {
            Data.myFaces = new List<int> { 11, 17, 2, 7 };
            Data.suitColor = SuitColorSync.ColorEnum.Blue;
            storage.SaveData((object)Data, _storageFileName);
            Debug.Log("Creating new player data file");
            return;
        }
        else
        {
            Data = (PlayerData)obj;
        }
    }
    public void SaveAllPlayerData()
    {
        Storage storage = new Storage();
        storage.SaveData(Data, _storageFileName);
        
        // following lines are debugging data
        object obj = storage.LoadData(_storageFileName);
        if (obj is null)
        {
            storage.SaveData((object)Data, _storageFileName);
            Debug.Log("Creating new player data file");
            return;
        }
        else
        {
            Data = (PlayerData)obj;
            Debug.Log("PlayerManager/SaveALL");

            Debug.Log(Data.myFaces.ToStringFull());
        }
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
        var prefab = (GameObject)Resources.Load("PhotonPrefab/PlayerFollowCamera");
        var cam = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cam.name = "PlayerFollowCamera";

        // instantiate player and link
        // var player = PhotonNetwork.Instantiate("PhotonPrefab/CharacterPrefab", Vector3.zero, Quaternion.identity);
        var player = SpawnPlayer();
        if (cam != null && player != null) cam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform.Find("FollowTarget");
    }

    const byte CustomManualInstantiationEventCode = 17;
    public GameObject PlayerHierarchy; 

    public GameObject SpawnPlayer()
    {
        string bundleName = "horror-handless";
        string assetName = "AvatarSpaceSuit_Handless";
        GameObject PlayerPrefab = AssetBundleLoader.LoadBundleAsset<GameObject>(bundleName, assetName);
        GameObject player = Instantiate(PlayerPrefab);
        
        // local setup. especially, copying photonview is important
        foreach (Component comp in PlayerHierarchy.GetComponents<Component>())
        {
            System.Type type = comp.GetType();
            Debug.Log("copying " + type.ToString());
            comp.PasteComponent(player);
        }
        PhotonView photonView = player.GetComponent<PhotonView>();

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[]
            {
            player.transform.position, player.transform.rotation, photonView.ViewID,
            bundleName, assetName
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                // must cache spawn event so i get spawned to players later joining room
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };
            photonView.Owner.TagObject = player;


            // raise instantiation event
            PhotonNetwork.RaiseEvent(CustomManualInstantiationEventCode, data, raiseEventOptions, sendOptions);
            return player;
        }
        else
        {
            Debug.LogError("Failed to allocate a ViewId.");

            Destroy(player);
            return null;
        }
    }
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CustomManualInstantiationEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            Debug.Log($"Instantiation: load {(string)data[3]}/{(string)data[4]} view{(int)data[2]}");
            GameObject PlayerPrefab = AssetBundleLoader.LoadBundleAsset<GameObject>((string)data[3], (string)data[4]);
            GameObject player = (GameObject)Instantiate(PlayerPrefab, (Vector3)data[0], (Quaternion)data[1]);
            // copy components
            foreach (Component comp in PlayerHierarchy.GetComponents<Component>())
            {
                System.Type type = comp.GetType();
                Debug.Log("copying " + type.ToString());
                comp.PasteComponent(player);
            }
            PhotonView photonView = player.GetComponent<PhotonView>();
            photonView.ViewID = (int)data[2];
            // tag object
            photonView.Owner.TagObject = player;
        }
    }
}
